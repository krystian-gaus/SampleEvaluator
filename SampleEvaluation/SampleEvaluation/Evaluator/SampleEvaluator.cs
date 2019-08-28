using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Evaluator.Enum;
using SampleEvaluation.Evaluator.CalibrationCurve;
using SampleEvaluation.Evaluator.CalibrationCurve.Impl;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Evaluator
{
  internal class SampleEvaluator
  {
    #region Private Fields

    private ICalibrationCurve calibrationCurve;

    #endregion Private Fields

    #region Public Constructors

    public SampleEvaluator(ICalibrationCurve calibrationCurve)
    {
      ExceptionHelper.CheckArgumentNull(calibrationCurve, "calibrationCurve");

      this.calibrationCurve = calibrationCurve;
    }

    #endregion Public Constructors

    #region Public Methods

    public EvaluationResult Evaluate(SampleDto sample)
    {
      ExceptionHelper.CheckArgumentNull(sample, "sample");

      EvaluationResult evaluationResult;

      double calibrationCurveValue = this.calibrationCurve.GetValue(sample.Concentration);
      if(sample.Value > calibrationCurveValue)
      {
        evaluationResult = EvaluationResult.POSITIVE;
      }
      else if (sample.Value < calibrationCurveValue)
      {
        evaluationResult = EvaluationResult.NEGATIVE;
      }
      else
      {
        evaluationResult = EvaluationResult.INDEFINITE;
      }

      return evaluationResult;
    }

    #endregion Public Methods
  }
}
