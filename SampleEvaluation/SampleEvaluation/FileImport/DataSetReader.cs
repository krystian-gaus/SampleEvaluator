using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Helper;

namespace SampleEvaluation.FileImport
{
  internal class DataSetReader
  {
    #region Public Methods

    public IEnumerable<string> GetHeaders(DataSet dataSet)
    {
      ExceptionHelper.CheckArgumentNull(dataSet, "dataSet");

      //ExcelReader only reads data from first sheet
      return dataSet.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName);
    }

    public IEnumerable<DataRow> GetRows(DataSet dataSet)
    {
      ExceptionHelper.CheckArgumentNull(dataSet, "dataSet");

      //ExcelReader only reads data from first sheet
      return dataSet.Tables[0].Rows.Cast<DataRow>();
    }

    #endregion Public Methods
  }
}
