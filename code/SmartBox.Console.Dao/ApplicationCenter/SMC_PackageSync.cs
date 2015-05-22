using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Common.Entities;
using System.Data;
using SmartBox.Console.Common.Entities.Search;
using SmartBox.Console.Common;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;

namespace SmartBox.Console.Dao
{
    public class SMC_PackageSyncDao : BaseDao<SMC_PackageSync>
    {
        public SMC_PackageSyncDao(string key)
            : base(key)
        {

        }

    }
}
