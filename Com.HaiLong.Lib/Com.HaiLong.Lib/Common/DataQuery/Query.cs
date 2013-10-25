namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;

    /// <summary>
    /// 分页查询类
    /// </summary>
    [Serializable]
    public class Query
    {
        private OrderInfo _OrderInfo;
        private PageInfo _PageInfo;

        /// <summary>
        /// 
        /// </summary>
        public Query()
        {
            _OrderInfo = new OrderInfo();
            
        }
        /// <summary>
        /// 分页构造函数
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="orderByList">排序内容</param>
        public Query(int currentPage, int pageSize,string orderByList)
        {
            this._PageInfo = new PageInfo(currentPage, pageSize);
            this._OrderInfo = new OrderInfo();
            if ((orderByList != null) && (orderByList.Trim().Length > 0))
            {
                string[] strArray = orderByList.Trim().TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
                foreach (string str in strArray)
                {
                    if (str.Trim().Length > 0)
                    {
                        OrderLogic orderLogic = str.Trim().ToUpper().EndsWith(" DESC") ? OrderLogic.Descend : OrderLogic.Ascend;
                        string orderByPropertyName = str.Trim().Split(new char[] { ' ' })[0];
                        this._OrderInfo.AddOrderItem(new OrderItem(orderByPropertyName, orderLogic));
                    }
                }
            }
        }
        /// <summary>
        /// 添加要顺序排序的字段名
        /// </summary>
        /// <param name="orderByPropertyName"></param>
        public void AddAscendOrderBy(string orderByPropertyName)
        {
            this.AddOrderBy(orderByPropertyName, OrderLogic.Ascend);
        }
        /// <summary>
        /// 添加要倒序排序的字段名
        /// </summary>
        /// <param name="orderByPropertyName"></param>
        public void AddDescendOrderBy(string orderByPropertyName)
        {
            this.AddOrderBy(orderByPropertyName, OrderLogic.Descend);
        }
        /// <summary>
        /// 添加排序
        /// </summary>
        /// <param name="orderByPropertyName"></param>
        /// <param name="orderLogic"></param>
        private void AddOrderBy(string orderByPropertyName, OrderLogic orderLogic)
        {
            if (this.OrderInfo == null)
            {
                this._OrderInfo = new OrderInfo();
            }
            OrderItem orderItem = new OrderItem(orderByPropertyName, orderLogic);
            this.OrderInfo.AddOrderItem(orderItem);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this._PageInfo = null;
            this._OrderInfo = null;
        }
        /// <summary>
        /// 设置分页信息
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页大小</param>
        public void SetPage(int currentPage, int pageSize)
        {
            if (this.PageInfo == null)
            {
                this._PageInfo = new PageInfo(currentPage, pageSize);
            }
            else
            {
                this._PageInfo.CurrentPage = currentPage;
                this._PageInfo.PageSize = pageSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public OrderInfo OrderInfo
        {
            get
            {
                return this._OrderInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PageInfo PageInfo
        {
            get
            {
                return this._PageInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsHaveOrderInfo
        {
            get
            {
                return this._OrderInfo.OrderItems.Count > 0;
            }
        }
    }
}
