namespace Com.HaiLong.Lib.Common.DataQuery
{
    using System;
    using System.Data;
    /// <summary>
    /// 分页信息类
    /// </summary>
    [Serializable]
    public class PageInfo
    {
        private int _CurrentPage;
        private int _PageSize;
        private int _PageCount;
        private int _StartIndex;

        /// <summary>
        /// 当前页数据开始行
        /// </summary>
        public int StartIndex
        {
            get
            {
                this._StartIndex = this.PageSize * (this.CurrentPage - 1);
                return _StartIndex;
            }
        }
        private int _EndIndex;
        /// <summary>
        /// 当前页数据结束行
        /// </summary>
        public int EndIndex
        {
            get
            {
                this._EndIndex = this.PageSize * this.CurrentPage - 1;
                return _EndIndex;
            }
        }

        private int _TotalCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        public PageInfo(int currentPage, int pageSize)
        {
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (pageSize < 1)
            {
                throw new QueryParseException("页大小不能小于1 ！");
            }
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return this._CurrentPage;
            }
            set
            {
                if (value < 1)
                {
                    this._CurrentPage = 1;
                }
                this._CurrentPage = value;
            }
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new QueryParseException("页大小不能小于1 ！");
                }
                this._PageSize = value;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                this._PageCount = (this.TotalCount / this.PageSize) + (((this.TotalCount % this.PageSize) > 0) ? 1 : 0);
                return _PageCount;
            }
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _TotalCount;
            }
            set
            {
                if (value < 0)
                {
                    this._TotalCount = 0;
                }
                _TotalCount = value;
            }
        }

    }
}
