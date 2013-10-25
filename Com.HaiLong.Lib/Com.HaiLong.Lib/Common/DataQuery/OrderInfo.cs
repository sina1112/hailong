namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public class OrderInfo
    {
        private List<OrderItem> _OrderItems = new List<OrderItem>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItem"></param>
        public void AddOrderItem(OrderItem orderItem)
        {
            this._OrderItems.Add(orderItem);
        }
        /// <summary>
        /// ��������SQL���(����'Order by')
        /// </summary>
        /// <returns></returns>
        public string GenerateSQLOrderString()
        {
            string str = "";
            for (int i = 0; i < this._OrderItems.Count; i++)
            {
                str = str + this._OrderItems[i].GenerateSQLString();
                if ((this._OrderItems.Count > 1) && (i < (this._OrderItems.Count - 1)))
                {
                    str = str + " , ";
                }
            }
            if (str.Length > 0)
            {
                str = " ORDER BY " + str;
            }
            return str;
        }
        /// <summary>
        /// ��������SQL���(������'Order by')
        /// </summary>
        /// <returns></returns>
        public string GenerateSQLString()
        {
            string str = "";
            for (int i = 0; i < this._OrderItems.Count; i++)
            {
                str = str + this._OrderItems[i].GenerateSQLString();
                if ((this._OrderItems.Count > 1) && (i < (this._OrderItems.Count - 1)))
                {
                    str = str + " , ";
                }
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public OrderItem GetOrderItem(int index)
        {
            return this._OrderItems[index];
        }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> OrderItems
        {
            get
            {
                return this._OrderItems;
            }
        }
    }
}
