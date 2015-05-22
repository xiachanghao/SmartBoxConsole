//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	 dbo.SMC_Unit.cs
// ??????? 
//
// ????? 2014-03-05 04:11:44
//
// ?????
// ?????
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("GlobalParam", SorMappingType.ByAttributes)]
    [Serializable]
    public class GlobalParam : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("ConfigKey", ColumnType.PrimaryKey)]
        public string ConfigKey
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("ConfigValue", ColumnType.Normal)]
        public string ConfigValue
        {
            get;
            set;
        }
        
        /// <summary>
        /// ≈‰÷√Àµ√˜
        /// </summary>
        [Column("ConfigDesc", ColumnType.Normal)]
        public string ConfigDesc
        {
            get;
            set;
        }

    }
}


