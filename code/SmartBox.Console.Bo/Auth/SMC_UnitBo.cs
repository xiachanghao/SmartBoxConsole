//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_UnitBo.cs
// 
// 
// 
// 2014-03-05 04:11:53
//
// 
// 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Beyondbit.Framework.Biz;
using Beyondbit.Framework.Biz.BO;
using Beyondbit.Framework.Core.InterceptorHandler;
using Beyondbit.Framework.DataAccess;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using SmartBox.Console.Dao;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartBox.Console.Bo
{

    public class SMC_UnitBo : BaseBO<SMC_Unit>
    {
        public SMC_UnitBo()
        {
            base._dao = this.SMC_UnitDao;
        }

        private SMC_UnitDao _SMC_UnitDao;
        protected SMC_UnitDao SMC_UnitDao
        {
            get
            {
                if (_SMC_UnitDao == null)
                {
                    _SMC_UnitDao = new SMC_UnitDao(AppConfig.statisticDBKey);
                }
                return _SMC_UnitDao;
            }
        }

        /// <summary>
        /// 判断是否有继承关系
        /// </summary>
        /// <param name="current_unit_id"></param>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        [Frame(false, false)]
        private  bool IsInhre(string current_unit_id, string unit_id)
        {
            SMC_Unit cu = SMC_UnitDao.Get(current_unit_id);
            if (cu == null)
            {
                return true;
            }
            else
            {
                return cu.Upper_Unit_ID == unit_id || cu.Unit_ID==unit_id || IsInhre(cu.Upper_Unit_ID, unit_id);
            }
        }

        [Frame(false, false)]
        public virtual Hashtable QueryUnitTreeData(string t, string root_unit_id, string current_user_unit_id, bool need_filter_unit)
        {
            try
            {
                IList<SMC_Unit> units = SMC_UnitDao.QueryChildUnitsByUnitID(root_unit_id, -1);
              
                List<Hashtable> objs = new List<Hashtable>();
                
                if (units != null && units.Count > 0)
                    foreach (SMC_Unit fn in units)
                    {
                        if (need_filter_unit && fn.Unit_ID != current_user_unit_id)
                            continue;
                        Hashtable hash = new Hashtable();

                        hash["id"] = fn.Unit_ID;
                        hash["name"] = fn.Unit_Name;

                        if (IsInhre(root_unit_id, fn.Unit_ID))
                        {
                            hash["open"] = true;
                        }
                        //hash["checked"] = true;
                        hash["target"] = "ifrm";
                        switch (t)
                        {
                            case "unit":
                                hash["url"] = "/AuthManage/UnitList?upper_unit_id=" + fn.Unit_ID;
                                break;
                            case "user":
                                hash["url"] = "/AuthManage/UserList?unit_id=" + fn.Unit_ID;
                                break;
                            case "role":
                                hash["url"] = "/AuthManage/RoleList?unit_id=" + fn.Unit_ID;
                                break;
                        }
                        
                        //var s = "zTreeOnClick('" + fn.Unit_ID + "')";

                        parseChildTreeData(t, hash, fn, fn.Unit_ID, root_unit_id);

                        objs.Add(hash);
                    }

                Hashtable Hash = new Hashtable();
                Hash["id"] = "";
                Hash["name"] = "根组织";
                Hash["open"] = true;
                Hash["target"] = "ifrm";
                Hash["children"] = objs;
                switch (t)
                {
                    case "unit":
                        Hash["url"] = "/AuthManage/UnitList?upper_unit_id=";
                        break;
                    case "user":
                        Hash["url"] = "/AuthManage/UserList?unit_id=";
                        break;
                    case "role":
                        Hash["url"] = "/AuthManage/RoleList?unit_id=";
                        break;
                }

                return Hash;
            }
            catch (DalException ex)
            {
                throw new BOException("QueryUnitTreeData", ex);
            }
        }

        [Frame(false, false)]
        private void parseChildTreeData(string t, Hashtable pHash, SMC_Unit parent_fncs, string Unit_ID, string current_unit_id)
        {
            IList<SMC_Unit> funcs = SMC_UnitDao.QueryChildUnitsByUnitID(Unit_ID, -1);
                        
            if (funcs != null && funcs.Count > 0)
            {
                List<Hashtable> objs = new List<Hashtable>();
                foreach (SMC_Unit fn in funcs)
                {
                    Hashtable hash = new Hashtable();
                    
                    hash["id"] = fn.Unit_ID;
                    
                    hash["name"] = fn.Unit_Name;

                    if (current_unit_id == "")
                    {
                        hash["open"] = false;
                    }else if (IsInhre(current_unit_id, fn.Unit_ID))
                    {
                        hash["open"] = true;
                    }

                    hash["target"] = "ifrm";
                    switch (t)
                    {
                        case "unit":
                            hash["url"] = "/AuthManage/UnitList?upper_unit_id=" + fn.Unit_ID;
                            break;
                        case "user":
                            hash["url"] = "/AuthManage/UserList?unit_id=" + fn.Unit_ID;
                            break;
                        case "role":
                            hash["url"] = "/AuthManage/RoleList?unit_id=" + fn.Unit_ID;
                            break;
                    }
                    //hash["onclick"] = "zTreeOnClick('" + fn.Unit_ID+"')";
                    parseChildTreeData(t, hash, fn, fn.Unit_ID, current_unit_id);
                    objs.Add(hash);
                }
                if (objs.Count > 0)
                {
                    pHash["children"] = objs;
                }
            }
        }

        /// <summary>
        /// 按Unit_ID查询子单位列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_Code"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUnitByUnitCode(PageView view, string Unit_ID)
        {
            return SMC_UnitDao.QueryUnitByUnitCode(view, Unit_ID);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUnitByUpperUnitCode(PageView view, string Upper_Unit_ID)
        {
            return SMC_UnitDao.QueryUnitByUpperUnitCode(view, Upper_Unit_ID);
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUnitByUpperUnitCodeWithSelfUnit(PageView view, string Upper_Unit_ID, string unit_id_self)
        {
            return SMC_UnitDao.QueryUnitByUpperUnitCodeWithSelfUnit(view, Upper_Unit_ID, unit_id_self);
        }

        [Frame(false, false)]
        public virtual SMC_Unit Get(string Unit_ID)
        {
            return SMC_UnitDao.Get(Unit_ID);
        }

        [Frame(false, false)]
        public virtual IList<SMC_Unit> GetAllUnits()
        {
            return SMC_UnitDao.GetAllUnits();
        }

        [Frame(false, false)]
        public virtual int Save(SMC_Unit entity)
        {
            if (SMC_UnitDao.Get(entity.Unit_ID) == null)
                return SMC_UnitDao.Insert(entity);
            else
                return SMC_UnitDao.Update(entity);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_Unit entity)
        {

            try
            {
                SMC_UnitDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_Unit> entities)
        {

            try
            {
                SMC_UnitDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_Unit unit)
        {
            try
            {
                int i = 0;
                if (SMC_UnitDao.Get(unit.Unit_ID) == null)
                {
                    //i = SMC_UnitDao.Insert(unit);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    //if (autoDao.HasMaxID("SMC_Unit"))
                    //{
                    //    int max_id = autoDao.GetMaxID("SMC_Unit");
                    //    unit.Unit_ID = max_id + 1;
                    //}
                    //else
                    //{
                    //    unit.Unit_ID = 1;
                    //}
                    Beyondbit.BUA.Client.Org org = new Beyondbit.BUA.Client.Org();
                    org.OrgName = unit.Unit_Name;
                    org.OrgType = "Unit_Org";
                    
                    Beyondbit.BUA.Client.IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();

                    if (String.IsNullOrEmpty(unit.Upper_Unit_ID))
                    {
                        Beyondbit.BUA.Client.Org topOrg = null;
                        try
                        {
                            topOrg = os.GetTopOrg();
                        }
                        catch (Exception ex)
                        {
                            throw new BOException("调用统一授权接口失败:" + ex.Message, ex);
                        }
                        org.ParentOrgCode = topOrg.OrgCode;
                        //org.OrgCode = NPinyin.Pinyin.GetInitials(unit.Unit_Name, System.Text.Encoding.UTF8);
                        org.OrgCode = "smc_" + NPinyin.Pinyin.GetInitials(unit.Unit_Name, System.Text.Encoding.UTF8);
                        unit.Upper_Unit_ID = "";
                    }
                    else
                    {
                        org.ParentOrgCode = unit.Upper_Unit_ID;
                        //org.OrgCode = unit.Upper_Unit_ID + "_" + NPinyin.Pinyin.GetInitials(unit.Unit_Name, System.Text.Encoding.UTF8);
                        org.OrgCode = "smc_" + NPinyin.Pinyin.GetInitials(unit.Unit_Name, System.Text.Encoding.UTF8);
                    }

                    try
                    {
                        os.AddOrg(org);
                    }
                    catch (Exception ex)
                    {
                        throw new BOException("调用统一授权借口失败:" + ex.Message, ex);
                    }
                                        
                    unit.Unit_ID = org.OrgCode;

                    try
                    {
                        i = SMC_UnitDao.Insert(unit);
                    }
                    catch (Exception ex)
                    {
                        throw new BOException("调用方法InsertOrUpdate失败:" + ex.Message, ex);
                    }
                    if (true)
                    {
                        //autoDao.UpdateMaxID("SMC_Unit");
                    }
                }
                else
                {
                    Beyondbit.BUA.Client.IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
                    Beyondbit.BUA.Client.Org org = os.GetOrgBaseInfo(Beyondbit.BUA.Client.ObjectType.Org, unit.Unit_ID);
                    org.OrgName = unit.Unit_Name;
                    os.ModiOrg(org);
                    unit.Unit_UpdateTime = DateTime.Now;
                   
                    i = SMC_UnitDao.Update(unit);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_Unit unit)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                //if (autoDao.HasMaxID("SMC_Unit"))
                //{
                //    int max_id = autoDao.GetMaxID("SMC_Unit");
                //    unit.Unit_ID = max_id + 1;
                //}
                //else
                //{
                //    unit.Unit_ID = 1;
                //}
                int i = SMC_UnitDao.Insert(unit);
                if (i > 0)
                {
                    //autoDao.UpdateMaxID("SMC_Unit");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_Unit unit)
        {
            try
            {
                int i = SMC_UnitDao.Update(unit);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }


        public virtual void GetUnitHashTree(Hashtable pHash)
        {
            _GetUnitHashTree(pHash, "");
        }

        private void _GetUnitHashTree(Hashtable pHash, string parentUnitID)
        {
            IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            pars.Add(new Tuple<string, string, object>("upper_unit_id", "=", parentUnitID));
            List<SMC_Unit> units = SMC_UnitDao.QueryList(pars);
            List<Hashtable> lst = new List<Hashtable>();

            if (units != null && units.Count > 0)
                foreach (SMC_Unit unit in units)
                {
                    Hashtable horg = new Hashtable();
                    horg["id"] = unit.Unit_ID;
                    horg["text"] = unit.Unit_Name;
                    lst.Add(horg);
                    _GetUnitHashTree(horg, unit.Unit_ID);
                }
            pHash["children"] = lst;
        }
    }
}
