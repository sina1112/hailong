using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.HaiLong.Provider.Qunar;
using System.Text;
using Com.HaiLong.Lib;
using Com.HaiLong.Lib.Common;
using Com.HaiLong.Provider.JieLv;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using Com.HaiLong.Provider.lw;

namespace LongWangApplication.Ashx
{
    /// <summary>
    /// QunarHandler 的摘要说明
    /// </summary>
    public class QunarHandler : BaseAshx
    {
        string JieLvUrl;
        string Usercd;
        string Authno;
        double CacheTime;
        int changePrice = 30;//默认增加价格
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            try
            {

                string action = base.GetStringParam("action", null, "GET");
                GetSetting();
                context.Response.ContentType = "text/xml";
                switch (action)
                {

                    case "GetHotels":
                        context.Response.Write(GetHotels());
                        break;
                    case "GetPrices":
                        context.Response.Write(GetPricesByCache());
                        break;
                    case "GetChangeHotels":
                        context.Response.Write(GetChangeHotels());
                        break;
                    default:
                        throw new Exception("错误:查询参数不存在！");
                }
            }
            catch (Exception e)
            {
                base.logManager.WriteLog("IP:" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                base.logManager.WriteLog(e.Message);
                base.logManager.WriteLog(e.ToString());
                context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?><hotel />");
                context.Response.End();
            }
        }
        /// <summary>
        /// 获取节点配置
        /// </summary>
        private void GetSetting()
        {
            JieLvUrl = ConfigHelper.GetConfigString("JieLvUrl");
            Usercd = ConfigHelper.GetConfigString("Usercd");
            Authno = ConfigHelper.GetConfigString("Authno");
            CacheTime = ConfigHelper.GetConfigDouble("CacheString");
            if (String.IsNullOrWhiteSpace(JieLvUrl))
            {
                throw new Exception("配置节点错误：‘JieLvUrl’配置节不存在。");
            }
            else if (String.IsNullOrWhiteSpace(Usercd))
            {
                throw new Exception("配置节点错误：‘Usercd’配置节不存在。");
            }
            else if (String.IsNullOrWhiteSpace(Authno))
            {
                throw new Exception("配置节点错误：‘Authno’配置节不存在。");
            }
        }
        /// <summary>
        /// 获取全量酒店
        /// </summary>
        /// <returns></returns>
        public string GetHotels()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><hotel/>";
            var h = new Com.HaiLong.Provider.Qunar.Hotel();
            HotelList list = h.GetHotels();
            if (list.Count > 0)
            {
                xml = XmlHelper.XmlSerialize(list, Encoding.UTF8);
            }
            return xml;
        }
        /// <summary>
        /// 获取缓存的价格
        /// </summary>
        /// <returns></returns>
        public string GetPricesByCache()
        {
            try
            {
                string hotelId = base.GetStringParam("hotelId", null, "GET");
                DateTime fromDate = DateTime.Parse(base.GetStringParam("fromDate", null, "GET"));
                DateTime toDate = DateTime.Parse(base.GetStringParam("toDate", null, "GET"));
                string cacheName = string.Format("{0}_{1}_{2}", hotelId, fromDate.ToShortDateString(), toDate.ToShortDateString());
                object objModel = DataCache.GetCache(cacheName);

                if (objModel == null)
                {
                    //try
                    //{
                    objModel = GetPrices(hotelId, fromDate, toDate);
                    if (objModel != null)
                    {
                        DataCache.SetCache(cacheName, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                    //}
                    //catch { }
                }
                return objModel.ToString();
            }
            catch (Exception e)
            {
                string hotelId = base.GetStringParam("hotelId", null, "GET");
                DateTime fromDate = DateTime.Parse(base.GetStringParam("fromDate", null, "GET"));
                DateTime toDate = DateTime.Parse(base.GetStringParam("toDate", null, "GET"));
                string cacheName = string.Format("{0}_{1}_{2}", hotelId, fromDate.ToShortDateString(), toDate.ToShortDateString());
                object objModel = DataCache.GetCache(cacheName);
                throw new Exception("cacheName:" + cacheName + " Message:" + e.Message);
            }



        }
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>
        public string GetPrices(string hotelId, DateTime fromDate, DateTime toDate)
        {
            //Stopwatch stwt = new Stopwatch();
            //stwt.Start();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><hotel><rooms></rooms></hotel>";
            int hId = int.Parse(hotelId);
            Com.HaiLong.Provider.JieLv.Hotel hotel = new Com.HaiLong.Provider.JieLv.Hotel();
            bool isActive = hotel.IsActive(hId);
            if (isActive)
            {
                int requestCount = 0;//总请求数
                int controlCount = 60;//控制获取数据的天数
                int day = Math.Abs(((TimeSpan)(toDate - fromDate)).Days);
                day = day > 90 ? 90 : day;//捷旅查询最多支持三个月的数据
                if (day > 0)
                {
                    requestCount = (day + 30 - 1) / 30;
                    ResponseHotelPriceInfo info = new ResponseHotelPriceInfo();
                    for (int i = 0; i < requestCount; i++)
                    {
                        DateTime startDate = fromDate.AddDays(30 * i);
                        DateTime endDate = fromDate.AddDays(30 * (i + 1));
                        endDate = endDate > toDate ? toDate : endDate;

                        string checkInDateStr = startDate.ToString("yyyy-MM-dd");
                        string checkOutDateStr = endDate.ToString("yyyy-MM-dd");

                        String message = String.Format("{{'Usercd':'{0}','Authno':'{1}' ,'hotelIds':'{2}','checkInDate':'{3}','checkOutDate':'{4}','QueryType':'hotelpriceall'}}", Usercd, Authno, hotelId, checkInDateStr, checkOutDateStr);//QueryType:hotelpriceall表示申请+即时确认；hotelpricecomfirm表示只要即时确认
                        //到捷旅接口查询相关数据
                        string output = HttpWebResponseUtility.PostJson(JieLvUrl, message);
                        ResponseHotelPriceInfo tempInfo = (ResponseHotelPriceInfo)JsonConvert.DeserializeObject(output, typeof(ResponseHotelPriceInfo));
                        if (tempInfo.Success == (int)JieLvEnum.ResponseType.SUCCESS)
                        {
                            info.Success = tempInfo.Success;

                            tempInfo.Data.ForEach(x => UnionHotelRoom(info.Data, x));
                        }
                        else
                        {
                            throw new Exception("捷旅接口返回错误：" + tempInfo.Msg + "<br/>");
                        }

                    }
                    if (info != null && info.Data.Count > 0)
                    {
                        Com.HaiLong.Provider.JieLv.Hotel jHotel = new Com.HaiLong.Provider.JieLv.Hotel();
                        DataTable jdt = jHotel.GetList(hId);//查询酒店信息
                        HasRoomHotel rh = new HasRoomHotel();
                        #region 酒店数据赋值
                        rh.Id = hotelId.ToString();
                        rh.City = jdt.Rows[0][0].ToString();
                        rh.Name = info.Data[0].hotelName;
                        rh.Address = jdt.Rows[0][1].ToString();
                        rh.Tel = jdt.Rows[0][2].ToString();
                        #endregion

                        List<int> rids = new List<int>();//房型ID集合
                        Com.HaiLong.Provider.JieLv.RoomType rt = new Com.HaiLong.Provider.JieLv.RoomType();

                        foreach (var hp in info.Data)
                        {
                            rids.Add(hp.roomtypeId);

                        }
                        RoomPrice rp = new RoomPrice();
                        RoomState rs = new RoomState();
                        DataTable roompriceDt = rp.GetListByHotelId(hotelId, fromDate, toDate);//获取控制的房价数据
                        DataTable roomstateDt = rs.GetListByHotelId(hotelId, fromDate, toDate);//获取控制的房态数据
                        DataTable dt = rt.GetList(rids);//获取房型数据    
                        
                        foreach (var hotelPrice in info.Data)
                        {
                            var beddt = dt.Select("roomtypeid=" + hotelPrice.roomtypeId.ToString());
                            if (beddt.Count() < 1)//数据库中不存在的房型舍弃
                            {
                                continue;
                            }
                            string state = beddt[0]["state"].ToString();//省份编码
                            string city = beddt[0]["city"].ToString();//城市编码
                            var tempDetail = hotelPrice.roomPriceDetail;
                            var groupRateType = tempDetail.GroupBy(p => p.ratetype).Select(g => (new {ratetype = g.Key }));//获取价格类型分组
                            foreach (var rtobject in groupRateType)
                            {
                                int tempRateType = rtobject.ratetype;
                                if(!ValidRateType(tempRateType.ToString()))//验证房型
                                continue;
                                List<string> breakfasts = new List<string>();//早餐
                                List<string> prices = new List<string>();//价格
                                List<string> status = new List<string>();//房态
                                List<string> counts = new List<string>();//房间数
                                string broadband = "0";//宽带
                                string advance = "0";//提前预订
                                string last = "0";//连住天数
                                for (int i = 0; i < day; i++)
                                {
                                    if (i < controlCount)
                                    {
                                        DateTime night = fromDate.AddDays(i);

                                        var details = tempDetail.Where(a => (a.night.ToShortDateString() == night.ToShortDateString())&&(a.ratetype==tempRateType)).Take(1);
                                        if (details.Count() > 0)
                                        {
                                            foreach (var detail in details)
                                            {
                                                int? rprice = null;
                                                string rstate = "";
                                                int count = 0;
                                                var drsprice = roompriceDt.Select(string.Format("roomtypeid='{0}' and ratetype={1}  and night='{2}' and includebreakfastqty2='{3}'", detail.roomtypeid.ToString(),detail.ratetype, detail.night.ToShortDateString(), detail.includebreakfastqty2.GetValueOrDefault(10)));
                                                var drsstate = roomstateDt.Select(string.Format("roomtypeid='{0}' and ratetype in (-1,{1}) and night='{2}' and (includebreakfastqty2='{3}' or includebreakfastqty2='-1')", detail.roomtypeid.ToString(),detail.ratetype, detail.night.ToShortDateString(), detail.includebreakfastqty2.GetValueOrDefault(10)));

                                                if (drsprice.Count() > 0)//过滤房价数据
                                                {
                                                    var drp = drsprice[0];
                                                    rprice = drp.IsNull(3) ? rprice : int.Parse(drp[3].ToString());
                                                }

                                                if (drsstate.Count() > 0 && (!drsstate[0].IsNull(3)))//过滤房态数据
                                                {
                                                    var drs = drsstate[0];
                                                    rstate = this.ChangeLWStatus(int.Parse(drs[3].ToString()));
                                                    if (!drs.IsNull(3) && int.Parse(drs[3].ToString()) == 1)//房态为龙网控制的确认有房，调整房间数为5
                                                    {
                                                        count = 5;
                                                    }
                                                    else
                                                    {
                                                        count = detail.qtyable.GetValueOrDefault(0);
                                                    }
                                                }
                                                else
                                                {
                                                    if ((state == "70001" || state == "70050") && (detail.qtyable.GetValueOrDefault(-1) < 1))//港澳非立即确认的，全部设为满房
                                                    {
                                                        rstate = "1";
                                                        count = 0;
                                                    }
                                                    else
                                                    {
                                                        rstate = this.ChangeStatus(detail.qtyable.GetValueOrDefault(-1));
                                                        count = detail.qtyable.GetValueOrDefault(0);
                                                    }
                                                }
                                                count = count < 0 ? 0 : count;
                                                status.Add(rstate);
                                                counts.Add(count.ToString());

                                                string bf = ChangeBreakFast(detail.includebreakfastqty2);
                                                breakfasts.Add(bf);
                                                broadband = this.ChangeBroadband(detail.netcharge);

                                                string pstr = this.ChangePrices(detail.preeprice.GetValueOrDefault(0), rprice, state);
                                                prices.Add(pstr);
                                                advance = this.ChangeAdvance(detail.ratetype.ToString());
                                                last = this.ChangeLast(detail.ratetype.ToString());


                                            }
                                        }
                                        else
                                        {
                                            breakfasts.Add("0");
                                            prices.Add("0");
                                            status.Add("1");
                                            counts.Add("0");

                                        }
                                    
                                    }
                                    else
                                    {
                                        breakfasts.Add("0");
                                        prices.Add("0");
                                        status.Add("1");
                                        counts.Add("0");
                                    }
                                }
                                string bed = "0";

                                if (beddt.Count() > 0)
                                {
                                    bed = this.ChangeBed(beddt[0][1].ToString());
                                }
                                Room room = new Room
                                {
                                    Id = string.Format("{0}_{1}", hotelPrice.roomtypeId.ToString(), tempRateType),
                                    Name = FormatRoomTypeName(city,hotelPrice.roomtypeName,prices),
                                    Breakfast = string.Join("|", breakfasts),
                                    Bed = bed,
                                    Broadband = broadband,
                                    Prepay = "0",
                                    Prices = string.Join("|", prices),
                                    Status = string.Join("|", status),
                                    Counts = string.Join("|", counts),
                                    Refusestate = "0", //申请房
                                    Advance = advance,
                                    Last = last

                                };

                                rh.rooms.Add(room);
                            }

                        }

                        xml = XmlHelper.XmlSerialize(rh, Encoding.UTF8);


                    }
                }
            }
            return xml;

        }

        //获取变化的酒店数据
        public string GetChangeHotels()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><hotel/>";
            long lastupdate = base.GetLongParam("lastupdate", null, "GET");
            TimeSpan timespan = DateTime.Now - new DateTime(1970, 1, 1);
            long interval = (timespan.Ticks) / (long)(Math.Pow(10, 7));

            string cacheName = "ChangeHotelDate";
            string cacheXml = "ChangeCacheXml";
            object objModel = DataCache.GetCache(cacheName);
            object objXML = DataCache.GetCache(cacheXml);
            if (lastupdate == 0)
            {
                var h = new Com.HaiLong.Provider.Qunar.Hotel();
                HotelList hotelList = h.GetHotels();
                ChangeHotelList chlist = new ChangeHotelList();
                ChangeHotels chs = new ChangeHotels();
                foreach (var hotel in hotelList)
                {
                    chs.Add(new ChangeHotel()
                    {
                        Id = hotel.Id,
                        UpdateTime = interval.ToString()
                    });

                }
                chlist.Add(chs);
                if (chs.Count > 0)
                {
                    xml = XmlHelper.XmlSerialize(chlist, Encoding.UTF8);
                }
            }
            else
            {
                if (objXML == null)
                {
                    var h = new Com.HaiLong.Provider.Qunar.Hotel();
                    HotelList hotelList = h.GetHotels();
                    ChangeHotelList chlist = new ChangeHotelList();
                    ChangeHotels chs = new ChangeHotels();
                    foreach (var hotel in hotelList)
                    {
                        chs.Add(new ChangeHotel()
                        {
                            Id = hotel.Id,
                            UpdateTime = interval.ToString()
                        });

                    }
                    chlist.Add(chs);
                    if (chs.Count > 0)
                    {
                        xml = XmlHelper.XmlSerialize(chlist, Encoding.UTF8);
                    }

                    objModel = interval;
                    objXML = xml;
                    if (objModel != null)
                    {
                        DataCache.SetCache(cacheName, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                    if (objXML != null)
                    {
                        DataCache.SetCache(cacheXml, xml, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                else
                    xml = objXML.ToString();
            }

            return xml;

        }
        /// <summary>
        /// 酒店房型合集
        /// </summary>
        /// <param name="parentHp"></param>
        /// <param name="hp"></param>
        private void UnionHotelRoom(List<HotelPrice> parentHp, HotelPrice hp)
        {
            var data = parentHp.Where<HotelPrice>(x => x.roomtypeId == hp.roomtypeId).ToList();
            if (data.Count() == 1)
            {
                data[0].roomPriceDetail.AddRange(hp.roomPriceDetail);
            }
            else
            {
                parentHp.Add(hp);
            }

        }

        /// <summary>
        /// 格式化房型名称
        /// </summary>
        /// <param name="roomTypeName"></param>
        /// <param name="jieLvPrices"></param>
        /// <returns></returns>
        private string FormatRoomTypeName(string city,string roomTypeName,List<string> jieLvPrices)
        {
            string formatName = roomTypeName;
            if (city == "70030")//三亚
            {
                bool mark = true;
                foreach (var p in jieLvPrices)
                {
                    var price = double.Parse(p);
                    if (price < 1000 && price != 0)//所有价格都大于1000才标记
                        mark = false;
                }
                if (mark)
                    formatName =  string.Format("{0}({1})", roomTypeName, "三晚起赠送价值200元的接机服务");
            }
            return formatName;
           
        
        }
        /// <summary>
        /// 转换捷旅早餐类型为Qunar类型
        /// </summary>
        /// <param name="jieLvBreakFast"></param>
        /// <returns></returns>
        private string ChangeBreakFast(int? jieLvBreakFast)
        {
            string qunarBreakFast = "0";
            switch (jieLvBreakFast)
            {
                case 10://不含
                    qunarBreakFast = "0";
                    break;
                case 11://1份中早
                    qunarBreakFast = "1";
                    break;
                case 12://1份西早
                    qunarBreakFast = "1";
                    break;
                case 13://1份自助
                    qunarBreakFast = "1";
                    break;
                case 21://2份中早
                    qunarBreakFast = "2";
                    break;
                case 22://2份西早
                    qunarBreakFast = "2";
                    break;
                case 23://2份自助
                    qunarBreakFast = "2";
                    break;
                case 31://3份中早
                    qunarBreakFast = "3";
                    break;
                case 32://3份西早
                    qunarBreakFast = "3";
                    break;
                case 33://3份自助
                    qunarBreakFast = "3";
                    break;
                case 34://床位早
                    qunarBreakFast = "-1";
                    break;
                case 35://4份自助
                    qunarBreakFast = "4";
                    break;
                case 36://5份自助
                    qunarBreakFast = "5";
                    break;
                case 37://6份自助
                    qunarBreakFast = "6";
                    break;
                case 38://7份自助
                    qunarBreakFast = "7";
                    break;
                case 39://1份早晚自助
                    qunarBreakFast = "1";
                    break;
                case 40://2份早晚自助
                    qunarBreakFast = "2";
                    break;
                default://含早，数量不定
                    qunarBreakFast = "-1";
                    break;
            }
            return qunarBreakFast;


        }

        /// <summary>
        /// 转换捷旅床型为Qunar床型
        /// </summary>
        /// <param name="jieLvBed"></param>
        /// <returns></returns>
        private string ChangeBed(string jieLvBed)
        {
            string qunarBed = "0";
            switch (jieLvBed)
            {
                case "single"://单床
                    qunarBed = "5";
                    break;
                case "double"://双床
                    qunarBed = "1";
                    break;
                case "big"://大床
                    qunarBed = "0";
                    break;
                case "cir"://圆床
                    qunarBed = "10";
                    break;
                case "sindou"://单床/双床
                    qunarBed = "4";
                    break;
                case "bigdou"://大床/双床
                    qunarBed = "2";
                    break;
                case "bigsing"://大床/单床
                    qunarBed = "0";
                    break;

            }
            return qunarBed;


        }
        /// <summary>
        /// 验证捷旅房型
        /// </summary>
        /// <param name="jieLvRatetype"></param>
        /// <returns></returns>
        private bool ValidRateType(string jieLvRatetype)
        {
            bool isPass = false;
            switch (jieLvRatetype)
            {
                case "1"://常规
                case "9"://含双早
                case "16"://含单早
                case "17"://不含早
                case "23"://连住2晚及以上
                case "37": //提前5天预订
                case "41"://单人入住
                case "42"://双人入住
                case "56"://含早
                case "57": //提前7天预订
                case "58": //提前10天预订
                //case "120": //限当天预订
                case "188": //提前3天预订
                case "222": //提前1天预订
                case "319"://连住3晚及以上
                case "327": //提前2天预订
                case "334": //大床
                case "354": //提前7天预订含单早
                case "420": //双床
                case "434": //提前7天预订含双早
                case "449": //连住3晚含单早
                case "450": //连住3晚含双早
                case "525": //提前1天预订不含早
                case "565": //提前14天预订
                case "567": //提前30天预订
                case "617": //限五六日连住2晚及以上
                case "660": //提前1天预订含双早
                case "721": //提前21天预订
                case "755": //含床位早
                case "757": //提前15天预订
                case "758": //三人入住
                case "759": //四人入住
                case "760": //五人入住
                case "761": //六人入住
                case "764": //提前60天预订
                case "771": //连住3晚
                case "772": //连住2晚及以上含单早
                case "773": //连住2晚及以上含双早
                case "786": //大床提前7天
                case "787": //双床提前7天
                    isPass = true;
                    break;
            }
            return isPass;
        
        }
        /// <summary>
        /// 转换捷旅价格类型为Qunar提前预订天数
        /// </summary>
        /// <param name="jieLvRatetype"></param>
        /// <returns></returns>
        private string ChangeAdvance(string jieLvRatetype)
        {
            string advance = "0";
            switch (jieLvRatetype)
            {
                case "37": //提前5天预订
                    advance = "5";
                    break;
                case "57": //提前7天预订
                    advance = "7";
                    break;
                case "58": //提前10天预订
                    advance = "10";
                    break;
                case "188": //提前3天预订
                    advance = "3";
                    break;
                case "222": //提前1天预订
                    advance = "1";
                    break;
                case "327": //提前2天预订
                    advance = "2";
                    break;
                case "354": //提前7天预订含单早
                    advance = "7";
                    break;
                case "434": //提前7天预订含双早
                    advance = "7";
                    break;
                case "525": //提前1天预订不含早
                    advance = "1";
                    break;
                case "565": //提前14天预订
                    advance = "14";
                    break;
                case "567": //提前30天预订
                    advance = "30";
                    break;
                case "660": //提前1天预订含双早
                    advance = "1";
                    break;
                case "721": //提前21天预订
                    advance = "21";
                    break;
                case "757": //提前15天预订
                    advance = "15";
                    break;
                case "764": //提前60天预订
                    advance = "60";
                    break;
                case "786": //大床提前7天
                    advance = "7";
                    break;
                case "787": //双床提前7天
                    advance = "7";
                    break;
            }
            return advance;
        }
        /// <summary>
        /// 转换捷旅价格类型为Qunar连住天数
        /// </summary>
        /// <param name="jieLvRatetype"></param>
        /// <returns></returns>
        private string ChangeLast(string jieLvRatetype)
        {
            string last = "0";
            switch (jieLvRatetype)
            {
                case "23"://连住2晚及以上
                case "617": //限五六日连住2晚及以上
                case "772": //连住2晚及以上含单早
                case "773": //连住2晚及以上含双早
                    last = "2";
                    break;
                case "319"://连住3晚及以上
                case "449": //连住3晚含单早
                case "450": //连住3晚含双早
                case "771": //连住3晚
                    last = "3";
                    break;

            }
            return last;
        
        }
        /// <summary>
        /// 转换捷旅宽带类型为Qunar宽带类型
        /// </summary>
        /// <param name="jieLvBroadband"></param>
        /// <returns></returns>
        private string ChangeBroadband(int? jieLvBroadband)
        {
            string qunarBroadband = "0";
            if (jieLvBroadband == null)//收费，金额未定
            {
                qunarBroadband = "3";
            }
            else if (jieLvBroadband == -1)
            {
                qunarBroadband = "3";
            }
            else if (jieLvBroadband == 0)//免费
            {
                qunarBroadband = "2";
            }
            else if (jieLvBroadband > 0)//上网费用
            {
                qunarBroadband = "3";
            }

            return qunarBroadband;


        }

        /// <summary>
        /// 转换捷旅价格为Qunar价格
        /// </summary>
        /// <param name="jieLvPrice"></param>
        /// <param name="changePrice"></param>
        /// <param name="state">省份</param>
        /// <returns></returns>
        private string ChangePrices(double jieLvPrice, int? customChangePrice, string state)
        {
            string price = "0";
            int jlPrice = Convert.ToInt32(Math.Ceiling(jieLvPrice));

            if (customChangePrice == null)//未自定义增减价
            {

                if (state == "70023")//海南
                {
                    price = (jlPrice + changePrice).ToString();
                }
                else
                {
                    if (jlPrice > 0 && jlPrice <= 800)
                        price = (jlPrice + changePrice).ToString();
                    else if (jlPrice > 800)
                        price = (jlPrice + 50).ToString();

                    if (jlPrice > 6000)
                        price = (jlPrice + 200).ToString();
                    else if (jlPrice > 4000 && jlPrice <= 6000)
                        price = (jlPrice + 150).ToString();
                    else if (jlPrice > 2500 && jlPrice <= 4000)
                        price = (jlPrice + 100).ToString();
                    else if (jlPrice > 800 && jlPrice <= 2500)
                        price = (jlPrice + 50).ToString();
                    else if (jlPrice > 0 && jlPrice <= 800)
                        price = (jlPrice + changePrice).ToString();
                }
            }
            else
            {
                price = (jlPrice + customChangePrice).ToString();

            }


            return price;

        }

        /// <summary>
        /// 转换捷旅房态为Qunar房态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string ChangeStatus(int status)
        {
            string s = status >= 0 ? "0" : "1";


            return s;

        }

        /// <summary>
        /// 转换龙网房态为Qunar房态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string ChangeLWStatus(int status)
        {
            string s = "1";
            if (status != -1)
                s = "0";
            return s;

        }
    }
}