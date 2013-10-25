namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;

    /// <summary>
    /// ��ҳ��ѯ��
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
        /// ��ҳ���캯��
        /// </summary>
        /// <param name="currentPage">��ǰҳ</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="orderByList">��������</param>
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
        /// ���Ҫ˳��������ֶ���
        /// </summary>
        /// <param name="orderByPropertyName"></param>
        public void AddAscendOrderBy(string orderByPropertyName)
        {
            this.AddOrderBy(orderByPropertyName, OrderLogic.Ascend);
        }
        /// <summary>
        /// ���Ҫ����������ֶ���
        /// </summary>
        /// <param name="orderByPropertyName"></param>
        public void AddDescendOrderBy(string orderByPropertyName)
        {
            this.AddOrderBy(orderByPropertyName, OrderLogic.Descend);
        }
        /// <summary>
        /// �������
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
        /// ���÷�ҳ��Ϣ
        /// </summary>
        /// <param name="currentPage">��ǰҳ</param>
        /// <param name="pageSize">ÿҳ��С</param>
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
