using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Helper;

namespace SampleEvaluation.FileImport
{
  internal class ExcelReader
  {
    #region Private Fields

    private const string EXTENDED_PROPERTIES_KEYWORD = "Extended Properties";
    private const string EXTENDED_PROPERTIES_VALUES = "Excel 12.0 XML;HDR=Yes";
    private const string PROVIDER = "Microsoft.ACE.OLEDB.12.0";

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Creates a data set from the given excel file
    /// </summary>
    /// <param name="excelFilePath"></param>
    /// <returns></returns>
    public DataSet GetDataSetFromExcelFile(string excelFilePath)
    {
      ExceptionHelper.CheckArgumentNullOrEmptyOrWhiteSpace(excelFilePath, "excelFilePath");

      DataSet ds = new DataSet();

      OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
      csbuilder.DataSource = excelFilePath;
      csbuilder.Provider = PROVIDER;
      csbuilder.Add(EXTENDED_PROPERTIES_KEYWORD, EXTENDED_PROPERTIES_VALUES);

      using(OleDbConnection con = new OleDbConnection(csbuilder.ToString()))
      {
        con.Open();

        DataTable schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

        if (schemaTable == null || schemaTable.Rows.Count == 0)
        {
          throw new Exception(string.Format("Es wurde kein Sheet aus der Excel-Datei {0} ausgelesen", Path.GetFileName(excelFilePath)));
        }
        else
        {
          //only use first sheet, because empty sheets are problematic
          string tableName = schemaTable.Rows[0]["Table_Name"].ToString();
          string sql = string.Format("SELECT * FROM [{0}]", tableName);
          OleDbDataAdapter adap = new OleDbDataAdapter(sql, con);
          adap.Fill(ds, tableName);
        }

        con.Close();
      }

      return ds;
    }

    #endregion Public Methods
  }
}
