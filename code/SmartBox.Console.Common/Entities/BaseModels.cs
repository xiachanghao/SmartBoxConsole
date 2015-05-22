using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    public class BaseModels:BaseEntry
    {
        [Column("CreateUid")]
        public string CreateUid
        { get; set; }
        [Column("CreateTime")]
        public DateTime CreateTime
        { get; set; }
        [Column("LastModUid")]
        public string LastModUid
        { get; set; }
        [Column("LastModTime")]
        public DateTime LastModTime
        { get; set; }
    }
}
