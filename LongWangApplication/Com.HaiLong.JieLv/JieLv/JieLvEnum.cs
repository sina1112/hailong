using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    public class JieLvEnum
    {
        #region 语言类型
        /// <summary>
        /// 语言类型
        /// </summary>
        public enum LanguageType
        {
            /// <summary>
            /// 所有
            /// </summary>
            ALL = 0,
            /// <summary>
            /// 中文
            /// </summary>
            CN = 1,
            /// <summary>
            /// 英语
            /// </summary>
            EN = 2,
            /// <summary>
            /// 阿拉伯文
            /// </summary>
            AR = 3,
            /// <summary>
            /// 德文
            /// </summary>
            DE = 4,
            /// <summary>
            /// 法文
            /// </summary>
            FR = 5,
            /// <summary>
            /// 意大利文
            /// </summary>
            IT = 6,
            /// <summary>
            /// 日文
            /// </summary>
            JA = 7,
            /// <summary>
            /// 朝鲜文
            /// </summary>
            KO = 8
        }
        #endregion

        #region 酒店主题类型
        /// <summary>
        /// 酒店主题类型
        /// </summary>
        public enum ThemeType
        {
            /// <summary>
            /// 所有
            /// </summary>
            ALL = 0,
            /// <summary>
            /// 商务酒店
            /// </summary>
            BIZ = 1,
            /// <summary>
            /// 温泉酒店
            /// </summary>
            SPA = 2,
            /// <summary>
            /// 主题酒店
            /// </summary>
            THEME = 3,
            /// <summary>
            /// 经济型酒店
            /// </summary>
            ECONOMY = 4,
            /// <summary>
            /// 度假酒店
            /// </summary>
            VACATION = 5,
            /// <summary>
            /// 酒店式公寓
            /// </summary>
            MANSION = 6,
            /// <summary>
            /// 会议酒店
            /// </summary>
            HUIYI = 7,

        }
        #endregion

        #region 响应类型
        /// <summary>
        /// 响应类型
        /// </summary>
        public enum ResponseType
        {
            /// <summary>
            /// 成功
            /// </summary>
            SUCCESS = 1,
            /// <summary>
            /// 失败
            /// </summary>
            FAILURE = 8,
            

        }
        #endregion

        #region 能处理的信用卡类型
        /// <summary>
        /// 能处理的信用卡类型
        /// </summary>
        public enum AllowcreditcardType
        {
            /// <summary>
            /// 所有
            /// </summary>
            ALL = 0,
            /// <summary>
            /// 万事达
            /// </summary>
            MASTER = 1,
            /// <summary>
            /// VISA
            /// </summary>
            VISA = 2,
            /// <summary>
            /// JCB
            /// </summary>
            JCB = 3,
            /// <summary>
            /// 银联
            /// </summary>
            UnionPay = 4,
            /// <summary>
            /// 大莱
            /// </summary>
            Diners = 5,
            /// <summary>
            /// 运通
            /// </summary>
            AEC = 6,

        }
        #endregion

        #region 酒店措施
        /// <summary>
        /// 酒店措施
        /// </summary>
        public enum FacilitiesType
        {
            /// <summary>
            /// 所有
            /// </summary>
            ALL = 0,
            /// <summary>
            /// 停车场
            /// </summary>
            Park = 11,
            /// <summary>
            /// 会议室
            /// </summary>
            MeetingRoom = 12,
            /// <summary>
            /// 游泳池
            /// </summary>
            Natatorium = 13,
            /// <summary>
            /// 健身房
            /// </summary>
            Gymnasium = 14,
            /// <summary>
            /// 洗衣服务
            /// </summary>
            LaundryService = 15,
            /// <summary>
            /// 中餐厅
            /// </summary>
            ChineseRestaurant = 16,
            /// <summary>
            /// 西餐厅
            /// </summary>
            WesternRestaurant = 17,
            /// <summary>
            /// 宴会厅
            /// </summary>
            Espana = 18,
            /// <summary>
            /// 租车服务
            /// </summary>
            CarRentalService = 19,
            /// <summary>
            /// 外币兑换
            /// </summary>
            ChangeMoney = 20,
            /// <summary>
            /// 咖啡厅
            /// </summary>
            CoffeeShop  = 21,
            /// <summary>
            /// ATM机
            /// </summary>
            ATM = 22,
            /// <summary>
            /// 酒吧
            /// </summary>
            BAR = 23,
            /// <summary>
            /// 叫醒服务
            /// </summary>
            WakeupService = 24,
            /// <summary>
            /// 网球场
            /// </summary>
            Court = 25,
            /// <summary>
            /// 歌舞厅
            /// </summary>
            Disco = 26,
            /// <summary>
            /// 美容美发
            /// </summary>
            BeautySalon = 27,
            /// <summary>
            /// 前台贵重物品保险柜
            /// </summary>
            Safe = 30,
            /// <summary>
            /// 送餐服务
            /// </summary>
            DeliveryService = 31,
            /// <summary>
            /// 礼宾司服务
            /// </summary>
            ConciergeService = 32,
            /// <summary>
            /// 商务中心
            /// </summary>
            BusinessCenter = 33,
            /// <summary>
            /// 旅游服务
            /// </summary>
            TravelService = 34,

        }
        #endregion

        #region 生效类型
        /// <summary>
        /// 生效类型
        /// </summary>
        public enum ActiveType
        {
            /// <summary>
            /// 生效
            /// </summary>
            Active = 1,
            /// <summary>
            /// 禁用
            /// </summary>
            UnActive = 8,


        }
        #endregion

        #region 变价类型
        /// <summary>
        /// 变价类型
        /// </summary>
        public enum PriceChangeType
        {
            /// <summary>
            /// 正在变价中
            /// </summary>
            Changing = 1,
            /// <summary>
            /// 默认
            /// </summary>
            Default = 8,


        }
        #endregion
    }
}
