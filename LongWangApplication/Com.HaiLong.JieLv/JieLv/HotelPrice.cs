using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    /// <summary>
    ///  酒店价格实体类 
    /// </summary>
    [Serializable]
    public partial class HotelPrice
    {
        /// <summary>
        /// 酒店ID
        /// </summary>
        public int hotelId { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string hotelName { get; set; }
        /// <summary>
        /// 酒店编号
        /// </summary>
        public string hotelCd { get; set; }
        /// <summary>
        /// 房型ID
        /// </summary>
        public int roomtypeId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string roomtypeName { get; set; }
        /// <summary>
        /// 入住日期至退房日期内，每天的具体信息
        /// </summary>
        public List<RoomPriceDetail> roomPriceDetail { get; set; }
    }
}
