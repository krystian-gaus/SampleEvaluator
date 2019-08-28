using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEvaluation.FileImport.Helper
{
  internal class HeaderTypeHelper
  {
    #region Private Fields

    private IEnumerable<string> sampleHeaders;

    #endregion Private Fields

    #region Public Constructors

    public HeaderTypeHelper()
    {
      sampleHeaders = new List<string>()
      {
        "Concentration",
        "Value"
      };
    }

    #endregion Public Constructors

    #region Public Methods

    public bool AreHeadersEqualToSampleHeaders(IEnumerable<string> headers)
    {
      return headers.Except(sampleHeaders).Count() == 0 && sampleHeaders.Except(headers).Count() == 0;
    }

    #endregion Public Methods
  }
}
