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

    public class SMC_UnitDao : BaseDao<SMC_Unit>
    {

        public SMC_UnitDao(string key)
            : base(key)
        {

        }

        /// <summary>
        /// 按Unit_ID查询子单位列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryUnitByUnitCode(PageView view, string Unit_ID)
        {
            string column = @"*";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Unit_ID))
            {
                sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_Unit", view.OrderBy.ToString(), "Unit_ID", sWhere, view);
        }

        public JsonFlexiGridData QueryUnitByUpperUnitCode(PageView view, string Upper_Unit_ID)
        {
            string column = "Unit_Name,Unit_Demo,Unit_CreatedTime,Unit_CreatedUser,Unit_Sequence,Unit_ID";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Upper_Unit_ID))
            {
                sWhere = string.Format(" and Upper_Unit_ID='{0}'", Upper_Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_Unit", view.OrderBy.ToString(), "Unit_ID", sWhere, view);
        }
        
        //单位管理员只能查看自己单位的组织列表
        public JsonFlexiGridData QueryUnitByUpperUnitCodeWithSelfUnit(PageView view, string Upper_Unit_ID, string unit_id_self)
        {
            string rootOrg = "";
            string column = "Unit_Name,Unit_Demo,Unit_CreatedTime,Unit_CreatedUser,Unit_Sequence,Unit_ID";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Upper_Unit_ID) && Upper_Unit_ID != rootOrg)
            {
                sWhere = string.Format(" and Upper_Unit_ID='{0}' and (unit_path like '%{2}..%' or unit_path = '{3}')", Upper_Unit_ID, unit_id_self, Upper_Unit_ID, Upper_Unit_ID);
            }
            else if (Upper_Unit_ID == rootOrg)
            {
                sWhere = string.Format(" and Unit_ID='{0}'", unit_id_self);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_Unit", view.OrderBy.ToString(), "Unit_ID", sWhere, view);
        }

        public IList<SMC_Unit> QueryChildUnitsByUnitID(string Unit_ID, int maxcount)
        {
            string sql = "";
            sql = string.Format("SELECT * FROM SMC_Unit WHERE Upper_Unit_ID=@unitid order by Unit_Sequence", Unit_ID);
            if (maxcount > 0)
            {
                sql = string.Format("SELECT top {0} * FROM SMC_Unit WHERE Upper_Unit_ID=@unitid order by Unit_Sequence", maxcount);
            }
            Hashtable pars = new Hashtable();
            pars.Add("unitid", Unit_ID);

            return base.Query(sql, pars);
        }

        public IList<SMC_Unit> GetAllUnits()
        {
            string sql = "";
            sql = string.Format("SELECT * FROM SMC_Unit order by Unit_Sequence");      
            

            return base.Query(sql);
        }   

        public int GetMaxId()
        {
            string sql = "select isnull(max(unit_id), 0) from smc_unit";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public IList<SMC_Unit> GetNeedSyncSMC_Units()
        {
            string sql = "select * from smc_unit";
            return this.Query(sql);
        }
    }
}
