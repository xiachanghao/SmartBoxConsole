using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Table("NotificationReport")]
    public class NotificationReport
    {
        [Column("ID", ColumnType.IdentityAndPrimaryKey)]
        public int ID { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        [Column("NotificationID")]
        public System.Int64 NotificationID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ReportCode")]
        public string ReportCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ReportMessage")]
        public string ReportMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("AppCode")]
        public string AppCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("DeviceToken")]
        public string DeviceToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Payload")]
        public string Payload { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("NotificationIdentifier")]
        public string NotificationIdentifier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ExpirationData")]
        public DateTime ExpirationData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Priority")]
        public int Priority { get; set; }
    }
}
