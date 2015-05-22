using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz.Entry;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Common.Entities
{
    [Table("SMC_PushDll", SorMappingType.ByAttributes)]
    [Serializable]
    public class SMC_PushDll : BaseEntry
    {
        ///
        ///????????int
        ///


        [Column("pd_id", ColumnType.PrimaryKey)]
        public int pd_id
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pd_name", ColumnType.Normal)]
        public string pd_name
        {
            get;
            set;
        }

        [Column("pd_dll_filename", ColumnType.Normal)]
        public string pd_dll_filename
        {
            get;
            set;
        }
        
        [Column("pd_xml_filename", ColumnType.Normal)]
        public string pd_xml_filename
        {
            get;
            set;
        }

        [Column("pd_zip_filename", ColumnType.Normal)]
        public string pd_zip_filename
        {
            get;
            set;
        }

        [Column("pd_zip_extension", ColumnType.Normal)]
        public string pd_zip_extension
        {
            get;
            set;
        }

        [Column("pd_zip_contenttype", ColumnType.Normal)]
        public string pd_zip_contenttype
        {
            get;
            set;
        }

        [Column("pd_zip_size", ColumnType.Normal)]
        public int pd_zip_size
        {
            get;
            set;
        }


        ///
        ///????????varchar
        ///


        [Column("pd_path", ColumnType.Normal)]
        public string pd_path
        {
            get;
            set;
        }


        ///
        ///????????int
        ///


        [Column("pd_status", ColumnType.Normal)]
        public bool pd_status
        {
            get;
            set;
        }

        [Column("pd_dll_status", ColumnType.Normal)]
        public string pd_dll_status
        {
            get;
            set;
        }


        [Column("pd_createdtime", ColumnType.Normal)]
        public DateTime pd_createdtime
        {
            get;
            set;
        }

        [Column("pd_updatetime", ColumnType.Normal)]
        public DateTime pd_updatetime
        {
            get;
            set;
        }


    }
}


