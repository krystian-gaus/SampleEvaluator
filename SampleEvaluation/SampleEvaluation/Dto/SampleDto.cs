using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEvaluation.Dto
{
  internal class SampleDto
  {
    #region Public Constructors

    public SampleDto(double concentration, double value)
    {
      this.Concentration = concentration;
      this.Value = value;
    }

    #endregion Public Constructors

    #region Public Properties

    public double Concentration
    {
      get;
      private set;
    }

    public double Value
    {
      get;
      private set;
    }

    #endregion Public Properties
  }
}
