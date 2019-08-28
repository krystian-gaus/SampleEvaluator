using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEvaluation.Evaluator.Dto
{
  internal class DomainDto
  {
    #region Public Constructors

    public DomainDto(double minValue, double maxValue)
    {
      this.MinValue = minValue;
      this.MaxValue = maxValue;
    }

    #endregion Public Constructors

    #region Public Properties

    public double MinValue
    {
      get;
      private set;
    }

    public double MaxValue
    {
      get;
      private set;
    }

    #endregion Public Properties
  }
}
