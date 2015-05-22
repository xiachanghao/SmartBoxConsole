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
using System.Data.Common;
using System.Data.SqlClient;

namespace SmartBox.Console.Dao
{

    public partial class SMC_PackageExtDao : BaseDao<SMC_PackageExt>
    {

        public SMC_PackageExtDao(string key)
            : base(key)
        {

        }

        public DataSet GetCollectedPackage(string uid)
        {
            string sql = "select ext.* from smc_collect c join smc_packageext ext on ext.pe_id=c.pe_id where c.uid=@uid";
            DbParameter[] pars = new DbParameter[]{
                new SqlParameter("@uid", uid)
            };
            
            return this.ExecuteDataset(sql, pars);
        }

        //public IList<SMC_User> QueryChildUnitsByUnitID(int Unit_ID)
        //{
        //    string sql = string.Format("SELECT * FROM SMC_User WHERE Upper_Unit_ID=@unitid order by Unit_Sequence", Unit_ID);
        //    Hashtable pars = new Hashtable();
        //    pars.Add("unitid", Unit_ID);

        //    return base.Query(sql, pars);
        //}    

        //public SMC_User GetUser(string uid)
        //{
        //    string sql = "SELECT * FROM SMC_User WHERE U_UID=@uid";
        //    Hashtable pars = new Hashtable();
        //    pars.Add("uid", uid);

        //    IList<SMC_User> r = base.Query(sql, pars);
        //    if (r.Count <= 0)
        //        return null;
        //    else
        //        return r[0];
        //}

        public int GetMaxId()
        {
            string sql = "select isnull(max(pe_id),0) pe_id from SMC_PackageExt";
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

        public string GetPeId(string tableId, string tableName)
        {
            string sql = "select pe_id from SMC_PackageExt where tableId=" + tableId + "and tableName='" + tableName + "'";
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
                    i = -1;
                }
            }
            return i.ToString();
        }

        public IList<SMC_PackageExt> GetNeedSyncPackageList()
        {
            string sql = "select * from smc_packageext";
            return this.Query(sql);
        }

        public IList<SMC_PackageExt> GetNotP4I()
        {
            string sql = "select ext.* from smc_packageext ext";

            return this.Query(sql);
        }

        public IList<SMC_PackageExt> GetAllPackageList(string clientType)
        {
            if (String.IsNullOrEmpty(clientType))
            {
                string sql = "select ext.* from smc_packageext ext where ext.pe_usefulststus=1 order by pe_id desc";
                
                return this.Query(sql);
            }
            else
            {
                string sql = "select ext.* from smc_packageext ext  where ext.pe_usefulststus=1 and pe_clienttype=@clientType order by pe_id desc";
                Hashtable pars = new Hashtable();
                pars.Add("clientType", clientType);
                return this.Query(sql, pars);
            }
        }

        public int GetBBPackageCount()
        {
            string sql = "select count(pe_id) cnt from smc_packageext ext where pe_isbb=1 and pe_usefulststus=1";

            object o = this.ExecuteScalar(sql);
            return Convert.ToInt32(o);
        }

        public int GetTJPackageCount()
        {
            string sql = "select count(pe_id) cnt from smc_packageext ext where pe_istj=1 and pe_usefulststus=1";

            object o = this.ExecuteScalar(sql);
            return Convert.ToInt32(o);
        }

        public int GetSCPackageCount(string uid)
        {
            string sql = "select count(clt_id) cnt from smc_collect where uid=@uid";
            Hashtable pars = new Hashtable();
            pars.Add("uid", uid);
            object o = this.ExecuteScalar(sql, pars);
            return Convert.ToInt32(o);
        }

        public SMC_PackageExt getPackage(string tableName, string tableId)
        {
            string sql = "select * from smc_packageext where tableName=@tableName and tableId=@tableId";
            Hashtable pars = new Hashtable();
            pars.Add("tableName", tableName);
            pars.Add("tableId", tableId);

            IList < SMC_PackageExt> r = this.Query(sql, pars);
            if (r != null && r.Count > 0)
                return r[0];
            else
                return null;
        }

        public SelectPagnationExDictionary GetApplicationExtList(string appName, string application, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, string categoryID, string orderby, int pageSize, int pageIndex)
        {
            string where = "";

            if (!String.IsNullOrEmpty(categoryID))
            {
                where += " and pe.pe_CategoryID like '%" + categoryID + "%'";
            }

            if (!String.IsNullOrEmpty(appName))
            {
                where += " and pe.pe_displayName like '%" + appName + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and pe.pe_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(u_unitcode))
            {
                where += " and pe.pe_unitcode like '%" + u_unitcode + "%'";
            }

            if (!String.IsNullOrEmpty(application))
            {
                where += " and pe.pe_ApplicationName like '%" + application + "%'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_start))
            {
                where += " and pe.pe_AuthSubmitTime >= '" + u_auth_submit_time_start + "'";
            }

            if (!String.IsNullOrEmpty(u_auth_submit_time_end))
            {
                where += " and pe.pe_AuthSubmitTime <= '" + u_auth_submit_time_end + "'";
            }

            if (!String.IsNullOrEmpty(orderby))
            {
                //orderby = " order by " + orderby;
            }
            //string sql = String.Format("select * from [dbo].SMC_User where 1=1 {0}", where);
            if (where.StartsWith(" and ", StringComparison.CurrentCultureIgnoreCase))
                where = where.Substring(5);
            string maindbName = DbSqlHelper.GetStatisticDBName();
            SelectPagnationExDictionary result = this.SelectPaginationExDictionary(@"" + maindbName + @".dbo.smc_packageext pe", "*", pageIndex + 1, pageSize, orderby, where, "");


            return result;
        }
    }
}
