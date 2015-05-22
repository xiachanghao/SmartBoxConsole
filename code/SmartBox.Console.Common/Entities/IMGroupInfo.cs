using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("", SorMappingType.ByAttributes)]
    public class IMGroupInfo : BaseEntry
    {
        [Column("PublishId")]
        public string PublishId { get; set; }

        [Column("GroupDisplayName")]
        public string DisplayName { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("UserName")]
        public string Owner { get; set; }

        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
