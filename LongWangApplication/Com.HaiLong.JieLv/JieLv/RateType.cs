using System;
using Com.HaiLong.Lib.DBUtility;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Com.HaiLong.Provider.JieLv
{
    /// <summary>
    /// RateType:价格类型类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RateType
    {
        public RateType()
        { }
        #region Model
        private int _ratetypeid;
        private string _ratetypename;
        /// <summary>
        /// 价格类型ID
        /// </summary>
        public int ratetypeid
        {
            set { _ratetypeid = value; }
            get { return _ratetypeid; }
        }
        /// <summary>
        /// 价格类型名称
        /// </summary>
        public string ratetypename
        {
            set { _ratetypename = value; }
            get { return _ratetypename; }
        }
       
        #endregion Model

        #region  Method

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ratetypeid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [hpm_ratetype]");
			strSql.Append(" where ratetypeid=@ratetypeid ");

			SqlParameter[] parameters = {
					new SqlParameter("@ratetypeid", SqlDbType.Int,4)};
			parameters[0].Value = ratetypeid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [hpm_ratetype] (");
			strSql.Append("ratetypename)");
			strSql.Append(" values (");
			strSql.Append("@ratetypename)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ratetypename", SqlDbType.NVarChar,96)};
			parameters[0].Value = ratetypename;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [hpm_ratetype] set ");
			strSql.Append("ratetypename=@ratetypename");
			strSql.Append(" where ratetypeid=@ratetypeid ");
			SqlParameter[] parameters = {
					new SqlParameter("@ratetypename", SqlDbType.NVarChar,96),
					new SqlParameter("@ratetypeid", SqlDbType.Int,4)};
			parameters[0].Value = ratetypename;
			parameters[1].Value = ratetypeid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ratetypeid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [hpm_ratetype] ");
			strSql.Append(" where ratetypeid=@ratetypeid ");
			SqlParameter[] parameters = {
					new SqlParameter("@ratetypeid", SqlDbType.Int,4)};
			parameters[0].Value = ratetypeid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(int ratetypeid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ratetypeid,ratetypename ");
			strSql.Append(" FROM [hpm_ratetype] ");
			strSql.Append(" where ratetypeid=@ratetypeid ");
			SqlParameter[] parameters = {
					new SqlParameter("@ratetypeid", SqlDbType.Int,4)};
			parameters[0].Value = ratetypeid;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ratetypeid"]!=null && ds.Tables[0].Rows[0]["ratetypeid"].ToString()!="")
				{
					this.ratetypeid=int.Parse(ds.Tables[0].Rows[0]["ratetypeid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ratetypename"]!=null )
				{
					this.ratetypename=ds.Tables[0].Rows[0]["ratetypename"].ToString();
				}
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM [hpm_ratetype] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method

    }
}
