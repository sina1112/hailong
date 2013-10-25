using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.HaiLong.Lib.Common;

namespace LongWangApplication.Ashx
{
    /// <summary>
    /// BaseAshx 的摘要说明
    /// </summary>
    public class BaseAshx : IHttpHandler
    {
       protected LogManager logManager = new LogManager();

       #region 分页页码索引
       /// <summary>
       /// 分页页码索引
       /// </summary>
       protected int PageIndex
       {
           get
           {
               int index = 0;
               int start = 0;
               if (int.TryParse(HttpContext.Current.Request.Form["start"], out start))
               {
                   int limit = 0;
                   if (int.TryParse(HttpContext.Current.Request.Form["limit"], out limit))
                   {
                       index = (start % limit > 0) ? start / limit + 1 : start / limit;
                   }
               }
               return index + 1;
           }
       }
       #endregion
       public virtual void ProcessRequest(HttpContext context)
        { 
            
        }
        #region 转换Request接收的参数值
        protected int GetIntParam(string paramName, string errorMsg,string requstMethod)
        {
            int param = 0;
            string value = requstMethod.ToUpper()=="POST"?HttpContext.Current.Request.Form[paramName]:HttpContext.Current.Request.QueryString[paramName];
            errorMsg = string.IsNullOrWhiteSpace(errorMsg) ? "参数传递无效：" + paramName : errorMsg;
            if (!int.TryParse(value, out param))
                throw new ApplicationException(errorMsg);
            return param;
        }
        protected long GetLongParam(string paramName, string errorMsg, string requstMethod)
        {
            long param = 0;
            string value = requstMethod.ToUpper() == "POST" ? HttpContext.Current.Request.Form[paramName] : HttpContext.Current.Request.QueryString[paramName];
            errorMsg = string.IsNullOrWhiteSpace(errorMsg) ? "参数传递无效：" + paramName : errorMsg;
            if (!long.TryParse(value, out param))
                throw new ApplicationException(errorMsg);
            return param;
        }
        protected string GetStringParam(string paramName, string errorMsg, string requstMethod)
        {
            string param = requstMethod.ToUpper() == "POST" ? HttpContext.Current.Request.Form[paramName] : HttpContext.Current.Request.QueryString[paramName];
            errorMsg = string.IsNullOrWhiteSpace(errorMsg) ? "参数传递无效：" + paramName : errorMsg;
            if (param == null)
                throw new ApplicationException(errorMsg);
            return param;
        }
        protected bool GetBoolParam(string paramName, string errorMsg, string requstMethod)
        {
            bool param = false;
            errorMsg = string.IsNullOrWhiteSpace(errorMsg) ? "参数传递无效：" + paramName : errorMsg;
            string value = requstMethod.ToUpper() == "POST" ? HttpContext.Current.Request.Form[paramName] : HttpContext.Current.Request.QueryString[paramName];
            if (!bool.TryParse(value, out param))
                throw new ApplicationException(errorMsg);
            return param;
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}