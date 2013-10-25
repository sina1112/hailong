using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.HaiLong.Lib.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace Com.HaiLong.Provider.lw
{
    /// <summary>
    ///  龙网房价实体类 
    /// </summary>
    [Serializable]
    public partial class RoomPrice
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 酒店ID
        /// </summary>
        public string hotelid { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime night { get; set; }
        /// <summary>
        /// 房型ID
        /// </summary>
        public string roomtypeid { get; set; }

        /// <summary>
        /// 含早份数：中1份，西1份，自1份
        /// </summary>
        public int? includebreakfastqty2 { get; set; }


        /// <summary>
        /// 增减价
        /// </summary>
        public int? changeprice { get; set; }
        /// <summary>
        /// 价格类型id
        /// </summary>
        public int ratetype { get; set; }

        /// <summary>
        /// 存在更新不存在添加数据
        /// </summary>
        public bool SaveOrUpdateChangePrice(string hotelid, string roomtypeid, DateTime night, int? includebreakfastqty2, int? changeprice,int ratetype)
        {
            StringBuilder strSql = new StringBuilder();
            string breakStr = includebreakfastqty2 == null ? "and includebreakfastqty2 is null" : "and includebreakfastqty2=@includebreakfastqty2";
            strSql.AppendFormat("if not exists (select id from lw_roomprice where roomtypeid=@roomtypeid and ratetype=@ratetype and night=@night {0} ) ", breakStr);
            strSql.Append(" insert into lw_roomprice(hotelid,roomtypeid,night,includebreakfastqty2,changeprice,ratetype) ");
            strSql.Append("values(@hotelid,@roomtypeid,@night,@includebreakfastqty2,@changeprice,@ratetype)");
            strSql.Append(" else ");
            strSql.Append("update [lw_roomprice] set ");
            strSql.Append("changeprice=@changeprice");
            strSql.Append(string.Format(" where roomtypeid =@roomtypeid and ratetype=@ratetype and  night=@night {0} ", breakStr));
            SqlParameter[] parameters = {
					new SqlParameter("@hotelid", SqlDbType.VarChar,50),
					new SqlParameter("@roomtypeid", SqlDbType.VarChar,50),
					new SqlParameter("@night", SqlDbType.DateTime),
					new SqlParameter("@includebreakfastqty2", SqlDbType.Int,4),
					new SqlParameter("@changeprice", SqlDbType.Int,4),
                    new SqlParameter("@ratetype", SqlDbType.Int,4)};
            parameters[0].Value = hotelid;
            parameters[1].Value = roomtypeid;
            parameters[2].Value = night;
            parameters[3].Value = includebreakfastqty2;
            parameters[4].Value = changeprice;
            parameters[5].Value = ratetype;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string roomTypeId,DateTime fromDate, DateTime toDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roomtypeid,night, includebreakfastqty2,changeprice,ratetype from lw_roomprice ");
            strSql.AppendFormat(" where  roomtypeid='{0}' and night between '{1}' and '{2}' ", roomTypeId, fromDate.ToShortDateString(), toDate.ToShortDateString());
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByHotelId(string hotelid, DateTime fromDate, DateTime toDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roomtypeid,night, includebreakfastqty2,changeprice,ratetype  from lw_roomprice ");
            strSql.AppendFormat(" where  hotelid='{0}' and night between '{1}' and '{2}' ", hotelid, fromDate.ToShortDateString(), toDate.ToShortDateString());
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }


        /// <summary>
        /// 删除今天之前数据
        /// </summary>
        public bool DeleteBeforeToday()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [lw_roomprice] ");
            strSql.Append(" where night<@night ");
            SqlParameter[] parameters = {
					new SqlParameter("@night", SqlDbType.DateTime)};
            parameters[0].Value = DateTime.Now.ToShortDateString();

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
