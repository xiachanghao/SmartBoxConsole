using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    /// <summary>		
    /// 权限信息
    /// </summary>
    public class Privilege
    {
        #region 属性
        /// <summary>
        /// 权限标识 
        ///	Length:20 
        /// </summary>
        public string PrivilegeCode
        {
            get;
            set;
        }
        /// <summary>
        /// 权限名称 
        ///	Length:100 
        /// </summary>
        public string PrivilegeName
        {
            get;
            set;
        }
        /// <summary>
        /// 权限类型 
        ///	Length:1 
        /// </summary>
        public PrivilegeType PrivilegeType
        {
            get;
            set;
        }
        /// <summary>
        /// 父权限标识 
        ///	Length:20 
        /// </summary>
        public string ParentID
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单权限对应的链接地址 
        ///	Length:2000 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }
        /// <summary>
        /// 创建用户标识 
        ///	Length:50 
        /// </summary>
        public string CreateUserUID
        {
            get;
            set;
        }
        /// <summary>
        /// 创建用户姓名 
        ///	Length:100 
        /// </summary>
        public string CreateUserName
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间 
        ///	Length:8 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次更新用户标识 
        ///	Length:50 
        /// </summary>
        public string LastUpdateUserUID
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次更新用户姓名 
        ///	Length:100 
        /// </summary>
        public string LastUpdateUserName
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次更新时间 
        ///	Length:8 
        /// </summary>
        public DateTime LastUpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 排序号
        ///	Length:4 
        /// </summary>
        public int Sequence
        {
            get;
            set;
        }

        #endregion
        #region ReadOnly
        public bool HaveChildren { get; set; }
        public string ParentName { get; set; }

        #endregion
    }
}
