using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("Manager", SorMappingType.ByAttributes)]
    public class Manager : BaseEntry
    {
        [Column("UserUid", ColumnType.PrimaryKey)]
        public string UserUid
        {
            set;
            get;
        }

        [Column("UserPwd")]
        public string UserPwd
        { get; set; }
    }
}

