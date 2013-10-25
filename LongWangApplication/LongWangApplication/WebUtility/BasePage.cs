using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

namespace LongWangApplication.WebUtility
{
    public class BasePage : Page
    {
        #region 当前用户
        /// <summary>
        /// 当前用户
        /// </summary>
        public string CurrentlyUser
        {
            get
            {
                if (Session["CurrentlyUser"] == null)
                    return null;
                return Session["CurrentlyUser"].ToString();
            }
            set
            {
                Session["CurrentlyUser"] = value;
            }
        }
        #endregion
        #region 服务器根路径
        /// <summary>
        /// 服务器根路径
        /// </summary>
        public string ApplicationRoot
        {
            get
            {
                string root = HttpContext.Current.Request.ApplicationPath;
                return root == "/" ? "" : root;
            }
        }
        #endregion

        #region 应用程序路径
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                return Request.Url.Authority + ApplicationRoot;
            }
        }
        #endregion

        #region 登录页面
        /// <summary>
        /// 登录页面
        /// </summary>
        public string LoginUrl
        {
            get
            {
                return ApplicationRoot + "/Manager/Login.aspx";
            }
        }
        #endregion
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (Request.Path.ToLower() != LoginUrl.ToLower())
            {
                try
                {
                    #region 检查用户是否已经登录
                    if (CurrentlyUser == null)
                        throw new ApplicationException("用户已过期，请重新登录！");
                    #endregion

                }
                catch (Exception ex)
                {
                    CurrentlyUser = null;

                    StringBuilder error = new StringBuilder();
                    error.Append("<script type=\"text/javascript\">");
                    error.AppendFormat("alert(\"{0}\");", ex.Message.Replace("\r\n", ""));
                    error.AppendFormat("parent.location.href=\"{0}\";", LoginUrl);
                    error.Append("</script>");

                    Response.Write(error.ToString());
                    Response.End();
                }
            }
            base.OnLoad(e);
        }
        #endregion
    }
}