using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("PrivilegeUser")]
    public class PrivilegeUser
    {
        #region Property
        [Column("ID", ColumnType.PrimaryKey)]
        public int ID { get; set; }

        [Column("Uid", ColumnType.PrimaryKey)]
        public string Uid { get; set; }

        [Column("CreateUid")]
        public string CreateUid { get; set; }

        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        [Column("UpdateUid")]
        public string UpdateUid { get; set; }

        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        #endregion
    }
}
