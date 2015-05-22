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

    public partial class SMC_Package4OutDao : BaseDao<SMC_Package4Out>
    {

        public SMC_Package4OutDao(string key)
            : base(key)
        {

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
            string sql = "select max(po_id) from SMC_Package4Out";
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

        public JsonFlexiGridData QueryOutPackageList(PageView view)
        {
            //加入下载数
            string columns = @"po_ID,DisplayName,pe_DownCount,Type,ClientType,Description,Version";
            string sqlorder;
            string sql = @"select po.po_ID,po.DisplayName,pe.pe_DownCount,po.Type,po.ClientType,po.Description,po.Version
  from SMC_Package4Out po left join SMC_PackageExt pe on pe.TableName='SMC_Package4Out' and po.po_id = pe.tableid";
            
            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  po_ID asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, string.Format("({0}) as temp", sql), sqlorder, "po_ID", "", view);
        }

        public JsonFlexiGridData QueryPackageGifList(PageView view, string id)
        {
            string columns = @"pp_id,pp_title,pp_desc,pp_path,pp_CreatedDate";
            string sqlorder;

            string sWhere = "";
            sWhere = string.Format(" and pe_id={0}", id);
           

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  pp_CreatedDate asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_PackagePicture", sqlorder, "pp_id", sWhere, view);

        }
    }
}
