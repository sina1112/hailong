using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;
using Com.HaiLong.Lib.DBUtility;
using System.Data;

namespace Com.HaiLong.Provider.Qunar
{
    [XmlRoot("list")]
    public class CityList:List<City>
    {
        /// <summary>
        /// 批量增加数据
        /// </summary>
        /// <param name="dt"></param>
        public static void BulkToDB(CityList list)
        {
            DataTable dt = GetTableSchema();
            foreach (var city in list)
            {
                DataRow r = dt.NewRow();
                r[0] = city.Code;
                r[1] = city.Name;
                r[2] = city.CountryName;
                r[3] = city.CountryPy;
                dt.Rows.Add(r);
            }
            SqlConnection sqlConn = new SqlConnection(PubConstant.ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = "Qunar_City";
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }

        private static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
        new DataColumn("Code",typeof(string)),
        new DataColumn("Name",typeof(string)),
	    new DataColumn("CountryName",typeof(string)),
        new DataColumn("CountryPy",typeof(string))});

            return dt;
        }
    }
}
