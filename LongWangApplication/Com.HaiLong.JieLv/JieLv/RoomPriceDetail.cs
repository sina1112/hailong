using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.HaiLong.Provider.JieLv
{
    /// <summary>
    ///  房价详细信息实体类 
    /// </summary>
    [Serializable]
    public partial class RoomPriceDetail
    {
        /// <summary>
        /// 酒店ID
        /// </summary>
        public int hotelid { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string namechn { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime night { get; set; }
        /// <summary>
        /// 房型ID
        /// </summary>
        public int roomtypeid { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string roomtypename { get; set; }
        /// <summary>
        /// 定价类型
        /// </summary>
        public int? pricingtype { get; set; }
        /// <summary>
        /// 配额类型
        /// </summary>
        public int? allotmenttype { get; set; }
        /// <summary>
        ///    币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 现付类型
        /// </summary>
        public int? facepaytype { get; set; }
        /// <summary>
        /// 同行标准价
        /// </summary>
        public double? preeprice { get; set; }
        /// <summary>
        /// 商务价
        /// </summary>
        public double? businessprice { get; set; }
        /// <summary>
        /// 价格类型id
        /// </summary>
        public int ratetype { get; set; }
        /// <summary>
        /// 价格类型名称
        /// </summary>
        public string ratetypename { get; set; }
        /// <summary>
        /// 含早份数：中1份，西1份，自1份
        /// </summary>
        public int? includebreakfastqty2 { get; set; }
        /// <summary>
        /// 当前可售房间数量 大于0表示可即时确认，等于0表示需要等待确认，小于0表示满房
        /// </summary>
        public int? qtyable { get; set; }
        /// <summary>
        /// 上网类型
        /// </summary>
        public int? internetprice { get; set; }
        /// <summary>
        /// 是否免费带宽，如收费则上网价格  小于等于0免费 ，NULL表示收费，金额未定，大于0表示上网的费用
        /// </summary>
        public int? netcharge { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? lastupdatepricetime { get; set; }
        /// <summary>
        /// 条款类型：提前订房，指定日期前，连住晚数，指定时间段
        /// </summary>
        public int? termtype { get; set; }

        /// <summary>
        /// 提前天数
        /// </summary>
        public int? advancedays { get; set; }

        /// <summary>
        /// 小时数
        /// </summary>
        public string advancetime { get; set; }
        /// <summary>
        /// 指定日期
        /// </summary>
        public DateTime? appointeddate { get; set; }
        /// <summary>
        /// 连住晚数
        /// </summary>
        public int? continuousdays { get; set; }
        /// <summary>
        /// 指定开始日期
        /// </summary>
        public DateTime? beginday { get; set; }
        /// <summary>
        /// 指定结束日期
        /// </summary>
        public DateTime? endday { get; set; }
        /// <summary>
        /// 取消修改条款类型
        /// </summary>
        public int? voidabletype { get; set; }
        /// <summary>
        /// 提前天数参数
        /// </summary>
        public int? advancedays2 { get; set; }
        /// <summary>
        /// 指定时段开始日期
        /// </summary>
        public DateTime? beginday2 { get; set; }
        /// <summary>
        /// 指定时段结束日期
        /// </summary>
        public DateTime? endday2 { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string advancetime2 { get; set; }
        /// <summary>
        /// 入住类型
        /// </summary>
        public int? restype { get; set; }
        /// <summary>
        /// 入住前/确认后 多少天
        /// </summary>
        public int? dayselect { get; set; }
        /// <summary>
        /// 入住前/确认后 多少天 几点前
        /// </summary>
        public string timeselect { get; set; }
        /// <summary>
        /// 不可修改或不可取消
        /// </summary>
        public int? noeditorcancel { get; set; }
        /// <summary>
        /// 不可修改包括内容
        /// </summary>
        public string noedit { get; set; }
    }
}
