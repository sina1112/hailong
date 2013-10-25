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
    [XmlRoot("changed")]
    public class ChangeHotelList : List<ChangeHotels>
    {
        
    }

    [Serializable]
    [XmlType("hotels")]
    public class ChangeHotels:List<ChangeHotel>
    {
        
    }

    [Serializable]
    [XmlType("hotel")]
    public class ChangeHotel
    {
        /// <summary>
        /// 酒店Id
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [XmlAttribute("updatetime")]
        public string UpdateTime { get; set; }
    
    }
}
