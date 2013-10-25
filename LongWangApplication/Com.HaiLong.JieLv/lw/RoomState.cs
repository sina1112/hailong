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
    ///  龙网房态实体类 
    /// </summary>
    [Serializable]
    public partial class RoomState
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
        /// 龙网房态
        /// </summary>
        public int? roomstate { get; set; }
        /// <summary>
        /// 价格类型id
        /// </summary>
        public int ratetype { get; set; }
        /// <summary>
        /// 存在更新不存在添加数据
        /// </summary>
        public bool SaveOrUpdateRoomState(string hotelid, string roomtypeid, DateTime night, int? includebreakfastqty2, int? roomstate, int ratetype)
        {
            StringBuilder strSql = new StringBuilder();
            string breakStr = includebreakfastqty2 == null ? "and includebreakfastqty2 is null" : "and (includebreakfastqty2=@includebreakfastqty2 or includebreakfastqty2=-1)";
            strSql.AppendFormat("if not exists (select id from lw_roomstate where roomtypeid=@roomtypeid and ratetype=@ratetype and night=@night {0}) ", breakStr);
            strSql.Append(" insert into lw_roomstate(hotelid,roomtypeid,night,includebreakfastqty2,roomstate,ratetype) ");
            strSql.Append("values(@hotelid,@roomtypeid,@night,@includebreakfastqty2,@roomstate,@ratetype)");
            strSql.Append(" else ");
            strSql.Append("update [lw_roomstate] set ");
            strSql.Append("roomstate=@roomstate");
            strSql.Append(string.Format(" where roomtypeid =@roomtypeid and ratetype=@ratetype and  night=@night {0} ", breakStr));
            SqlParameter[] parameters = {
					new SqlParameter("@hotelid", SqlDbType.VarChar,50),
					new SqlParameter("@roomtypeid", SqlDbType.VarChar,50),
					new SqlParameter("@night", SqlDbType.DateTime),
					new SqlParameter("@includebreakfastqty2", SqlDbType.Int,4),
					new SqlParameter("@roomstate", SqlDbType.Int,4),
                    new SqlParameter("@ratetype", SqlDbType.Int,4)};
            parameters[0].Value = hotelid;
            parameters[1].Value = roomtypeid;
            parameters[2].Value = night;
            parameters[3].Value = includebreakfastqty2;
            parameters[4].Value = roomstate;
            parameters[5].Value = ratetype;
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
       /// <summary>
        /// 批量保存数据
       /// </summary>
       /// <param name="roomTypes"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <param name="roomstate"></param>
       /// <returns></returns>
        public bool BatchSaveRoomState(DataTable roomTypes, DateTime startTime, DateTime endTime, int? roomstate)
        {
            bool isSuccess = false;
            DataTable temp = new DataTable();
            temp.Columns.Add("hotelid", typeof(string));
            temp.Columns.Add("roomtypeid", typeof(string));
            temp.Columns.Add("night", typeof(DateTime));
            temp.Columns.Add("includebreakfastqty2", typeof(int));
            temp.Columns.Add("roomstate", typeof(int));
            temp.Columns.Add("ratetype", typeof(int));
            foreach (DataRow item in roomTypes.Rows)
            {
                DateTime tempTime = startTime;
                while (tempTime <= endTime)
                {
                    DataRow dr = temp.NewRow();
                    dr[0] = item[0];
                    dr[1] = item[1];
                    dr[2] = tempTime;
                    dr[3] = -1;
                    if (roomstate == null)
                        dr[4] = DBNull.Value;
                    else
                        dr[4] = roomstate;
                    dr[5] = -1;//匹配所有价格类型
                    temp.Rows.Add(dr);
                    tempTime = tempTime.AddDays(1);
                }
            }

            using (SqlConnection connection = new SqlConnection(DbHelperSQL.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlBulkCopy sbc = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            if (temp.Rows.Count > 0)
                            {
                                sbc.ColumnMappings.Add("hotelid", "hotelid");
                                sbc.ColumnMappings.Add("roomtypeid", "roomtypeid");
                                sbc.ColumnMappings.Add("night", "night");
                                sbc.ColumnMappings.Add("includebreakfastqty2", "includebreakfastqty2");
                                sbc.ColumnMappings.Add("roomstate", "roomstate");
                                sbc.ColumnMappings.Add("ratetype", "ratetype");
                                sbc.BatchSize = temp.Rows.Count;
                                sbc.NotifyAfter = temp.Rows.Count;
                                sbc.BulkCopyTimeout = 5000;
                                sbc.DestinationTableName = "lw_roomstate";
                                sbc.WriteToServer(temp);
                            }
                        }
                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new ApplicationException("批量插入数据失败！" + e.Message);
                    }
                    finally
                    {
                        temp.Dispose();
                    }
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string roomTypeId, DateTime fromDate, DateTime toDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roomtypeid,night, includebreakfastqty2,roomstate,ratetype  from lw_roomstate ");
            strSql.AppendFormat(" where  roomtypeid='{0}' and night between '{1}' and '{2}' ", roomTypeId,fromDate.ToShortDateString(), toDate.ToShortDateString());
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByHotelId(string hotelid, DateTime fromDate, DateTime toDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roomtypeid,night, includebreakfastqty2,roomstate,ratetype  from lw_roomstate ");
            strSql.AppendFormat(" where  hotelid='{0}' and night between '{1}' and '{2}' ", hotelid, fromDate.ToShortDateString(), toDate.ToShortDateString());
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        

        /// <summary>
        /// 删除今天之前数据
        /// </summary>
        public bool DeleteBeforeToday()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [lw_roomstate] ");
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

        /// <summary>
        /// 删除数据
        /// </summary>
        public bool Delete(string hotelIds,DateTime startDate,DateTime endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [lw_roomstate] ");
            strSql.Append(string.Format(" where hotelid in({0}) ", hotelIds));
            strSql.AppendFormat(" and night between '{0}' and '{1}'", startDate.ToString(), endDate.ToString());
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
