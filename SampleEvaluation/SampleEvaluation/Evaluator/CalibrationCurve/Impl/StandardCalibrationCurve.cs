using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Evaluator.Dto;
using SampleEvaluation.Evaluator.Dto.Converter;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Evaluator.CalibrationCurve.Impl
{
  internal class StandardCalibrationCurve : ICalibrationCurve
  {
    #region Private Fields

    private IEnumerable<LinearFunctionParametersDto> calibrationCurvePieces;

    #endregion Private Fields

    #region Public Constructors

    public StandardCalibrationCurve(IEnumerable<SampleDto> calibrationSamples)
    {
      ExceptionHelper.CheckArgumentNullOrCountLessThanTwo(calibrationSamples, "calibrationSamples");

      this.InitializeCalibrationCurvePieces(calibrationSamples);
    }

    #endregion Public Constructors

    #region Public Methods

    public double GetValue(double concentration)
    {
      LinearFunctionParametersDto parameters = this.calibrationCurvePieces.Where(x => this.isConcentrationInDomain(concentration, x.Domain)).First();
      return (parameters.Slope * concentration) + parameters.VerticalIntercept;
    }

    #endregion Public Methods

    #region Private Methods

    private void InitializeCalibrationCurvePieces(IEnumerable<SampleDto> calibrationSamples)
    {
      IList<LinearFunctionParametersDto> calibrationCurvePieces = new List<LinearFunctionParametersDto>();
      LinearFunctionParametersDtoConverter linearFunctionConverter = new LinearFunctionParametersDtoConverter();    
      calibrationSamples = calibrationSamples.OrderBy(x => x.Concentration);
      int samplesCount = calibrationSamples.Count();

      for (int i = 0; i < samplesCount - 1; i++)
      {
        SampleDto sample1 = calibrationSamples.ElementAt(i);
        SampleDto sample2 = calibrationSamples.ElementAt(i + 1);
        bool leftBounded = i == 0 ? false : true;
        bool rightBounded = i == samplesCount - 2 ? false : true;
        LineParametersDto lineParameters = new LineParametersDto(sample1, sample2, leftBounded, rightBounded);

        calibrationCurvePieces.Add(linearFunctionConverter.Convert(lineParameters));
      }

      this.calibrationCurvePieces = calibrationCurvePieces;
    }

    private bool isConcentrationInDomain(double concentration, DomainDto domain)
    {
      return concentration >= domain.MinValue && concentration <= domain.MaxValue;
    }

    #endregion Private Methods
  }
}
