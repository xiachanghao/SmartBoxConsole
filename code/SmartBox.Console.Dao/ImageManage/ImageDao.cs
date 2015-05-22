using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Dao
{
    public class ImageDao:BaseDao<Image>
    {
        public ImageDao(string key)
            : base(key)
        { }

        public IList<Image> QueryImageList()
        {
            string sql = "select id from Image";
            return base.Query(sql);
        }

        public IList<Image> QueryByHashCode(string hashCode)
        {
            string sql = "select * from Image where HashCode = '" + hashCode+"'";
            return base.Query(sql);
        }
    }
}
