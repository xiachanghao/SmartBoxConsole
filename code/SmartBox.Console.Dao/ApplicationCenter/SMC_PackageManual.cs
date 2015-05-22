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

    public partial class SMC_PackageManualDao : BaseDao<SMC_PackageManual>
    {

        public SMC_PackageManualDao(string key)
            : base(key)
        {

        }

        public JsonFlexiGridData QueryPackageManualList(PageView view, string id)
        {
            string columns = @"pm_id,pm_name,pm_url,pm_createdtime,pm_updatetime";
            string sqlorder;

            string sWhere = "";
            sWhere = string.Format(" and pe_id={0}", id);


            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  pm_createdtime asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_PackageManual", sqlorder, "pm_id", sWhere, view);

        }


        public int GetMaxId()
        {
            string sql = "select isnull(max(pm_id), 0) pm_id from SMC_PackageManual";
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

        public IList<SMC_PackageManual> GetPackageManuals(int packageid)
        {
            string sql = "select * from smc_packagemanual where pe_id=@packageid";
            Hashtable pars = new Hashtable();
            pars.Add("packageid", packageid);
            return this.Query(sql, pars);
        }
    }
}
