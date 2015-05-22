using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("DeviceUserApply")]
    public class DeviceUserApply
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }

        [Column("DeviceUserID")]
        public string DeviceUserID { get; set; }
        
        /// <summary>
        /// 0：申请中；1：已审核通过；2：已审核不通过；3已审核不通过并禁用
        /// </summary>
        [Column("Status")]
        public int Status { get; set; }
        
        [Column("ApplyTime")]
        public DateTime ApplyTime { get; set; }

        [Column("CheckUid")]
        public string CheckUid { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("CheckTime")]
        public DateTime CheckTime { get; set; }

        
    }
}
