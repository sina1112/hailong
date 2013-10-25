using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Com.HaiLong.Lib.Common
{
    /// <summary>
    /// 把zTreeNode对象转换成Json字符串
    /// </summary>
    public class zTreeConvert
    {
        #region 将一个zTreeNode对象封装成jsons格式
        /**
         * 将一个对象的所有属性和属性值按json的格式要求输入为一个封装后的结果。
         *
         * 返回值类似{"属性1":"属性1值","属性2":"属性2值","属性3":"属性3值"}
         * 
         * */
        private static string NodeToJSON(zTreeNode node)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.AppendFormat("id:'{0}',", node.ID);
            if (node.Open)
                result.Append("open:true,");
            if (node.NoCheck)
                result.Append("nocheck:true,");
            if (node.IsParent)
                result.Append("isParent:true,");
            if (!string.IsNullOrWhiteSpace(node.Icon))
                result.AppendFormat("icon:'{0}',", node.Icon);
            if (!string.IsNullOrWhiteSpace(node.OpenIcon))
                result.AppendFormat("iconOpen:'{0}',", node.OpenIcon);
            if (!string.IsNullOrWhiteSpace(node.CloseIcon))
                result.AppendFormat("iconClose:'{0}',", node.CloseIcon);
            if (!string.IsNullOrWhiteSpace(node.IconSkin))
                result.AppendFormat("iconSkin:'{0}',", node.IconSkin);
            if (!string.IsNullOrWhiteSpace(node.Url))
                result.AppendFormat("url:'{0}',", node.Url);
            if (!string.IsNullOrWhiteSpace(node.Target))
                result.AppendFormat("target:'{0}',", node.Target);
            result.AppendFormat("drag:{0},", node.Drag.ToString().ToLower());
            result.AppendFormat("noR:{0},", node.NoR.ToString().ToLower());

            if (node.Childs != null && node.Childs.Count > 0)
            {
                result.Append("children:[");
                bool isFristLine = true;
                foreach (zTreeNode child in node.Childs)
                {
                    if (!isFristLine)
                    {
                        result.Append(",");
                    }
                    isFristLine = false;
                    result.Append(NodeToJSON(child));
                }
                result.Append("],");
            }

            if (node.OtherAttrList != null && node.OtherAttrList.Count > 0)
            {
                foreach (string key in node.OtherAttrList.Keys)
                {
                    result.AppendFormat("{0}:'{1}',", key, node.OtherAttrList[key]);
                }
            }

            result.AppendFormat("name:'{0}'", node.Name);
            result.Append("}");

            return result.ToString();
        }
        #endregion

        #region 把对象列表转换成json串
        /// <summary>
        /// 把对象列表转换成json串
        /// </summary>
        /// <param name="objlist"></param>
        /// <returns></returns>
        public static string ToJSON(List<zTreeNode> objlist)
        {
            StringBuilder result = new StringBuilder();
            result.Append("[");
            bool isFirsetLine = true;
            foreach (zTreeNode node in objlist)
            {
                if (!isFirsetLine)
                {
                    result.Append(",");
                }
                result.Append(NodeToJSON(node));

                isFirsetLine = false;
            }
            result.Append("]");
            return result.ToString();
        }
        #endregion

        #region 把对象转换成json串
        /// <summary>
        /// 把对象转换成json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(zTreeNode obj)
        {
            if (obj == null)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            result.Append("[");
            result.Append(NodeToJSON(obj));
            result.Append("]");

            return result.ToString();
        }
        #endregion
    }
}