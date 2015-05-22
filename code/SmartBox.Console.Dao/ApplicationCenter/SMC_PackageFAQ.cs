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

    public partial class SMC_PackageFAQDao : BaseDao<SMC_PackageFAQ>
    {

        public SMC_PackageFAQDao(string key)
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

        public virtual bool PostQuestion(string question, int pe_id, string uid, string uname)
        {
            SMC_PackageFAQ faq = new SMC_PackageFAQ();
            faq.pf_askdate = DateTime.Now;
            faq.pf_askemail = "";
            faq.pf_askmobile = "";
            faq.pf_question = question;
            faq.pf_id = this.GetMaxId() + 1;
            faq.pe_id = pe_id;
            faq.pf_uid = uid;
            faq.pf_uname = uname;
            faq.pf_need_syncto_inside = true;
            int i = this.Insert(faq);
            return true;
        }

        public void DeleteByPEID(string pe_id)
        {
            string sql = string.Format("Delete from SMC_PackageFAQ where pe_id='{0}'", pe_id);
            base.ExecuteNonQuery(sql);
        }

        public JsonFlexiGridData QueryPackageFAQList(PageView view, string id)
        {
            string columns = @"pf_id,pf_uname,pf_askdate,pf_question,pf_peplyman";
            string sqlorder;

            string sWhere = "";
            sWhere = string.Format(" and pe_id={0}", id);
           

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  pf_askdate asc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_PackageFAQ", sqlorder, "pf_id", sWhere, view);
        }

        //public int GetMaxId()
        //{
        //    string sql = "select isnull(max(pf_id), 0) pf_id from SMC_PackageFAQ";
        //    object o = this.ExecuteScalar(sql);
        //    int i = 0;
        //    if (o != null)
        //    {
        //        try
        //        {
        //            i = Convert.ToInt32(o);
        //        }
        //        catch (Exception e)
        //        {
        //            i = 0;
        //        }
        //    }
        //    return i;
        //}

        public IList<SMC_PackageFAQ> GetPackageFAQs(int pe_id)
        {
            string sql = "select * from SMC_PackageFAQ";
            return this.Query(sql);
        }

        public IList<SMC_PackageFAQ> GetNeedSyncToInsideFAQ()
        {
            string sql = "select * from SMC_PackageFAQ where pf_need_syncto_inside=1";
            return this.Query(sql);
        }

        public IList<SMC_PackageFAQ> GetNeedSyncToOutsideFAQ()
        {
            string sql = "select * from SMC_PackageFAQ where pf_need_syncto_outside=1";
            return this.Query(sql);
        }
    }
}
