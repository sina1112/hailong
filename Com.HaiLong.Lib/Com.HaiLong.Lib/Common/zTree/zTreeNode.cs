using System;
using System.Collections.Generic;

namespace Com.HaiLong.Lib.Common
{
    /// <summary>
    /// zTree节点类
    /// </summary>
    public class zTreeNode
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 节点值
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 用于设置节点是否不具有 checkbox 勾选功能
        /// </summary>
        public bool NoCheck
        {
            get;
            set;
        }

        /// <summary>
        /// 用户设置节点checkbox是否选中
        /// </summary>
        public bool Checked
        {
            get;
            set;
        }

        /// <summary>
        /// 节点的子节点数据集合
        /// </summary>
        public List<zTreeNode> Childs
        {
            get;
            set;
        }

        /// <summary>
        /// 直接使用 图片的Url路径 设置节点的个性化图标。
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// 直接使用 图片的Url路径 设置父节点展开时的个性化图标。
        /// </summary>
        public string OpenIcon
        {
            get;
            set;
        }

        /// <summary>
        /// 直接使用 图片的Url路径 设置父节点折叠时的个性化图标。
        /// </summary>
        public string CloseIcon
        {
            get;
            set;
        }

        /// <summary>
        /// 利用 className 设置节点的个性化图标
        /// </summary>
        public string IconSkin
        {
            get;
            set;
        }

        /// <summary>
        /// 用于记录 treeNode 节点的 展开 / 折叠 状态。
        /// </summary>
        public bool Open
        {
            get;
            set;
        }

        /// <summary>
        /// 指定节点被点击后的跳转页面 URL 地址
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 用于设置点击节点后 url 跳转的目标。[treeNode.url 存在时有效]
        /// </summary>
        public string Target
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可以拖动
        /// </summary>
        public bool Drag
        {
            get;
            set;
        }

        /// <summary>
        /// 是否不相应右键事件
        /// </summary>
        public bool NoR
        {
            get;
            set;
        }

        /// <summary>
        /// 用于记录 treeNode 节点是否为父节点
        /// </summary>
        public bool IsParent
        {
            get;
            set;
        }

        /// <summary>
        /// 其它自定义属性
        /// </summary>
        public Dictionary<string, string> OtherAttrList
        {
            get;
            set;
        }
    }
}
