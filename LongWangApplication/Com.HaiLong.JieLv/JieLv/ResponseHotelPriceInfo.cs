using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    public class ResponseHotelPriceInfo
    {
        public ResponseHotelPriceInfo() 
        {
            Data = new List<HotelPrice>();
        }
        public int Success { get;set;}
        public string Msg { get; set; }
        public List<HotelPrice> Data { get;set;}
    }
}
