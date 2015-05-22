using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SmartBox.Console.Common
{
    public class SelectPagnationEx
    {
        public DataSet Result
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }

        public int PageCount
        {
            get;
            set;
        }

        public int ReturnValue
        {
            get;
            set;
        }
    }

    public class SelectPagnationExDictionary
    {
        public List<IDictionary<string, object>> Result
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }

        public int PageCount
        {
            get;
            set;
        }

        public int ReturnValue
        {
            get;
            set;
        }
    }

    public class SelectPagnationEx<T> where T : new()
    {
        public IList<T> Result
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }

        public int PageCount
        {
            get;
            set;
        }

        public int ReturnValue
        {
            get;
            set;
        }
    }

    public class SelectPagnationExHelper<T> where T : new()
    {
        public static SelectPagnationEx<T> Translate(SelectPagnationEx re)
        {
            SelectPagnationEx<T> result = new SelectPagnationEx<T>();
            result.PageCount = re.PageCount;
            result.RecordCount = re.RecordCount;
            result.ReturnValue = re.ReturnValue;
            result.Result = new List<T>();

            if (re.Result.Tables.Count > 0 && re.Result.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in re.Result.Tables[0].Rows) {
                    T t = new T();
                    Type tp = typeof(T);
                    System.Reflection.PropertyInfo[] properties = tp.GetProperties();
                    if (properties != null && properties.Length > 0)
                    {
                        foreach (System.Reflection.PropertyInfo p in properties)
                        {
                            switch (p.PropertyType.FullName)
                            {
                                //case "System.Int32":
                                //    p.SetValue(t, dr[p.Name], null);
                                //    break;
                                //case "System.String":
                                //    p.SetValue(t, dr[p.Name].ToString(), null);
                                //    break;
                                default:
                                    if (p.Name != "ObjectEntryState")
                                    {
                                        if (dr.IsNull(p.Name))
                                            p.SetValue(t, null, null);
                                        else
                                            p.SetValue(t, dr[p.Name], null);
                                    }
                                    break;
                            }
                            
                        }
                    }
                    result.Result.Add(t);
                }
            }
            return result;
        }
    }

    public class SelectPagnationExDictionaryHelper
    {
        public static SelectPagnationExDictionary Translate(SelectPagnationEx ex)
        {
            SelectPagnationExDictionary result = new SelectPagnationExDictionary();
            result.PageCount = ex.PageCount;
            result.RecordCount = ex.RecordCount;
            result.ReturnValue = ex.ReturnValue;
            result.Result = new List<IDictionary<string, object>>();

            foreach (System.Data.DataRow dr in ex.Result.Tables[0].Rows)
            {
                IDictionary<string, object> Obj = new Dictionary<string, object>();

                foreach (DataColumn clmn in dr.Table.Columns)
                {

                    if (dr.IsNull(clmn.ColumnName))
                        Obj[clmn.ColumnName] = null;
                    else
                        Obj[clmn.ColumnName] = dr[clmn.ColumnName];
                }
                result.Result.Add(Obj);
            }
            return result;
        }
    }
}
