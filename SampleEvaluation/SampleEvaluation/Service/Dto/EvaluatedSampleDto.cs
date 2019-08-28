using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Evaluator.Enum;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Service.Dto
{
  internal class EvaluatedSampleDto
  {
    #region Public Constructors

    public EvaluatedSampleDto(SampleDto sample, EvaluationResult result)
    {
      ExceptionHelper.CheckArgumentNull(sample, "sample");

      this.Sample = sample;
      this.Result = result;
    }

    #endregion Public Constructors

    #region Public Properties

    public SampleDto Sample
    {
      get;
      private set;
    }

    public EvaluationResult Result
    {
      get;
      private set;
    }

    #endregion Public Properties
  }
}
