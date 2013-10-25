using System;
using System.Data;
using System.Text;
using Com.HaiLong.Lib.DBUtility;
using System.Collections.Generic;
namespace Com.HaiLong.Provider.JieLv
{
	/// <summary>
    /// RoomType:房型实体类
	/// </summary>
	[Serializable]
	public partial class RoomType
	{
        public RoomType()
		{}
		#region Model
		private int _roomtypeid;
		private string _namechn;
        private int? _basetype;
        private int? _roomqty;
        private int? _bedqty;
		private string _bedtype;
		private string _bedsize;
        private int? _allowaddbed;
        private int? _allowaddbedqty;
		private string _allowaddbedsize;
        private int? _nosm;
		private string _floordistribution;
        private int? _internet;
		private string _roomfacilities;
		private string _remark;
		private string _remark2;
		private string _acreages;
        private DateTime? _createtime;
        private DateTime? _updatetime;
		private int _active;
		/// <summary>
		/// 房型编号
		/// </summary>
		public int roomtypeid
		{
			set{ _roomtypeid=value;}
			get{return _roomtypeid;}
		}

		/// <summary>
		/// 客房中文名称
		/// </summary>
		public string namechn
		{
			set{ _namechn=value;}
			get{return _namechn;}
		}
		/// <summary>
		/// 是否基础房型
		/// </summary>
        public int? basetype
		{
			set{ _basetype=value;}
			get{return _basetype;}
		}
		/// <summary>
		/// 客房面积
		/// </summary>
        public string acreages
		{
			set{ _acreages=value;}
			get{return _acreages;}
		}
		/// <summary>
		/// 房间数量
		/// </summary>
        public int? roomqty
		{
			set{ _roomqty=value;}
			get{return _roomqty;}
		}
		/// <summary>
		/// 房间床数量
		/// </summary>
        public int? bedqty
		{
			set{ _bedqty=value;}
			get{return _bedqty;}
		}
		/// <summary>
		/// 床型
		/// </summary>
		public string bedtype
		{
			set{ _bedtype=value;}
			get{return _bedtype;}
		}
		/// <summary>
		/// 房间床尺寸
		/// </summary>
		public string bedsize
		{
			set{ _bedsize=value;}
			get{return _bedsize;}
		}
		/// <summary>
		/// 是否允许加床
		/// </summary>
        public int? allowaddbed
		{
			set{ _allowaddbed=value;}
			get{return _allowaddbed;}
		}
		/// <summary>
		/// 加床数量
		/// </summary>
        public int? allowaddbedqty
		{
			set{ _allowaddbedqty=value;}
			get{return _allowaddbedqty;}
		}
		/// <summary>
		/// 允许加床尺寸
		/// </summary>
		public string allowaddbedsize
		{
			set{ _allowaddbedsize=value;}
			get{return _allowaddbedsize;}
		}
		/// <summary>
		/// 该房型有无烟房
		/// </summary>
        public int? nosm
		{
			set{ _nosm=value;}
			get{return _nosm;}
		}
		/// <summary>
		/// 房型分布在多少层
		/// </summary>
		public string floordistribution
		{
			set{ _floordistribution=value;}
			get{return _floordistribution;}
		}
		/// <summary>
		/// 宽带还是拨号
		/// </summary>
        public int? internet
		{
			set{ _internet=value;}
			get{return _internet;}
		}

		/// <summary>
		/// 房间设施
		/// </summary>
		public string roomfacilities
		{
			set{ _roomfacilities=value;}
			get{return _roomfacilities;}
		}
		/// <summary>
		/// 信息备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 房型设施
		/// </summary>
		public string remark2
		{
			set{ _remark2=value;}
			get{return _remark2;}
		}


		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime? createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
        public DateTime? updatetime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 是否生效
		/// </summary>
		public int active
		{
			set{ _active=value;}
			get{return _active;}
		}
		#endregion Model

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(List<int> roomtypeids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT r.roomtypeid,r.bedtype,h.state,h.city ");
            strSql.Append(" FROM hpm_roomtype r inner join hpm_hotel h on h.hotelid = r.hotelid");
            strSql.AppendFormat(" where  r.roomtypeid in('{0}')", string.Join("','", roomtypeids));
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT r.roomtypeid,r.bedtype ");
            strSql.Append(" FROM hpm_hotel h  inner join hpm_roomtype r on h.hotelid=r.hotelid");
            strSql.AppendFormat(" where  h.hotelid='{0}' and r.active=1", hotelid);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList2(string hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select roomtypeid,namechn from hpm_roomtype where hotelid={0} and active=1 ", hotelid);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList3(string hotelids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select hotelid,roomtypeid from hpm_roomtype where hotelid in ({0}) and active=1 ", hotelids);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
	}
}

