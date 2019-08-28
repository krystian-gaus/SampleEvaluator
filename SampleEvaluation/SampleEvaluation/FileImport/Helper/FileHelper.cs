using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Helper;

namespace SampleEvaluation.FileImport.Helper
{
  internal class FileHelper
  {
    #region Private Fields

    private const string SEARCH_PATTERN = "*.xlsx";

    #endregion Private Fields

    #region Public Methods

    public IEnumerable<string> GetFilePaths(string directoryPath)
    {
      ExceptionHelper.CheckArgumentNullOrEmptyOrWhiteSpace(directoryPath, "directoryPath");

      return Directory.EnumerateFiles(directoryPath, SEARCH_PATTERN);
    }

    #endregion Public Methods
  }
}
