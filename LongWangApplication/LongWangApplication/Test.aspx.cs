using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Com.HaiLong.Lib.Common;
using Newtonsoft.Json;
using Com.HaiLong.Provider.JieLv;
using System.Text;
using Com.HaiLong.Lib;
using Com.HaiLong.Provider;
using Com.HaiLong.Provider.Qunar;
using System.Data;
using System.Diagnostics;
namespace LongWangApplication
{
    public partial class Test : System.Web.UI.Page
    {
        string JieLvUrl;
        string Usercd;
        string Authno;
        string CityUrl;
        protected void Page_Load(object sender, EventArgs e)
        {

            GetSetting();
            //GetHotelPrice();
            //int interval = 1356420934;
            //DateTime t = new DateTime(1970, 1, 1).AddSeconds(interval);

            //long lastupdate = 1378674673;
            //long cacheTime = 60*1;
            //DateTime t1 = DateTime.Parse("2013-09-08 21:15:30");
            //DateTime t2 = DateTime.Parse("2013-09-08 21:15:59");

            //TimeSpan timespan1 = t1 - new DateTime(1970, 1, 1);
            //TimeSpan timespan2 = t2 - new DateTime(1970, 1, 1);

            //long interval1 = (timespan1.Ticks)/(long)(Math.Pow(10,7));
            //long interval2 = (timespan2.Ticks) / (long)(Math.Pow(10, 7));
            //Response.Write("interval2-interval1:" + (interval2 - interval1).ToString() + "<br />");
            if (!IsPostBack)
            {
                this.fromDate.Text = DateTime.Now.ToShortDateString();
                this.toDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
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
            CityUrl = ConfigHelper.GetConfigString("CityUrl");
            if (String.IsNullOrWhiteSpace(JieLvUrl))
            {
                Response.Write("配置节点错误：‘JieLvUrl’配置节不存在。");
                Response.End();
            }
            else if (String.IsNullOrWhiteSpace(Usercd))
            {
                Response.Write("配置节点错误：‘Usercd’配置节不存在。");
                Response.End();
            }
            else if (String.IsNullOrWhiteSpace(Authno))
            {
                Response.Write("配置节点错误：‘Authno’配置节不存在。");
                Response.End();
            }
            else if (String.IsNullOrWhiteSpace(CityUrl))
            {
                Response.Write("配置节点错误：‘CityUrl’配置节不存在。");
                Response.End();
            }
        }
        /// <summary>
        /// 获取酒店、房型信息(捷旅接口目前仍有问题，传的ID值若其中一个不存在，则直接返回错误。)
        /// </summary>
        private void GetHotelinfo()
        {
            int currentPage = 1;
            int pageSize = 20;
            int startIndex = 1;
            int endIndex=20;
            int pageCount = 2;//总页数
            for(int i=0;i<pageCount;i++)
            {
                currentPage = i+1;
                startIndex = (currentPage-1)*pageSize+1;
                endIndex = currentPage*pageSize;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < pageSize; j++)
                {
                    sb.Append(startIndex+j);
                    sb.Append("/");
                }
                string hotelIds = sb.ToString().TrimEnd(new char[] { '/'});
                String message = String.Format("{{'Usercd':'{0}','Authno':'{1}' ,'hotelIds':'{2}','QueryType':'hotelinfo'}}", Usercd, Authno, hotelIds);
                string output = HttpWebResponseUtility.PostJson(JieLvUrl, message);
                ResponseHotelInfo hotelInfo = (ResponseHotelInfo)JsonConvert.DeserializeObject(output, typeof(ResponseHotelInfo));
                foreach(var hotel in hotelInfo.Data)
                {
                    Response.Write(hotel.hotelid+":"+hotel.namechn+"<br/>");
                }
                if (hotelInfo.Data.Count <1)
                    return;
                
            }
           

           
            // Response.Write();

        }
        /// <summary>
        /// 获取价格类型，添加到数据库，若存在则跳过
        /// </summary>
        private void GetRateType()
        {
            String message = String.Format("{{'Usercd':'{0}','Authno':'{1}' ,'QueryType':'checkratetype'}}", Usercd, Authno);
            try
            {
                string output = HttpWebResponseUtility.PostJson(JieLvUrl, message);
                ResponseRateTypeInfo info = (ResponseRateTypeInfo)JsonConvert.DeserializeObject(output, typeof(ResponseRateTypeInfo));
                if (info.Success == (int)JieLvEnum.ResponseType.SUCCESS)
                {
                    foreach (var rateType in info.Data)
                    {
                        if (!rateType.Exists(rateType.ratetypeid))
                        {
                            rateType.Add();
                        }
                        Response.Write(rateType.ratetypeid + ":" + rateType.ratetypename + "<br/>");
                    }
                }
                else
                {
                    Response.Write("捷旅接口返回错误："+ info.Msg + "<br/>");
                }
            }
            catch (Exception e)
            {
                Response.Write(e.Message + "<br/>");
            }
        }
        /// <summary>
        /// 获取酒店价格/房态
        /// </summary>
        private void GetHotelPrice()
        {
            int requestCount = 0;//总请求数
            string hotelIds = this.tbhotelId.Text;
            DateTime  checkInDate = DateTime.Parse(this.fromDate.Text);
            DateTime checkOutDate = DateTime.Parse(this.toDate.Text);

            int day = Math.Abs(((TimeSpan)(checkOutDate - checkInDate)).Days);
            requestCount = (day  +  30  - 1) / 30;
            ResponseHotelPriceInfo info = new ResponseHotelPriceInfo();
            try
            {
                for (int i = 0; i < requestCount; i++)
                {
                    DateTime startDate = checkInDate.AddDays(30 * i);
                    DateTime endDate = checkInDate.AddDays(30 * (i + 1));
                    endDate = endDate>checkOutDate?checkOutDate:endDate;

                    string checkInDateStr = startDate.ToString("yyyy-MM-dd");
                    string checkOutDateStr = endDate.ToString("yyyy-MM-dd");

                    String message = String.Format("{{'Usercd':'{0}','Authno':'{1}' ,'hotelIds':'{2}','checkInDate':'{3}','checkOutDate':'{4}','QueryType':'hotelpriceall'}}", Usercd, Authno, hotelIds, checkInDateStr, checkOutDateStr);//QueryType:hotelpriceall表示申请+即时确认；hotelpricecomfirm表示只要即时确认
                    string output = HttpWebResponseUtility.PostJson(JieLvUrl, message);
                    ResponseHotelPriceInfo tempInfo = (ResponseHotelPriceInfo)JsonConvert.DeserializeObject(output, typeof(ResponseHotelPriceInfo));
                    if (tempInfo.Success == (int)JieLvEnum.ResponseType.SUCCESS)
                    {
                        info.Success = tempInfo.Success;

                        tempInfo.Data.ForEach(x=>UnionHotelRoom(info.Data,x));
                    }
                    else
                    {
                        throw new Exception("捷旅接口返回错误：" + tempInfo.Msg + "<br/>");
                    }
                }
                foreach (var hotelPrice in info.Data)
                {
                    Response.Write(hotelPrice.hotelId + ":" + hotelPrice.hotelName + "  roomtypeid: " + hotelPrice.roomtypeId + "<br/>");
                    foreach (var detail in hotelPrice.roomPriceDetail)
                    {
                        Response.Write("  roomtypeid: " + detail.roomtypeid + "  roomtypename: " + detail.roomtypename + " night:" + detail.night + " preeprice:" + detail.preeprice + " businessprice:" + detail.businessprice + " pricingtype:" + detail.pricingtype + " qtyable:" + detail.qtyable.GetValueOrDefault(-1) + " includebreakfastqty2:" + detail.includebreakfastqty2 + " ratetype:" + detail.ratetype + " ratetypename:" + detail.ratetypename + "<br/>");
                    }
                }
               
            }
            catch (Exception e)
            {
                Response.Write(e.Message + "<br/>");
            }
 
        }
        /// <summary>
        /// 酒店房型合集
        /// </summary>
        /// <param name="parentHp"></param>
        /// <param name="hp"></param>
        private void UnionHotelRoom(List<HotelPrice> parentHp, HotelPrice hp)
        {
            var data = parentHp.Where<HotelPrice>(x=>x.roomtypeId==hp.roomtypeId).ToList();
            if (data.Count() == 1)
            {
                data[0].roomPriceDetail.AddRange(hp.roomPriceDetail);
            }
            else
            {
                parentHp.Add(hp);
            }
            
        }
        private void GetCity()
        {
            //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            //xml += "<list> <city countryPy=\"jianada\" countryName=\"加拿大\" name=\"108 Mile Ranch\" code=\"108_mile_ranch\"/>";
            //xml += "<city countryPy=\"aiji\" countryName=\"埃及\" name=\"6th Of October\" code=\"6th_of_october\"/><city countryPy=\"nuowei\" countryName=\"挪威\" name=\"A\" code=\"a\"/>";
            //xml += "</list>";


            Com.HaiLong.Provider.Qunar.City city1 = new Com.HaiLong.Provider.Qunar.City { CountryPy = "aiji", CountryName = "埃及", Name = "108 Mile Ranch", Code = "108_mile_ranch" };
            Com.HaiLong.Provider.Qunar.City city2 = new Com.HaiLong.Provider.Qunar.City { CountryPy = "jianada", CountryName = "加拿大", Name = "6th Of October", Code = "6th_of_october" };
            CityList list = new CityList { city1, city2 };
            string s = XmlHelper.XmlSerialize(list, Encoding.UTF8);
           
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string output = HttpWebResponseUtility.PostJson(CityUrl, string.Empty);
                CityList citys = XmlHelper.XmlDeserialize<CityList>(output, Encoding.UTF8);
                sw.Stop();
                Response.Write("getOutputTime:" + sw.ElapsedMilliseconds + "<br/>");
                sw.Start();
                CityList.BulkToDB(citys);
                sw.Stop();
                Response.Write("insertTime:" + sw.ElapsedMilliseconds+"<br/>");
                Response.Write("初始化Qunar城市数据成功!");
            }
            catch (Exception e)
            {
                
                Response.Write(e.Message + "<br/>");
            }
           
        }
        protected void btnInitRateType_Click(object sender, EventArgs e)
        {
            GetRateType();
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            GetHotelPrice();
        }

        protected void btnGetCity_Click(object sender, EventArgs e)
        {
            GetCity();
           // GetLongWang();
        }
        private void GetLongWang()
        {
            string url = "http://qunar.hiholiday.cn/Hotel/Default.asmx/HotelPriceForQunarSale?";

            url +="UserName=lwtrip&Password=lwtrip@1818&hotelId=1&fromDate=2013-5-15&toDate=2013-5-25";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string output = HttpWebResponseUtility.Get(url);
          //  CityList citys = XmlHelper.XmlDeserialize<CityList>(output, Encoding.UTF8);
            sw.Stop();
            Response.Write("getOutputTime:" + sw.ElapsedMilliseconds + "<br/>");
 
        }
    }
}