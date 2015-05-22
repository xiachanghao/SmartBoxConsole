using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartBox.Console.Common
{
    public class TypeToHashtable<T> where T : new()
    {
        public static Hashtable ToHashtable(T t)
        {
            Hashtable result = new Hashtable();
            Type tp = typeof(T);
            System.Reflection.PropertyInfo[] tps = tp.GetProperties();
            for (int j = 0; j < tps.Length; ++j)
            {
                result[tps[j].Name] = tps[j].GetValue(t, null);
            }
            return result;
        }
    }
}
