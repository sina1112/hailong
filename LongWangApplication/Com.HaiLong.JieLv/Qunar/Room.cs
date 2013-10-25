using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Com.HaiLong.Provider.Qunar
{
     [Serializable]
     [XmlType("room")]
    public class Room
    {
        /// <summary>
        /// 房间Id
        /// </summary>
        [XmlAttribute("id")]
         public string Id { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// 早餐
        /// </summary>
        [XmlAttribute("breakfast")]
        public string Breakfast { get; set; }
        /// <summary>
        /// 床型
        /// </summary>
        [XmlAttribute("bed")]
        public string Bed { get; set; }
        /// <summary>
        /// 宽带
        /// </summary>
        [XmlAttribute("broadband")]
        public string Broadband { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        [XmlAttribute("prepay")]
        public string Prepay { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        [XmlAttribute("prices")]
        public string Prices { get; set; }
        /// <summary>
        /// 房态
        /// </summary>
        [XmlAttribute("status")]
        public string Status { get; set; }
        /// <summary>
        /// 房量
        /// </summary>
        [XmlAttribute("counts")]
        public string Counts { get; set; }
        /// <summary>
        /// 连住
        /// </summary>
        [XmlAttribute("last")]
        public string Last { get; set; }
        /// <summary>
        /// 提前预订
        /// </summary>
        [XmlAttribute("advance")]
        public string Advance { get; set; }
        /// <summary>
        /// 房量不足拒绝预定
        /// </summary>
        [XmlAttribute("refusestate")]
        public string Refusestate { get; set; }
    }
}
