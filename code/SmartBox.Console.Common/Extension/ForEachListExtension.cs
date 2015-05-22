using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public static class ForEachListExtension
    {
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (action != null)
            {
                foreach (TSource item in source)
                {
                    action(item);
                }
            }
            return source;
        }
    }
}
