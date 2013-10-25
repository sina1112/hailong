using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Com.HaiLong.Lib.Common;
using System.Web.SessionState;
using Com.HaiLong.Provider.JieLv;
using System.Data;
using Com.HaiLong.Lib.Common.DataQuery;
using Com.HaiLong.Lib;
using Com.HaiLong.Provider.lw;

namespace LongWangApplication.Ashx
{
    /// <summary>
    /// ManagerHandler 的摘要说明
    /// </summary>
    public class ManagerHandler : BaseAshx, IRequiresSessionState
    {
        string post = "POST";
        string get = "GET";
        public override void ProcessRequest(HttpContext context)
        {

            base.ProcessRequest(context);
            object result = null;//返回结果
            bool isError = false;//是否有错误

            try
            {
                if (context.Request.RequestType == post)
                {
                    string action = base.GetStringParam("action", null, post);
                    switch (action)
                    {
                        case "userLogin":
                            result = UserLogin(context);

                            break;
                        case "getCity":
                            result = GetCity();

                            break;
                        case "getHotelByKey":
                            result = GetHotelByKey();
                            break;
                        case "updateHotelActive":
                            result = UpdateHotelActive();
                            break;
                        case "allHotelUnActive":
                            result = AllHotelUnActive();
                            break;
                        case "getRoomPrice":
                            result = GetRoomPrice();
                            break;
                        case "getRoomType":
                            result = GetRoomType();
                            break;
                        case "updateChanagePrice":
                            result = UpdateChanagePrice();
                            break;
                        case "updateRoomState":
                            result = UpdateRoomState();
                            break;
                        case "batchUpdateRoomState":
                            result = BatchUpdateRoomState();
                            break;
                        case "clearRoomState":
                            result = ClearRoomState();
                            break;
                        case "getActiveHotelCount":
                            result = GetActiveHotelCount();
                            break;
                        default:
                            break;
                    }
                }
                else if (context.Request.RequestType == get)
                {
                    string action = base.GetStringParam("action", null, get);
                    switch (action)
                    {

                        default:
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                isError = true;
                result = e.Message;
            }
            finally
            {
                string returnStr = null;
                var obj = new
                {
                    IsError = isError,
                    Result = result
                };
                returnStr = JsonConvert.SerializeObject(obj);
                context.Response.ContentType = "application/Json";
                context.Response.Write(returnStr);
                context.Response.End();
            }
        }

        /// <summary>
        /// 获取上线酒店总数
        /// </summary>
        /// <returns></returns>
        private object GetActiveHotelCount()
        {
            Hotel hotel = new Hotel();
            return hotel.GetActiveCount();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private object UserLogin(HttpContext context)
        {
            string loginName = base.GetStringParam("userName", null, post);
            string password = base.GetStringParam("passWord", null, post);

            string name = ConfigHelper.GetConfigString("LoginName");
            string pwd = ConfigHelper.GetConfigString("LoginPwd");

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException("配置节点错误：‘LoginName’配置节不存在。");
            }
            else if (String.IsNullOrWhiteSpace(pwd))
            {
                throw new ApplicationException("配置节点错误：‘LoginPwd’配置节不存在。");
            }

            if (loginName.Equals(name) && password.Equals(pwd))
            {
                context.Session["CurrentlyUser"] = loginName;
                return true;
            }
            else
                throw new ApplicationException("用户名或密码错误。");

        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        private object GetCity()
        {
            City city = new City();
            List<City> list = city.GetCitys();
            list.Insert(0, new City { CityId = 0, Name = "全国" });
            return list;
        }

        /// <summary>
        /// 通过KEY值获取酒店
        /// </summary>
        /// <returns></returns>
        private object GetHotelByKey()
        {
            string cityName = base.GetStringParam("cityName", null, post);
            string key = base.GetStringParam("key", null, post);
            Query dataQuery = new Query();
            dataQuery.SetPage(PageIndex, base.GetIntParam("limit", null, post));
            Hotel hotel = new Hotel();
            DataTable dt = hotel.GetList(cityName, key, ref dataQuery);
            List<object> objList = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new
                {
                    HotelId = row["HotelId"],
                    CityName = row["CityName"],
                    HotelName = row["HotelName"],
                    IsActive = row["IsActive"],
                    State = row["State"]

                });
            }
            object obj = new
            {
                total = dataQuery.PageInfo.TotalCount,
                rows = objList,
            };

            return obj;
        }
        /// <summary>
        /// 更新酒店上下线
        /// </summary>
        /// <returns></returns>
        private object UpdateHotelActive()
        {
            int active = base.GetIntParam("active", null, post);
            string hotelids = base.GetStringParam("hotelids", null, post);
            Hotel hotel = new Hotel();
            return hotel.Update(hotelids, active);

        }
        /// <summary>
        /// 下线所有上线的酒店
        /// </summary>
        /// <returns></returns>
        private object AllHotelUnActive()
        {
            Hotel hotel = new Hotel();
            return hotel.SetUnActive();
        }

        /// <summary>
        /// 获取房型
        /// </summary>
        /// <returns></returns>
        private object GetRoomType()
        {
            string hotelId = base.GetStringParam("hotelId", null, post);
            RoomType rt = new RoomType();
            DataTable dt = rt.GetList2(hotelId.Replace("'", ""));

            List<object> objList = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                objList.Add(new
                {
                    RoomTypeId = dr[0].ToString(),
                    Name = dr[1].ToString()
                });
            }

            return objList;
        }
        /// <summary>
        /// 获取房态
        /// </summary>
        /// <returns></returns>
        private object GetRoomPrice()
        {
            string Usercd = ConfigHelper.GetConfigString("Usercd");
            string Authno = ConfigHelper.GetConfigString("Authno");
            string JieLvUrl = ConfigHelper.GetConfigString("JieLvUrl");
            string roomTypeId = base.GetStringParam("roomTypeId", null, post);
            string state = base.GetStringParam("state", null, post);
            DateTime fromDate = DateTime.Now;
            DateTime toDate = fromDate.AddDays(60);//查60天的数据
            int requestCount = 0;//总请求数
            int day = Math.Abs(((TimeSpan)(toDate - fromDate)).Days);
            
