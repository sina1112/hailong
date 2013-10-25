namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public class OrderItem
    {
        private string _OrderByPropertyName;
        private OrderLogic _OrderLogic;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="orderByPropertyName"></param>
       /// <param name="orderLogic"></param>
        public OrderItem(string orderByPropertyName,OrderLogic orderLogic)
        {
            this._OrderByPropertyName = orderByPropertyName;
            this._OrderLogic = orderLogic;
        }
        /// <summary>
        /// ����SQL�����ַ���
        /// </summary>
        /// <returns></returns>
        public string GenerateSQLString()
        {
            return (this._OrderByPropertyName + " " + ((this._OrderLogic == OrderLogic.Ascend) ? "ASC" : "DESC"));
        }
        /// <summary>
        /// ������
        /// </summary>
        public string OrderByPropertyName
        {
            get
            {
                return this._OrderByPropertyName;
            }
            set
            {
                this._OrderByPropertyName = value;
            }
        }
        /// <summary>
        /// �����߼�
        /// </summary>
        public OrderLogic OrderLogic
        {
            get
            {
                return this._OrderLogic;
            }
            set
            {
                this._OrderLogic = value;
            }
        }
    }
}
