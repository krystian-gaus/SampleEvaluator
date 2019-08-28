using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Dto.Converter;
using SampleEvaluation.Evaluator.Enum;
using SampleEvaluation.Evaluator;
using SampleEvaluation.Evaluator.CalibrationCurve;
using SampleEvaluation.Evaluator.CalibrationCurve.Impl;
using SampleEvaluation.FileImport;
using SampleEvaluation.FileImport.Helper;
using SampleEvaluation.Validator;
using SampleEvaluation.Helper;
using SampleEvaluation.Service.Dto;
using System.Data.OleDb;

namespace SampleEvaluation.Service
{
  internal class SampleEvaluationService
  {
    #region Private Fields

    private FileHelper fileHelper;
    private HeaderTypeHelper headerTypeHelper;
    private ExcelReader excelReader;
    private DataSetReader dataSetReader;
    private SampleValidator sampleValidator;
    private SampleDtoConverter sampleDtoConverter;

    #endregion Private Fields

    #region Public Constructors

    public SampleEvaluationService()
    {
      this.Initialize();
    }

    #endregion Public Constructors

    #region Public Methods

    public IEnumerable<EvaluatedSampleDto> EvaluateSamples(string calibrationDirectory, string sampleDirectory, int calibrationSampleQuantity)
    {
      ExceptionHelper.CheckArgumentNullOrEmptyOrWhiteSpace(calibrationDirectory, "calibrationDirectory");
      ExceptionHelper.CheckArgumentNullOrEmptyOrWhiteSpace(sampleDirectory, "sampleDirectory");

      IList<EvaluatedSampleDto> evaluatedSamples = new List<EvaluatedSampleDto>();

      IEnumerable<SampleDto> calibrationSamples = this.GetCalibrationSamples(calibrationDirectory);
      this.ValidateCalibrationSamples(calibrationSamples, calibrationSampleQuantity);

      IEnumerable<SampleDto> samples = this.GetSamples(sampleDirectory);
      this.ValidateSamples(samples);

      ICalibrationCurve calibrationCurve = new StandardCalibrationCurve(calibrationSamples);
      SampleEvaluator sampleEvalutator = new SampleEvaluator(calibrationCurve);
      foreach(SampleDto sample in samples)
      {
        EvaluationResult result = sampleEvalutator.Evaluate(sample);
        evaluatedSamples.Add(new EvaluatedSampleDto(sample, result));
      }

      return evaluatedSamples;
    }

    #endregion Public Methods

    #region Private Methods

    private IEnumerable<SampleDto> GetSamples(string directory, bool isCalibrationDirectory)
    {
      IList<SampleDto> samples = new List<SampleDto>();

      IEnumerable<string> filePaths = this.fileHelper.GetFilePaths(directory);
      string filePath;
      if (filePaths.Count() == 1)
      {
        filePath = filePaths.First();
      }
      else
      {
        if(isCalibrationDirectory)
        {
          throw new Exception("No or more than one calibration samples file found");
        }
        else
        {
          throw new Exception("No or more than one samples file found");
        }
      }

      DataSet dataSet = this.excelReader.GetDataSetFromExcelFile(filePath);
      IEnumerable<string> headers = this.dataSetReader.GetHeaders(dataSet);
      IEnumerable<DataRow> rows = this.dataSetReader.GetRows(dataSet);
      bool headersAreSampleHeaders = this.headerTypeHelper.AreHeadersEqualToSampleHeaders(headers);

      if (headersAreSampleHeaders)
      {
        foreach (DataRow row in rows)
        {
          samples.Add(this.sampleDtoConverter.Convert(row));
        }
      }
      else
      {
        throw new Exception(string.Format("Excel file {0} hasn't got the right form", Path.GetFileName(filePath)));
      }

      return samples;
    }

    private IEnumerable<SampleDto> GetSamples(string sampleDirectory)
    {
      return this.GetSamples(sampleDirectory, false);
    }

    private void ValidateSamples(IEnumerable<SampleDto> samples)
    {
      this.sampleValidator.CheckSamples(samples);
    }

    private void ValidateCalibrationSamples(IEnumerable<SampleDto> calibrationSamples, int calibrationSampleQuantity)
    {
      this.sampleValidator.CheckCalibrationSamples(calibrationSamples, calibrationSampleQuantity);
    }

    private IEnumerable<SampleDto> GetCalibrationSamples(string calibrationDirectory)
    {
      return this.GetSamples(calibrationDirectory, true);
    }

    private void Initialize()
    {
      this.fileHelper = new FileHelper();
      this.headerTypeHelper = new HeaderTypeHelper();
      this.excelReader = new ExcelReader();
      this.dataSetReader = new DataSetReader();
      this.sampleValidator = new SampleValidator();
      this.sampleDtoConverter = new SampleDtoConverter();
    }

    #endregion Private Methods
  }
}
