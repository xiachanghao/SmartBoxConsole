using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 设备绑定审核状态
    /// </summary>
    public enum ApplyDeviceBindStatus : int
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 0,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approval = 1,
        /// <summary>
        /// 审核不通过
        /// </summary>
        UnApproval = 2
    }
}
