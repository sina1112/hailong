using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace LongWangApplication.Ashx
{
    /// <summary>
    /// JieLvHandler 的摘要说明
    /// </summary>
    public class JieLvHandler : BaseAshx
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
          

            //if (context.Request.RequestType != "POST")
            //    return;
            //object result = null;//返回结果
            //bool isError = false;//是否有错误

            //try
            //{
            //    string url = "网址";
            //    string action = base.GetStringParam("action", null);
            //    switch (action)
            //    {
            //        case "userLogin":
            //            //result = UserLogin();
                       
            //            break;
            //        case "userRegister":
            //            //result = UserRegister();
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //catch (Exception e)
            //{
            //    isError = true;
            //    result = e.Message;
            //}
            //finally
            //{
            //    string returnStr = null;
            //    var obj = new
            //    {
            //        IsError = isError,
            //        Result = result
            //    };
            //    returnStr = JsonConvert.SerializeObject(obj);
            //    context.Response.ContentType = "application/Json";
            //    context.Response.Write(returnStr);
            //    context.Response.End();
            //}
        }
        
      
    }
}