using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Com.HaiLong.Lib.DBUtility;
using Com.HaiLong.Lib.Common.DataQuery;
using System.Data.SqlClient;
namespace Com.HaiLong.Provider.JieLv
{
    /// <summary>
    /// Hotel:酒店实体类 
    /// </summary>
    [Serializable]
    public partial class Hotel
    {
        public Hotel()
        { }
        #region Model
        private int _hotelid;
        private string _hotelcd;
        private int _star;
        private string _namechn;
        private string _nameeng;
        private int _country;
        private int _state;
        private int _city;
        private int? _zone;
        private int? _bd;
        private int? _floor;
        private string _website;
        private string _addresschn;
        private string _adresseng;
        private string _centraltel;
        private string _fax;
        private string _postcode;
        private string _email;
        private string _language;
        private string _themetype;
        private string _acceptcustom;
        private string _introducechn;
        //private string _summarychn;
        private string _allowcreditcard;
        private string _facilities;
        private string _facilitiesdisabled;
        private string _interiornotes;
        private string _remark;
        private int? _supplierminor;
        private string _keynames;
        private string _jingdu;
        private string _weidu;
        private int _pricechange = 8;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _active;
        private string _outeriornotes;
        /// <summary>
        /// 酒店ID
        /// </summary>
        public int hotelid
        {
            set { _hotelid = value; }
            get { return _hotelid; }
        }
        /// <summary>
        /// 酒店编码
        /// </summary>
        public string hotelcd
        {
            set { _hotelcd = value; }
            get { return _hotelcd; }
        }

        /// <summary>
        /// 酒店星级
        /// </summary>
        public int star
        {
            set { _star = value; }
            get { return _star; }
        }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string namechn
        {
            set { _namechn = value; }
            get { return _namechn; }
        }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string nameeng
        {
            set { _nameeng = value; }
            get { return _nameeng; }
        }

