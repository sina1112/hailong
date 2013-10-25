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
    [XmlRoot("list")]
    public class HotelList:List<Hotel>
    {
        
    }
}
