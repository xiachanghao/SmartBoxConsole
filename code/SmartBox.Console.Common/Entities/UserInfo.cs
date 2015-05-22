using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("UserInfo", SorMappingType.ByAttributes)]
    public class UserInfo : BaseEntry 
    {
        [Column("UserUId", ColumnType.PrimaryKey)]
        public string UserUId
        {
            set;
            get;
        }

        [Column("Signature")]
        public string Signature
        { get; set; }

        [Column("UserIconCode")]
        public string UserIconCode
        { get; set; }

        [Column("Status")]
        public string Status
        { get; set; }
        
        [Column("Lock")]
        public bool Lock
        { get; set; }

        [Column("LastLoginIP")]
        public string LastLoginIP
        { get; set; }

        [Column("LastLoginTime")]
        public string LastLoginTime
        { get; set; }

        [Column("LastLogoutTime")]
        public string LastLogoutTime
        { get; set; }
        
        [Column("LastLockTime")]
        public DateTime LastLockTime
        { get; set; }
        
        [Column("LastUnLockTime")]
        public DateTime LastUnLockTime
        { get; set; }
    }
}
