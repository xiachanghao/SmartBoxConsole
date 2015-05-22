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

    public class GlobalParamDao : BaseDao<GlobalParam>
    {

        public GlobalParamDao(string key)
            : base(key)
        {

        }


        public IList<GlobalParam> LoadGlobalParam()
        {
            return this.QuerySql("select * from GlobalParam");
        }

        public bool Exists(GlobalParam parm)
        {
            string sql = "select count(configkey) from Globalparam where configkey=@configkey";
            Hashtable par = new Hashtable();
            par.Add("configkey", parm.ConfigKey);
            object o = this.ExecuteScalar(sql, par);
            return Convert.ToInt32(o) > 0;
        }

        public GlobalParam GetGlobalParam(string key)
        {
            List<KeyValuePair<string, object>> pars = new List<KeyValuePair<string,object>>();
            pars.Add(new KeyValuePair<string, object>("ConfigKey", key));
            GlobalParam gParam = this.Get(pars);
            return gParam;
        }
    }
}
