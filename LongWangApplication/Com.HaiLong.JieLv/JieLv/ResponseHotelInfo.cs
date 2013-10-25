using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    public class ResponseHotelInfo
    {
        public int Success { get;set;}
        public string Msg { get; set; }
        public List<Hotel> Data { get;set;}
    }
}
