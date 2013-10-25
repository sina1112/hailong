namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;
    /// <summary>
    /// 自定义查询异常
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
