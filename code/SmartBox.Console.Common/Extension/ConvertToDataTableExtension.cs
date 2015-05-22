using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace SmartBox.Console.Common
{
    public static class ConvertToDataTableExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <exception cref="ArgumentOutOfRangeException">当参数中的类型没有开放属性时会引发异常</exception>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> list) where T:class,new()
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);

            List<string> propertyList = new List<string>();
            foreach (PropertyInfo property in type.GetProperties())
            {
                DataColumn column = new DataColumn(property.Name);
                dt.Columns.Add(column);
                propertyList.Add(property.Name);
            }
            if (propertyList.Count<=0)
            {
                throw new ArgumentOutOfRangeException(type.Name, "参数中没有开放的属性,无法将对象集合转换成DateTable");
            }

            foreach (T item in list)
            {
                DataRow row = dt.NewRow();
                foreach (string columnName in propertyList)
                {
                    row[columnName] = type.GetProperty(columnName).GetValue(item, null);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        public static DataTable ToDataTable(this Enum t, string keyColumnName, string valueColumnName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(keyColumnName);
            dt.Columns.Add(valueColumnName);

            foreach (string key in Enum.GetNames(t.GetType()))
            {
                DataRow row = dt.NewRow();
                row[keyColumnName] = key;
                row[valueColumnName] = (int)Enum.Parse(t.GetType(), key);
                dt.Rows.Add(row);
            }

            return dt;
        }

        public static DataTable ToDataTable<T>(this T[] objArry, string columnName) where T:struct
        {
            return ConvertValueTypeToDataTable<T>(objArry, columnName);
        }

        public static DataTable ToDataTable(this string[] str, string columnName)
        {
            return ConvertValueTypeToDataTable<string>(str, columnName);
        }

        public static DataTable ToDataTable<TK, TV>(this IDictionary<TK, TV> dic, string keyColumnName, string valueColumnName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(keyColumnName);
            dt.Columns.Add(valueColumnName);

            foreach (TK key in dic.Keys)
            {
                DataRow row = dt.NewRow();
                row[keyColumnName] = key;
                row[valueColumnName] = dic[key];
                dt.Rows.Add(row);
            }

            return dt;
        }

        private static DataTable ConvertValueTypeToDataTable<T>(T[] objArry, string columnName)
        {
            DataTable dt = new DataTable();
            //初始化列数据
            dt.Columns.Add(new DataColumn(columnName));

            foreach (T item in objArry)
            {
                DataRow row = dt.NewRow();
                row[columnName] = item;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
