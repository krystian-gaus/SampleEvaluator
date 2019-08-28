using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Evaluator.Dto.Converter
{
  internal class LinearFunctionParametersDtoConverter
  {
    #region Public Methods

    public LinearFunctionParametersDto Convert(LineParametersDto lineSegment)
    {
      ExceptionHelper.CheckArgumentNull(lineSegment, "lineSegment");

      SampleDto sample1 = lineSegment.FirstSample;
      SampleDto sample2 = lineSegment.SecondSample;

      double slope = (sample2.Value - sample1.Value) / (sample2.Concentration - sample1.Concentration);
      double verticalIntercept = sample1.Value - (slope * sample1.Concentration);
      double minValue;
      if (lineSegment.LeftBounded)
      {
        minValue = this.GetMinOf(sample1.Concentration, sample2.Concentration);
      }
      else
      {
        minValue = double.MinValue;
      }
      double maxValue;
      if (lineSegment.RightBounded)
      {
        maxValue = this.GetMaxOf(sample1.Concentration, sample2.Concentration);
      }
      else
      {
        maxValue = double.MaxValue;
      }
      DomainDto domain = new DomainDto(minValue, maxValue);

      return new LinearFunctionParametersDto(slope, verticalIntercept, domain);
    }

    #endregion Public Methods

    #region Private Methods

    private double GetMinOf(double d1, double d2)
    {
      double result;
      if (d1 <= d2)
      {
        result = d1;
      }
      else
      {
        result = d2;
      }
      return result;
    }

    private double GetMaxOf(double d1, double d2)
    {
      double result;
      if (d1 >= d2)
      {
        result = d1;
      }
      else
      {
        result = d2;
      }
      return result;
    }

    #endregion Private Methods
  }
}
