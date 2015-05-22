using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("LogInfo", SorMappingType.ByAttributes)]
    public class LogInfo : BaseEntry
    {
        #region

        /// <summary>
        /// 
        /// </summary>
        [Column("LogId", ColumnType.IdentityAndPrimaryKey)]
        public int LogId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Msg")]
        public string Msg
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("UserUid")]
        public string UserUid
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Time")]
        public DateTime Time
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Type")]
        public string Type
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Ip")]
        public string Ip
        {
            set;
            get;
        }
       
        #endregion Model
    }
}
