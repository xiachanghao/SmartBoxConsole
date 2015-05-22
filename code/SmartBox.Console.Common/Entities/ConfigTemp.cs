using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("ConfigTemp", SorMappingType.ByAttributes)]
    public class ConfigTemp:BaseEntry
    {
        #region

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
        [Column("PluginCode",ColumnType.PrimaryKey)]
        public string PluginCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("Key", ColumnType.PrimaryKey)]
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
        //[Column("ValueAssembly",ColumnType.ReadOnly)]
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
        public string ConfigId
        {
            set;
            get;
        }
        public string UserUId
        {
            set;
            get;
        }
        #endregion Model
    }
}
