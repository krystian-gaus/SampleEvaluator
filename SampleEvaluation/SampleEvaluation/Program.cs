using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Service;
using SampleEvaluation.Service.Dto;

namespace SampleEvaluation
{
  internal class Program
  {
    #region Private Fields

    private static string calibrationDirectory;
    private static string sampleDirectory;
    private static int calibrationSampleQuantity;

    #endregion Private Fields

    #region Public Methods

    public static void Main(string[] args)
    {
      try
      {
        InitializeFields();
        SampleEvaluationService evaluationService = new SampleEvaluationService();
        IEnumerable<EvaluatedSampleDto> evaluatedSamples = evaluationService.EvaluateSamples(calibrationDirectory, sampleDirectory, calibrationSampleQuantity);
        foreach (EvaluatedSampleDto evaluatedSample in evaluatedSamples)
        {
          WriteResultToConsole(evaluatedSample);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(string.Format("Failure during evaluation process: {0}", e.Message));
      }
      Console.Read();
    }

    #endregion Public Methods

    #region Private Methods
  
    private static void InitializeFields()
    {
      calibrationDirectory = Properties.Settings.Default.CalibrationDirectory;
      sampleDirectory = Properties.Settings.Default.SampleDirectory;
      calibrationSampleQuantity = Properties.Settings.Default.CalibrationSampleQuantity;
    }

    private static void WriteResultToConsole(EvaluatedSampleDto evaluatedSample)
    {
      Console.WriteLine("Evaluation result of sample ({0} | {1}): {2}", evaluatedSample.Sample.Concentration, evaluatedSample.Sample.Value, evaluatedSample.Result.ToString());
    }

    #endregion Private Methods
  }
}
