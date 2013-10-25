using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.HaiLong.Lib.DBUtility;

namespace Com.HaiLong.Provider.JieLv
{
    /// <summary>
    /// 城市类
    /// </summary>
    [Serializable]
    public class City
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<City> GetCitys()
        {
            List<City> list = new List<City>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 5000 c.cityid,c.name  from dbo.jltour_city c left join hpm_hotel h on c.cityid=h.city where h.hotelid is not null  and  c.qunarcitycode is not null and c.active=1  group by c.cityid,c.name  ORDER BY c.name");
            using (SqlDataReader rdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (rdr.Read())
                {
                    list.Add(ReaderBind(rdr));
                }
            }
            return list;
        }
        #region 对象实体绑定数据
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        private City ReaderBind(IDataReader rdr)
        {
            var model = new City
            {
                CityId = rdr.GetInt32(0),
                Name = rdr.GetString(1),
            };
            return model;
        }
        #endregion


    }
}
