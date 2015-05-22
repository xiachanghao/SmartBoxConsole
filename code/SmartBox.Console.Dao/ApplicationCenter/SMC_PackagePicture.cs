using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using System.Data;
using SmartBox.Console.Common.Entities.Search;
using SmartBox.Console.Common;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public partial class SMC_PackagePictureDao : BaseDao<SMC_PackagePicture>
    {
        public SMC_PackagePictureDao(string key)
            : base(key)
        {

        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(pp_id), 0) pp_id from SMC_PackagePicture";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                try
                {
                    i = Convert.ToInt32(o);
                }
                catch (Exception e)
                {
                    i = 0;
                }
            }
            return i;
        }

        public IList<SMC_PackagePicture> GetPackagePictures(int pe_id)
        {
            string sql = "select * from SMC_PackagePicture where pe_id=@peid";
            Hashtable pars = new Hashtable();
            pars.Add("peid", pe_id);
            return this.Query(sql, pars);
        }
    }
}
