using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Com.HaiLong.Lib.DBUtility;
using System.Xml.Serialization;

namespace Com.HaiLong.Provider.Qunar
{
    [Serializable]
    [XmlType("hotel")]
    public class Hotel
    {
        /// <summary>
        /// 酒店Id
        /// </summary>
         [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// 酒店城市
        /// </summary>
        [XmlAttribute("city")]
        public string City { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        ///  酒店地址
        /// </summary>
        [XmlAttribute("address")]
        public string Address { get; set; }
        /// <summary>
        /// 酒店联系电话
        /// </summary>
       [XmlAttribute("tel")]
        public string Tel { get; set; }
        /// <summary>
        /// 优惠信息
        /// </summary>
       [XmlAttribute("promotion")]
        public string Promotion { get; set; }

        #region 获取有效酒店
        /// <summary>
        /// 获取有效酒店
        /// </summary>
        /// <returns></returns>
        public HotelList GetHotels()
        {
            HotelList list = new HotelList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   h.hotelid,c.qunarcitycode,h.namechn,h.addresschn, h.centraltel,'' as promotion from hpm_hotel h inner join jltour_city c on h.city=c.cityid ");
            //strSql.Append(" where  h.country in ('70007','70008','70009') and h.active=1 and h.hoteltype in(1,2) and c.qunarcitycode is not null ");
            //strSql.Append(" and  h.centraltel is not null and h.centraltel!='' and h.addresschn is not null and h.addresschn !='' ");
            strSql.Append("where    h.active=1 ");
            using (SqlDataReader rdr = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (rdr.Read())
                {
                    list.Add(ReaderBind(rdr));
                }
            }
            return list;
        }
        #endregion

        #region 对象实体绑定数据
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        private Hotel ReaderBind(IDataReader rdr)
        {
            var model = new Hotel
            {
               Id=rdr.GetInt32(0).ToString(),
               City=rdr.GetString(1),
               Name=rdr.GetString(2),
               Address=rdr.GetString(3),
               Tel=rdr.GetString(4),
               Promotion=rdr.GetString(5)
            };
            return model;
        }
        #endregion
    }
}

