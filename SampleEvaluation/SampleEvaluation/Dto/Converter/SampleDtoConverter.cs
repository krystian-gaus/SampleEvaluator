using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.FileImport;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Dto.Converter
{
  internal class SampleDtoConverter
  {
    #region Private Fields

    private const string CONCENTRATION = "Concentration";
    private const string VALUE = "Value";

    #endregion Private Fields

    #region Public Methods

    public SampleDto Convert(DataRow row)
    {
      ExceptionHelper.CheckArgumentNull(row, "row");

      double concentration = System.Convert.ToDouble(row.Field<double>(CONCENTRATION));
      double value = System.Convert.ToDouble(row.Field<double>(VALUE));

      return new SampleDto(concentration, value);
    }

    #endregion Public Methods

  }
}
