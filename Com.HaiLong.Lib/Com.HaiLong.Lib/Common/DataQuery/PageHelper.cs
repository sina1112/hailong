using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Com.HaiLong.Lib.Common.DataQuery
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    public class PageHelper
    {
        /// <summary>
        /// 分页的存储过程名
        /// </summary>
        public const string PROC_PAGE = "SP_Page2005";
        /// <summary>
        /// 分页存储过程需要的参数
        /// </summary>
        private static SqlParameter[] _PageParams;
        /// <summary>
        /// 分页存储过程需要的参数
        /// </summary>
        public static SqlParameter[] PageParams
        {

            get
            {
                _PageParams = new SqlParameter[]{
                    new SqlParameter("@TableName", SqlDbType.VarChar,5000),
                    new SqlParameter("@Fields", SqlDbType.VarChar,5000),
                    new SqlParameter("@OrderField", SqlDbType.VarChar,5000),
                    new SqlParameter("@sqlWhere", SqlDbType.VarChar,5000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@pageIndex", SqlDbType.Int),
                    new SqlParameter("@IsStatCounts", SqlDbType.Int),
                    new SqlParameter("@Counts", SqlDbType.Int),
                    new SqlParameter("@TotalPage", SqlDbType.Int),
    
                     };
                SqlParameter[] clonedParms = new SqlParameter[_PageParams.Length];

                for (int i = 0, j = _PageParams.Length; i < j; i++)
                    clonedParms[i] = (SqlParameter)((ICloneable)_PageParams[i]).Clone();
                return clonedParms;
            }
        }
        /// <summary>
        /// 分页统计总数类型
        /// </summary>
        public enum StatType
        {
            /// <summary>
            /// 不统计总数
            /// </summary>
            UnStat = 0,
            /// <summary>
            /// 统计总数
            /// </summary>
            Stat = 1
        }
    }
}