        /// <summary>
        /// 国家
        /// </summary>
        public int country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public int state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public int city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 城市区
        /// </summary>
        public int? zone
        {
            set { _zone = value; }
            get { return _zone; }
        }
        /// <summary>
        /// 商业区
        /// </summary>
        public int? bd
        {
            set { _bd = value; }
            get { return _bd; }
        }
        /// <summary>
        /// 层高
        /// </summary>
        public int? floor
        {
            set { _floor = value; }
            get { return _floor; }
        }
        /// <summary>
        /// 酒店网站
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 中文地址
        /// </summary>
        public string addresschn
        {
            set { _addresschn = value; }
            get { return _addresschn; }
        }
        /// <summary>
        /// 英文地址
        /// </summary>
        public string adresseng
        {
            set { _adresseng = value; }
            get { return _adresseng; }
        }
        /// <summary>
        /// 电话总机
        /// </summary>
        public string centraltel
        {
            set { _centraltel = value; }
            get { return _centraltel; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// email
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 语言类型
        /// </summary>
        public string language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 酒店主题
        /// </summary>
        public string themetype
        {
            set { _themetype = value; }
            get { return _themetype; }
        }
        /// <summary>
        /// 接受的客人类型
        /// </summary>
        public string acceptcustom
        {
            set { _acceptcustom = value; }
            get { return _acceptcustom; }
        }
        /// <summary>
        /// 中文介绍
        /// </summary>
        public string introducechn
        {
            set { _introducechn = value; }
            get { return _introducechn; }
        }
        ///// <summary>
        ///// 中文摘要
        ///// </summary>
        //public string summarychn
        //{
        //    set { _summarychn = value; }
        //    get { return _summarychn; }
        //}

        /// <summary>
        /// 能处理的信用卡类型
        /// </summary>
        public string allowcreditcard
        {
            set { _allowcreditcard = value; }
            get { return _allowcreditcard; }
        }

        /// <summary>
        /// 酒店措施
        /// </summary>
        public string facilities
        {
            set { _facilities = value; }
            get { return _facilities; }
        }
        /// <summary>
        /// 残疾人措施
        /// </summary>
        public string facilitiesdisabled
        {
            set { _facilitiesdisabled = value; }
            get { return _facilitiesdisabled; }
        }


        /// <summary>
        /// 特别提示
        /// </summary>
        public string interiornotes
        {
            set { _interiornotes = value; }
            get { return _interiornotes; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 是否捷旅优势酒店
        /// </summary>
        public int? supplierminor
        {
            set { _supplierminor = value; }
            get { return _supplierminor; }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string keynames
        {
            set { _keynames = value; }
            get { return _keynames; }
        }
        /// <summary>
        /// 精度
        /// </summary>
        public string jingdu
        {
            set { _jingdu = value; }
            get { return _jingdu; }
        }
        /// <summary>
        /// 维度
        /// </summary>
        public string weidu
        {
            set { _weidu = value; }
            get { return _weidu; }
        }
        /// <summary>
        /// 此酒店是否正在变价中
        /// </summary>
        public int pricechange
        {
            set { _pricechange = value; }
            get { return _pricechange; }
        }
        /// <summary>
        /// 变价开始日期
        /// </summary>
        public DateTime? begintime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 变价结束日期
        /// </summary>
        public DateTime? endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 是否生效
        /// </summary>
        public int active
        {
            set { _active = value; }
            get { return _active; }
        }
        /// <summary>
        /// 外部备注
        /// </summary>
        public string outeriornotes
        {
            set { _outeriornotes = value; }
            get { return _outeriornotes; }
        }
        #endregion Model

        /// <summary>
        /// 酒店房型
        /// </summary>
        public List<RoomType> Rooms;



        /// <summary>
        /// 更新数据
        /// </summary>
        public bool Update(string hotelids, int isActive)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [hpm_hotel] set ");
            strSql.Append(string.Format("active={0}", isActive));
            strSql.Append(string.Format(" where hotelid in({0}) ",hotelids));
            

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
        /// <summary>
        ///  下线全部已上线的酒店
        /// </summary>
        /// <returns></returns>
        public bool SetUnActive()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [hpm_hotel] set active=8 where active=1 ");


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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(int hotelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.qunarcitycode,h.addresschn, h.centraltel  from hpm_hotel h inner join jltour_city c on h.city=c.cityid  ");
            strSql.AppendFormat(" where  h.hotelid={0}", hotelId);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string cityName, string hotelName, ref Query dataQuery)
        {
            cityName = cityName.Replace("'", "");
            hotelName = hotelName.Replace("'", "");
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append("c.qunarcitycode is not null and h.hoteltype in(1,2) and  h.country in ('70007','70008','70009') ");

            if (!string.IsNullOrWhiteSpace(cityName))
            {
                strwhere.AppendFormat("  and  c.name like '%{0}%' ", cityName);
            }
            if (!string.IsNullOrWhiteSpace(hotelName))
            {
                strwhere.AppendFormat("   and h.namechn like '%{0}%'", hotelName);
            }

            SqlParameter[] parms = PageHelper.PageParams;
            parms[0].Value = "hpm_hotel h inner join jltour_city c on h.city=c.cityid";
            parms[1].Value = "h.hotelid,c.name,h.namechn,h.active,h.state";
            parms[2].Value = dataQuery.IsHaveOrderInfo ? dataQuery.OrderInfo.GenerateSQLString() : "h.active asc";
            parms[3].Value = strwhere.ToString();
            parms[4].Value = dataQuery.PageInfo.PageSize;
            parms[5].Value = dataQuery.PageInfo.CurrentPage;
            parms[6].Value = (int)PageHelper.StatType.Stat;

            parms[7].Direction = ParameterDirection.Output;
            parms[8].Direction = ParameterDirection.Output;

            DataTable table = new DataTable();
            table.Columns.Add("HotelId");
            table.Columns.Add("CityName");
            table.Columns.Add("HotelName");
            table.Columns.Add("IsActive");
            table.Columns.Add("State");
            using (SqlDataReader reader = DbHelperSQL.RunProcedure(PageHelper.PROC_PAGE, parms))
            {
                DataRow row = null;
                while (reader.Read())
                {
                    row = table.NewRow();
                    row["HotelId"] = reader.GetInt32(1);
                    row["CityName"] = reader.GetString(2);
                    row["HotelName"] = reader.GetString(3);
                    row["IsActive"] = reader.GetInt32(4);
                    row["State"] = reader.GetInt32(5);
                    table.Rows.Add(row);
                }
            }
            dataQuery.PageInfo.TotalCount = (int)parms[7].Value;
            return table;

        }

        /// <summary>
        /// 获取上线的酒店总数
        /// </summary>
        public int GetActiveCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hpm_hotel where active=1");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 酒店是否上线
        /// </summary>
        public bool IsActive(int hotelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top 1 1 from hpm_hotel where active=1 and hotelid={0}", hotelId);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return obj == null ? false : (Convert.ToInt32(obj)==1?true:false);
        }
    }
}

