using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("ConfigCategory", SorMappingType.ByAttributes)]
    public class ConfigCategory
    {
        #region Model
        [Column("ConfigCategoryId", ColumnType.IdentityAndPrimaryKey)]
        public string ConfigCategoryId
        {
            get;
            set;

        }
        /// <summary>
        /// 
        /// </summary>
        [Column("ConfigCategoryCode")]
        public string ConfigCategoryCode
        {
            get;
            set;
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
