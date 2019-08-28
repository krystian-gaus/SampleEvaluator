using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEvaluation.Helper
{
  internal static class ExceptionHelper
  {
    #region Public Methods

    public static void CheckArgumentNullOrEmptyOrWhiteSpace(string argument, string argumentDescription)
    {
      if (string.IsNullOrWhiteSpace(argument))
      {
        throw new ArgumentException(string.Format("{0} must not be null or empty or whitespace", argumentDescription));
      }
      else
      {
        //ok
      }
    }

    public static void CheckArgumentNullOrEmpty(IEnumerable<object> argument, string argumentDescription)
    {
      if (argument == null || argument.Count() == 0)
      {
        throw new ArgumentException(string.Format("{0} must not be null or empty", argumentDescription));
      }
      else
      {
        //ok
      }
    }

    public static void CheckArgumentNullOrCountLessThanTwo(IEnumerable<object> argument, string argumentDescription)
    {
      if (argument == null)
      {
        throw new ArgumentException(string.Format("{0} must not be null or empty", argumentDescription));
      }
      else if (argument.Count() < 2)
      {
        throw new ArgumentException(string.Format("{0} must not have less elements than 2", argumentDescription));
      }
      else
      {
        //ok
      }
    }

    public static void CheckArgumentNull(object argument, string argumentDescription)
    {
      if (argument == null)
      {
        throw new ArgumentException(string.Format("{0} must not be null", argumentDescription));
      }
      else
      {
        //ok
      }
    }

    #endregion Public Methods
  }
}
