using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public enum PrivilegeType
    {
        /// <summary>
        /// 一般权限
        /// </summary>
        Nomal = 1,
        /// <summary>
        /// 菜单权限
        /// </summary>
        Menu = 2,
        
        //目录权限
        Dir=3,
        Func=1
    }
}
