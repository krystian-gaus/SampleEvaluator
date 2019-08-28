using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Evaluator.Dto
{
  internal class LineParametersDto
  {
    #region Public Constructors

    public LineParametersDto(SampleDto firstSample, SampleDto secondSample, bool leftBounded, bool rightBounded)
    {
      ExceptionHelper.CheckArgumentNull(firstSample, "firstSample");
      ExceptionHelper.CheckArgumentNull(secondSample, "secondSample");
      if (firstSample.Concentration == secondSample.Concentration)
      {
        throw new ArgumentException("Samples must not have equal concentrations");
      }
      else
      {
        //ok
      }

      this.FirstSample = firstSample;
      this.SecondSample = secondSample;
      this.LeftBounded = leftBounded;
      this.RightBounded = rightBounded;
    }

    #endregion Public Constructors

    #region Public Properties

    public SampleDto FirstSample
    {
      get;
      private set;
    }

    public SampleDto SecondSample
    {
      get;
      private set;
    }

    public bool LeftBounded
    {
      get;
      private set;
    }

    public bool RightBounded
    {
      get;
      private set;
    }

    #endregion Public Properties
  }
}
