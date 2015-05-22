using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public enum UserEnabledStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        UnAuthed = 2,
        /// <summary>
        /// 启用
        /// </summary>
        Enabled = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled = 0
    }
}
