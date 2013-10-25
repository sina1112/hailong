using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Com.HaiLong.Provider.Qunar
{
    /// <summary>
    /// 带房间的酒店类
    /// </summary>
    [XmlRoot("hotel")]
    public class HasRoomHotel:Hotel
    {
        public List<Room> rooms = new List<Room>();
    }
}
