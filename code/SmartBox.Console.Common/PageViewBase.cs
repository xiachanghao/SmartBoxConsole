using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    /// <summary>
    /// 分页基类
    /// </summary>
    public abstract class PageViewBase
    {
        /// <summary>
        /// 每页记录数
        /// </summary>
        /// <value>The size of the page.</value>
        /// <Author>wangsm 2008/12/30</Author>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码，从0开始计数
        /// </summary>
        /// <value>The index of the page.</value>
        /// <Author>wangsm 2008/12/30</Author>
        public int PageIndex { get; set; }

        /// <summary>
        ///总记录数
        /// </summary>
        /// <value>The record count.</value>
        /// <Author>wangsm 2008/12/30</Author>
        public int RecordCount { get; set; }


        /// <summary>
        /// Gets or sets the current user id.
        /// </summary>
        /// <value>The current user id.</value>
        public string CurrentUserId { get; set; }
    }
}
