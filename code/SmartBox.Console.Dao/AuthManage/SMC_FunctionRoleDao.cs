//----------------------------------------------------------------
// Copyright (C) 2012 ???????????
// ????.
// All rights reserved.
//
// ????	    DAOdbo.SMC_FunctionRole.cs
// ???????  
//
// ?????   2014-03-05 04:11:21
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
using System.Collections;


namespace SmartBox.Console.Dao
{

    public class SMC_FunctionRoleDao : BaseDao<SMC_FunctionRole>
    {

        public SMC_FunctionRoleDao(string key)
            : base(key)
        {

        }

        public bool Exists(int frId)
        {
            string sql = String.Format("select count(fr_id) cnt from smc_functionrole where fr_id={0}", frId);
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }

        

        public bool Exists(int functionId, int roleId)
        {
            string sql = String.Format("select count(fr_id) cnt from smc_functionrole where role_id={0} and fn_id={1}", roleId, functionId);
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }

        public bool IsFunctionInRole(int functionId, int roleId)
        {
            string sql = String.Format("select count(fr_id) cnt from smc_functionrole where role_id={0} and fn_id={1}", roleId, functionId);
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }

        public bool IsFuncAssigned(int roleId)
        {
            string sql = String.Format("select count(fr_id) cnt from smc_functionrole where role_id={0} ", roleId);
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }

        public int GetMaxId()
        {
            string sql = "select isnull(max(fr_id), 0) fr_id from smc_functionrole";
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public bool Delete(int functionId, int roleId)
        {
            string sql = String.Format("delete from smc_functionrole where role_id={0} and fn_id={1}", roleId, functionId);
            object o = this.ExecuteNonQuery(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }

        public bool DeleteByFID(int functionId)
        {
            string sql = String.Format("delete from smc_functionrole where fn_id={0}", functionId);
            object o = this.ExecuteNonQuery(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }

            return i > 0;
        }
    }
}
