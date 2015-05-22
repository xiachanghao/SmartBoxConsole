using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    [Table("ConfigInfo", SorMappingType.ByAttributes)]
    public class ConfigInfo : BaseEntry
    {
        #region

        /// <summary>
        /// 
        /// </summary>
        [Column("ConfigId", ColumnType.IdentityAndPrimaryKey)]
        public string ConfigId
        {
            set;
            get;
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
        [Column("UserUId")]
        public string UserUId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("PluginCode")]
        public string PluginCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Key")]
        public string Key1
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Value")]
        public string Value1
        {
            set;
            get;
        }
        //[Column("ValueAssembly")]
        //public string ValueAssembly
        //{
        //    set;
        //    get;
        //}
        /// <summary>
        /// 
        /// </summary>
        [Column("Summary")]
        public string Summary
        {
            set;
            get;
        }

        public string OldValue
        {
            set;
            get;
        }
        #endregion Model
    }
}
