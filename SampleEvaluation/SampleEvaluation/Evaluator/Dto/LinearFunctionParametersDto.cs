using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Evaluator.Dto
{
  internal class LinearFunctionParametersDto
  {
    #region Public Constructors

    public LinearFunctionParametersDto(double slope, double verticalIntercept, DomainDto domain)
    {
      ExceptionHelper.CheckArgumentNull(domain, "domain");

      this.Slope = slope;
      this.VerticalIntercept = verticalIntercept;
      this.Domain = domain;
    }

    #endregion Public Constructors

    #region Public Properties

    public double Slope
    {
      get;
      private set;
    }

    public double VerticalIntercept
    {
      get;
      private set;
    }

    public DomainDto Domain
    {
      get;
      private set;
    }

    #endregion Public Properties
  }
}
