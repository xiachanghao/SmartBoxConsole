using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.Collections;

namespace SmartBox.Console.Dao
{
    public partial class SMC_PackagePictureDao : BaseDao<SMC_PackagePicture>
    {
        public IList<SMC_PackagePicture> GetList(int pe_id)
        {
            string sql = "select * from smc_packagepicture where pe_id=@peid";
            Hashtable pars = new Hashtable();
            pars.Add("peid", pe_id);
            return this.Query(sql, pars);
        }
    }
}
