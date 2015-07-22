using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("Manager", SorMappingType.ByAttributes)]
    public class ManagerInfo : BaseEntry
    {
        [Column("UserUid", ColumnType.PrimaryKey)]
        public string UserUid
        {
            set;
            get;
        }

        [Column("UserPwd")]
        public string UserPwd
        {
            set;
            get;
        }

        /// <summary>
        /// 是否主管理员
        /// </summary>
        [Column("IsMain", ColumnType.Normal)]
        public bool IsMain { get; set; }

        [Column("Lock")]
        public bool Lock { get; set; }

        [Column("LastLoginIP")]
        public string LastLoginIP { get; set; }

        [Column("LastLoginTime")]
        public DateTime LastLoginTime { get; set; }

        [Column("ErrorCount")]
        public int ErrorCount { get; set; }

        [Column("LastLockTime")]
        public DateTime LastLockTime { get; set; }
    }
}
