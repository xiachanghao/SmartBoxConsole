//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_Role.cs
// ???????  
//
// ?????   2014-03-05 04:11:44
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


namespace SmartBox.Console.Dao
{

    public class SMC_RoleDao : BaseDao<SMC_Role>
    {

        public SMC_RoleDao(string key)
            : base(key)
        {

        }

        /// <summary>
        /// 按Unit_ID查询子单位列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        public JsonFlexiGridData QueryRolesByUnitID(PageView view, string Unit_ID)
        {
            string column = "Role_Name,Role_CreatedTime,Role_Sequence,Role_ID";

            string sWhere = "";
            if (!String.IsNullOrEmpty(Unit_ID))
            {
                sWhere = string.Format(" and Unit_ID='{0}'", Unit_ID);
            }

            return base.QueryDataForFlexGridByPager(column, "SMC_Role", view.OrderBy.ToString(), "Role_ID", sWhere, view);
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(role_id), 0) from smc_role";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }


    }
}
