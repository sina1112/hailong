using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Com.HaiLong.Lib.DBUtility;
using System.Xml.Serialization;
namespace Com.HaiLong.Provider.Qunar
{
    /// <summary>
    /// 城市类
    /// </summary>
    [Serializable]
    [XmlType("city")]
    public partial class City
    {
        public City()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _countryname;
        private string _countrypy;
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("countryPy")]
        public string CountryPy
        {
            set { _countrypy = value; }
            get { return _countrypy; }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("countryName")]
        public string CountryName
        {
            set { _countryname = value; }
            get { return _countryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("code")]
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }


        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public City(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Code,Name,CountryName,CountryPy ");
            strSql.Append(" FROM [Qunar_City] ");
            strSql.Append(" where Code=@Code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar,-1)};
            parameters[0].Value = Code;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"] != null)
                {
                    this.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CountryName"] != null)
                {
                    this.CountryName = ds.Tables[0].Rows[0]["CountryName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CountryPy"] != null)
                {
                    this.CountryPy = ds.Tables[0].Rows[0]["CountryPy"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Qunar_City]");
            strSql.Append(" where Code=@Code ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar,-1)};
            parameters[0].Value = Code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [Qunar_City] (");
            strSql.Append("Code,Name,CountryName,CountryPy)");
            strSql.Append(" values (");
            strSql.Append("@Code,@Name,@CountryName,@CountryPy)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar,100),
                    new SqlParameter("@Name", SqlDbType.NChar,50),
                    new SqlParameter("@CountryName", SqlDbType.NChar,50),
                    new SqlParameter("@CountryPy", SqlDbType.VarChar,10)};
            parameters[0].Value = Code;
            parameters[1].Value = Name;
            parameters[2].Value = CountryName;
            parameters[3].Value = CountryPy;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Qunar_City] set ");
            strSql.Append("Name=@Name,");
            strSql.Append("CountryName=@CountryName,");
            strSql.Append("CountryPy=@CountryPy");
            strSql.Append(" where Code=@Code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NChar,50),
                    new SqlParameter("@CountryName", SqlDbType.NChar,50),
                    new SqlParameter("@CountryPy", SqlDbType.VarChar,10),
                    new SqlParameter("@Code", SqlDbType.VarChar,100)};
            parameters[0].Value = Name;
            parameters[1].Value = CountryName;
            parameters[2].Value = CountryPy;
            parameters[3].Value = Code;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [Qunar_City] ");
            strSql.Append(" where Code=@Code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar,-1)};
            parameters[0].Value = Code;

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
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Code,Name,CountryName,CountryPy ");
            strSql.Append(" FROM [Qunar_City] ");
            strSql.Append(" where Code=@Code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar,-1)};
            parameters[0].Value = Code;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"] != null)
                {
                    this.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CountryName"] != null)
                {
                    this.CountryName = ds.Tables[0].Rows[0]["CountryName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CountryPy"] != null)
                {
                    this.CountryPy = ds.Tables[0].Rows[0]["CountryPy"].ToString();
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [Qunar_City] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

       
        #endregion  Method
    }
}

