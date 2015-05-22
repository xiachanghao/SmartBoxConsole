using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("PluginCategory", SorMappingType.ByAttributes)]
    public class PluginCategory
    {
        #region Model
        [Column("PluginCateId", ColumnType.IdentityAndPrimaryKey)]
        public string PluginCateId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("PluginCateCode")]
        public string PluginCateCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("DisplayName")]
        public string DisplayName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Summary")]
        public string Summary
        {
            set;
            get;
        }
        #endregion Model
    }
}
