//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_Unit.cs
// ???????  
//
// ?????   2014-03-05 04:11:53
//
// ?????
// ?????
//----------------------------------------------------------------
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

    public class SMC_PackageExtHistoryDao : BaseDao<SMC_PackageExtHistory>
    {

        public SMC_PackageExtHistoryDao(string key)
            : base(key)
        {

        }


        public IList<SMC_PackageExtHistory> LoadSMC_PackageExtHistory()
        {
            return this.QuerySql("select * from SMC_PackageExtHistory");
        }

        public bool Exists(SMC_PackageExtHistory parm)
        {
            string sql = "select count(id) from SMC_PackageExtHistory where pe_id=@pe_id and pe_version=@pe_version";
            Hashtable par = new Hashtable();
            par.Add("pe_id", parm.pe_id);
            par.Add("pe_version", parm.pe_Version);
            object o = this.ExecuteScalar(sql, par);
            return Convert.ToInt32(o) > 0;
        }
    }
}
