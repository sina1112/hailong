namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;
    /// <summary>
    /// �Զ����ѯ�쳣
    /// </summary>
    [Serializable]
    public class QueryParseException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public QueryParseException(string message) : base(message)
        {
        }
    }
}
