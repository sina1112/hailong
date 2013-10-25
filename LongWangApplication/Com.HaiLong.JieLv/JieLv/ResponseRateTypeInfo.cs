using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    public class ResponseRateTypeInfo
    {
        public int Success { get;set;}
        public string Msg { get; set; }
        public List<RateType> Data { get; set; }
    }
}
