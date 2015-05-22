//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_AutoTableID.cs
// ???????  
//
// ?????   2014-03-05 04:11:17
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


namespace SmartBox.Console.Dao
{

    public class SMC_AutoTableIDDao : BaseDao<SMC_AutoTableID>
    {

        public SMC_AutoTableIDDao(string key)
            : base(key)
        {

        }

        public int GetMaxID(string tableName)
        {
            int i = 0;
            object o = base.ExecuteScalar("SELECT AT_MAXID FROM SMC_AUTOTABLEID WHERE AT_TABLENAME='" + tableName + "'");
            if (o == null)
                i = 0;
            string s = o.ToString();
            if (String.IsNullOrEmpty(s))
                i = 0;
            else
            {
                i = int.Parse(s);
            }
            return i;
        }

        public int GetMaxID(string tableName, string columnNmae)
        {
            string sql = "select isnull(max(" + columnNmae + "), 0) from " + tableName;
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public bool HasMaxID(string tableName)
        {
            string sql = "SELECT COUNT(AT_MAXID) CNT FROM SMC_AUTOTABLEID WHERE AT_TABLENAME = '" + tableName + "'";
            object o = base.ExecuteScalar(sql);
            int i = Convert.ToInt32(o);
            return i > 0;
        }

        public bool InsertMaxID(string tableName, int max_id)
        {
            string sql = "INSERT INTO SMC_AUTOTABLEID values('" + tableName + "', " + max_id.ToString() + ")";
            int i = base.ExecuteNonQuery(sql);
            return i > 0;
        }

        public bool UpdateMaxID(string tableName, int max_id)
        {
            string sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + max_id.ToString() + " WHERE AT_TABLENAME = '" + tableName + "'";
            int i = base.ExecuteNonQuery(sql);
            return i > 0;
        }

        public bool UpdateMaxID(string tableName)
        {
            int maxid = 0;
            tableName = tableName.ToLower();
            int i = 0;
            string sql = "";
            switch (tableName)
            {
                case "smc_packageextsynctooutside":
                    SMC_PackageExtSyncToOutsideDao daoET = new SMC_PackageExtSyncToOutsideDao(base.connectionKey);
                    maxid = daoET.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_packageextsynctooutside'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_functionrole":
                    SMC_FunctionRoleDao daoFR = new SMC_FunctionRoleDao(base.connectionKey);
                    maxid = daoFR.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_functionrole'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_functions":
                    SMC_FunctionsDao daoF = new SMC_FunctionsDao(base.connectionKey);
                    maxid = daoF.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_functions'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_role":
                    SMC_RoleDao daoR = new SMC_RoleDao(base.connectionKey);
                    maxid = daoR.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_role'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_unit":
                    SMC_UnitDao daoU = new SMC_UnitDao(base.connectionKey);
                    maxid = daoU.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_unit'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_userlist":
                    SMC_UserListDao daoUL = new SMC_UserListDao(base.connectionKey);
                    maxid = daoUL.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_userlist'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_packageext":
                    SMC_PackageExtDao peDao = new SMC_PackageExtDao(base.connectionKey);
                    maxid = peDao.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_packageext'";
                    i = base.ExecuteNonQuery(sql);
                    break;
                case "smc_autotableid":
                    maxid = this.GetMaxId();
                    sql = "UPDATE SMC_AUTOTABLEID SET AT_MAXID=" + maxid.ToString() + " WHERE AT_TABLENAME = 'smc_autotableid'";
                    i = base.ExecuteNonQuery(sql);
                    break;
            }
            
            return i > 0;
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(at_id), 0) from smc_autotableid";
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