            requestCount = (day + 30 - 1) / 30;
            ResponseHotelPriceInfo info = new ResponseHotelPriceInfo();
            for (int i = 0; i < requestCount; i++)
            {
                DateTime startDate = fromDate.AddDays(30 * i);
                DateTime endDate = fromDate.AddDays(30 * (i + 1));
                endDate = endDate > toDate ? toDate : endDate;

                string checkInDateStr = startDate.ToString("yyyy-MM-dd");
                string checkOutDateStr = endDate.ToString("yyyy-MM-dd");

                String message = String.Format("{{'Usercd':'{0}','Authno':'{1}' ,'roomtypeids':'{2}','checkInDate':'{3}','checkOutDate':'{4}','QueryType':'hotelpriceall'}}", Usercd, Authno, roomTypeId, checkInDateStr, checkOutDateStr);//QueryType:hotelpriceall表示申请+即时确认；hotelpricecomfirm表示只要即时确认
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
            List<object> objList = new List<object>();
            
            foreach (var hprice in info.Data)
            {
                RoomPrice rp = new RoomPrice();
                RoomState rs = new RoomState();
                DataTable dtprice = rp.GetList(roomTypeId, fromDate, toDate);//获取控制的房价数据
                DataTable dtstate = rs.GetList(roomTypeId, fromDate, toDate);//获取控制的房态数据
                foreach (var roomDetail in hprice.roomPriceDetail)
                {
                    if (!ValidRateType(roomDetail.ratetype.ToString()))//验证房型
                        continue;
                    int ChanagePriceValue = 30;
                    int? customChangePrice = null;
                    int hailongRoomStateValue = -2;
                    var drprice = dtprice.Select(string.Format("roomtypeid='{0}' and night='{1}' and includebreakfastqty2='{2}' and ratetype={3}", roomTypeId, roomDetail.night.ToShortDateString(), roomDetail.includebreakfastqty2,roomDetail.ratetype));
                    var drstate = dtstate.Select(string.Format("roomtypeid='{0}' and night='{1}' and (includebreakfastqty2='{2}' or includebreakfastqty2='{3}') and ratetype in({3},{4})", roomTypeId, roomDetail.night.ToShortDateString(), roomDetail.includebreakfastqty2, -1, roomDetail.ratetype));//-1表示所有类型的都满足

                    if (drprice.Count() > 0)
                    {
                        var drp = drprice[0];
                        customChangePrice = drp.IsNull(3) ? customChangePrice : int.Parse(drp[3].ToString());
                    }
                    if (customChangePrice == null)
                    {
                        if (state != "70023")//海南
                        {
                            if (roomDetail.preeprice > 6000)
                                ChanagePriceValue = 200;
                            else if(roomDetail.preeprice>4000&&roomDetail.preeprice<=6000)
                                ChanagePriceValue = 150;
                            else if(roomDetail.preeprice>2500&&roomDetail.preeprice<=4000)
                                ChanagePriceValue = 100;
                            else if (roomDetail.preeprice > 800 && roomDetail.preeprice <= 2500)
                                ChanagePriceValue = 50;
                        }
                        if ((state == "70001" || state == "70050") && roomDetail.qtyable < 1)//港澳非立即确认的，全部设为满房
                        {
                            roomDetail.qtyable = -1;
                        }
                    }
                    else
                    {
                        ChanagePriceValue = customChangePrice.Value;
                    }
                    if (drstate.Count() > 0)
                    {
                        var drs = drstate[0];
                        hailongRoomStateValue = drs.IsNull(3) ? hailongRoomStateValue : int.Parse(drs[3].ToString());
                    }
                    objList.Add(new
                    {
                        hotelId = roomDetail.hotelid,
                        roomTypeId = roomDetail.roomtypeid,
                        roomtypename = string.Format("{0}({1})",roomDetail.roomtypename,roomDetail.ratetypename),
                        night = roomDetail.night.ToShortDateString(),
                        includebreakfastqty2 = roomDetail.includebreakfastqty2,
                        preeprice = roomDetail.preeprice,
                        ChanagePrice = ChanagePriceValue,
                        qtyable = roomDetail.qtyable,
                        roomState = roomDetail.qtyable,
                        hailongRoomState = hailongRoomStateValue,
                        ratetype = roomDetail.ratetype

                    });
                }
            }

            object obj = new
            {
                total = objList.Count,
                rows = objList,
            };
            return obj;
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
        /// 更新增减价
        /// </summary>
        /// <returns></returns>
        private object UpdateChanagePrice()
        {
            string hotelId = base.GetStringParam("hotelId", null, post);
            string roomTypeId = base.GetStringParam("roomTypeId", null, post);
            string itemList = base.GetStringParam("itemList", null, post);
            int changePrice = base.GetIntParam("changePrice", null, post);

            List<RoomPrice> priceList = JsonConvert.DeserializeObject<List<RoomPrice>>(itemList);
            foreach (var price in priceList)
            {
                price.SaveOrUpdateChangePrice(hotelId, roomTypeId, price.night, price.includebreakfastqty2, changePrice,price.ratetype);
            }
            return true;
        }

        /// <summary>
        /// 更新房态（龙网）
        /// </summary>
        /// <returns></returns>
        private object UpdateRoomState()
        {
            string hotelId = base.GetStringParam("hotelId", null, post);
            string roomTypeId = base.GetStringParam("roomTypeId", null, post);
            string itemList = base.GetStringParam("itemList", null, post);
            int? changeRoomState = base.GetIntParam("changeRoomState", null, post);
            changeRoomState = changeRoomState == -2 ? null : changeRoomState;//-2表示与捷旅同步房态
            List<RoomState> stateList = JsonConvert.DeserializeObject<List<RoomState>>(itemList);
            foreach (var state in stateList)
            {
                state.SaveOrUpdateRoomState(hotelId, roomTypeId, state.night, state.includebreakfastqty2, changeRoomState,state.ratetype);
            }
            return true;
        }
        /// <summary>
        /// 批量更新房态（龙网）
        /// </summary>
        /// <returns></returns>
        private object BatchUpdateRoomState()
        {
            bool isSuccess = false;
            string hotelids = base.GetStringParam("hotelIds", null, post);
            DateTime startTime = DateTime.Parse(base.GetStringParam("startTime", null, post));
            DateTime endTime = DateTime.Parse(base.GetStringParam("endTime", null, post));
            int? changeRoomState = base.GetIntParam("changeRoomState", null, post);
            changeRoomState = changeRoomState == -2 ? null : changeRoomState;//-2表示与捷旅同步房态

            RoomState rs = new RoomState();
            RoomType rt = new RoomType();
            DataTable roomtypes = rt.GetList3(hotelids);
            rs.Delete(hotelids, startTime, endTime);
            isSuccess = rs.BatchSaveRoomState(roomtypes, startTime, endTime, changeRoomState);

            return isSuccess;
        }
        /// <summary>
        /// 清除今天之前的旧数据
        /// </summary>
        /// <returns></returns>
        private object ClearRoomState()
        {
            RoomPrice rp = new RoomPrice();
            RoomState rs = new RoomState();

            return rp.DeleteBeforeToday() && rs.DeleteBeforeToday();

        }
    }
}