//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_BUAUserSyncToInsideBO.cs
// 
// 
// 
// 2014-06-06 14:02:53
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
using System.Collections;
using Beyondbit.BUA.Client;
using System.Configuration;

namespace SmartBox.Console.Bo
{
    public class SMC_BUAUserSyncToInsideBO : BaseBO
    {
        private SMC_BUAUserSyncToInsideDao _SMC_BUAUserSyncToInsideDao;
        protected SMC_BUAUserSyncToInsideDao SMC_BUAUserSyncToInsideDao
        {
            get
            {
                if (_SMC_BUAUserSyncToInsideDao == null)
                {
                    _SMC_BUAUserSyncToInsideDao = new SMC_BUAUserSyncToInsideDao(AppConfig.statisticDBKey);
                }
                return _SMC_BUAUserSyncToInsideDao;
            }
        }

        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryBUAUserAsyncToInsideResultList(PageView pageview, string sync_bat_no, string sync_time_start, string sync_time_end, string sync_status, string userName)
        {
            try
            {
                return SMC_BUAUserSyncToInsideDao.QueryBUAUserAsyncToInsideResultList(pageview, sync_bat_no, sync_time_start, sync_time_end, sync_status, userName);
            }
            catch (DalException ex)
            {
                throw new BOException("查询包的同步结果出错", ex);
            }
        }

        [Frame(false, false)]
        public virtual void BUAUserSyncToOutside(string ids)
        {
            SMC_UserDao userDao = new SMC_UserDao(AppConfig.statisticDBKey);
            string[] idlist = ids.Split(",".ToCharArray());
            List<SMC_User> usersToBeSynced = new List<SMC_User>();
            
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                {
                    continue;
                }
                SMC_User u = userDao.GetUser(uid);
                if (u != null)
                {
                    usersToBeSynced.Add(u);
                }
            }

            if (usersToBeSynced == null || usersToBeSynced.Count == 0)
                return;

            SmartBox.Console.Service.ApplicationCenterWS.WebService ws = new Service.ApplicationCenterWS.WebService();

            Service.ApplicationCenterWS.SMC_User[] users = new Service.ApplicationCenterWS.SMC_User[usersToBeSynced.Count];
            int i = 0;
            foreach (SMC_User user in usersToBeSynced)
            {
                users[i] = new Service.ApplicationCenterWS.SMC_User();
                users[i].U_CREATEDDATE = user.U_CREATEDDATE;
                users[i].U_ID = user.U_ID;
                users[i].U_NAME = user.U_NAME;
                users[i].U_PASSWORD = user.U_PASSWORD;
                users[i].U_UID = user.U_UID;
                users[i].U_UNITCODE = user.U_UNITCODE;

                ++i;
            }
            bool result = ws.SMC_UserSync(users);
            
            SMC_BUAUserSyncToOutsideDao buaUserSyncOutDao = new SMC_BUAUserSyncToOutsideDao(AppConfig.statisticDBKey);
            int batNo = buaUserSyncOutDao.GetMaxBatNo() + 1;
            foreach (SMC_User user in usersToBeSynced)
            {
                SMC_BUAUserSyncToOutside log = new SMC_BUAUserSyncToOutside();
                log.description = "同步" + (result ? "成功" : "失败");
                log.sync_bat_no = batNo;
                log.sync_status = result;
                log.sync_time = DateTime.Now;
                log.sync_user_name = "";
                log.sync_user_uid = "";
                log.user_name = user.U_NAME;
                log.user_uid = user.U_UID;

                try
                {
                    int maxId = buaUserSyncOutDao.GetMaxId() + 1;
                    log.buso_id = maxId;
                    buaUserSyncOutDao.Insert(log);
                    buaUserSyncOutDao.UpdateMaxId();
                }
                catch
                {
                }
            }
        }

        [Frame(false, false)]
        public virtual void BUAUserSyncToInside(string ids, Hashtable result)
        {
            ServiceFactory factory = Beyondbit.BUA.Client.ServiceFactory.Instance();
            IUserService us = factory.GetUserService();
            IOrgService os = factory.GetOrgService();
            SMC_UserDao userDao = new SMC_UserDao(AppConfig.statisticDBKey);
            SMC_BUAUserSyncToInsideDao buaSyncInsideDao = new SMC_BUAUserSyncToInsideDao(AppConfig.statisticDBKey);
            GlobalParamDao globalParamDao = new GlobalParamDao(AppConfig.statisticDBKey);
            GlobalParam p = globalParamDao.GetGlobalParam("user_default_status");
            int batNo = 0;
            try
            {
                batNo = buaSyncInsideDao.GetMaxBatNo();
            }
            catch (Exception e)
            {
            }
            batNo += 1;

            string[] idlist = ids.Split(",".ToCharArray());
            foreach (string uid in idlist)
            {
                if (String.IsNullOrEmpty(uid))
                {
                    continue;
                }
                Beyondbit.BUA.Client.User _u = null;
                try
                {
                    _u = us.GetUserInfo(uid);
                }
                catch (Exception ex)
                {
                    result["Msg"] += uid + ex.Message;
                    continue;
                }

                SMC_BUAUserSyncToInside log = new SMC_BUAUserSyncToInside();
                log.sync_bat_no = batNo;

                try
                {
                    log.sync_status = true;
                    log.sync_time = DateTime.Now;
                    log.sync_user_name = "";
                    log.sync_user_uid = "";
                    log.user_name = _u.UserFullName;
                    log.user_uid = _u.UserUid;
                    log.description = "同步成功";
                    log.busi_id = buaSyncInsideDao.GetMaxId() + 1;
                    SMC_UnitDao unitDao = new SMC_UnitDao(AppConfig.statisticDBKey);
                    
                    SMC_User u = null;
                    u = userDao.GetUser(_u.UserUid);
                    if (u == null)
                    {
                        u = new SMC_User();
                        u.U_NAME = _u.UserFullName;
                        u.U_PASSWORD = _u.UserPassword;
                        u.U_UID = _u.UserUid;
                        int id = userDao.GetMaxId() + 1;
                        u.U_ID = id;
                        string unitCode = os.GetUnitCode(ObjectType.User, u.U_UID);
                        Org unitOrg = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
                        u.U_UNITCODE = unitCode;

                        //List<KeyValuePair<string, string>> pars = new List<KeyValuePair<string,string>>();
                        //pars.Add(new KeyValuePair<string, string>("unit_id",unitCode));
                        SMC_Unit unit = unitDao.Get(unitCode);
                        if (unit == null)
                        {
                            unit = new SMC_Unit();
                            unit.Unit_ID = unitCode;
                            unit.Unit_Name = unitOrg.OrgName;
                            unit.Unit_Sequence = 0;
                            unit.Unit_UpdateTime = DateTime.Now;
                            unit.Unit_CreatedTime = DateTime.Now;
                            unit.Upper_Unit_ID = "";
                            unit.Unit_Path = unitCode;
                            unitDao.Insert(unit);
                        }
                        u.u_unitname = unitOrg.OrgName;
                        u.U_CREATEDDATE = DateTime.Now;
                        u.u_enable_status = p.ConfigValue == "1" ? UserEnabledStatus.Enabled : UserEnabledStatus.UnAuthed;
                        u.u_auth_submit_time = DateTime.Now;
                        u.u_need_sync = false;
                        userDao.Insert(u);
                        userDao.UpdateMaxId();
                    }
                    else
                    {
                        u.U_NAME = _u.UserFullName;
                        u.U_PASSWORD = _u.UserPassword;
                        u.U_CREATEDDATE = DateTime.Now;
                        string unitCode = os.GetUnitCode(ObjectType.User, u.U_UID);
                        Org unitOrg = os.GetOrgBaseInfo(ObjectType.Org, unitCode);

                        //List<KeyValuePair<string, string>> pars = new List<KeyValuePair<string, string>>();
                        //pars.Add(new KeyValuePair<string, string>("unit_id", unitCode));
                        SMC_Unit unit = unitDao.Get(unitCode);
                        if (unit == null)
                        {
                            unit = new SMC_Unit();
                            unit.Unit_ID = unitCode;
                            unit.Unit_Name = unitOrg.OrgName;
                            unit.Unit_Sequence = 0;
                            unit.Unit_UpdateTime = DateTime.Now;
                            unit.Unit_CreatedTime = DateTime.Now;
                            unit.Upper_Unit_ID = "";
                            unit.Unit_Path = unitCode;
                            unitDao.Insert(unit);
                        }

                        
                        u.u_unitname = unitOrg.OrgName;
                        u.U_UNITCODE = unitCode;
                        u.u_enable_status = p.ConfigValue == "1" ? UserEnabledStatus.Enabled : UserEnabledStatus.UnAuthed;
                        u.u_auth_submit_time = DateTime.Now;
                        u.u_need_sync = false;
                        u.u_update_time = DateTime.Now;
                        userDao.Update(u);
                    }
                    
                }
                catch (Exception ex)
                {
                    log.sync_status = false;
                    log.description = "同步失败,原因可能是:" + ex.Message + "。\r\nStackTrace:\r\n" + ex.StackTrace;
                    if (result != null)
                    {
                        result["IsSuccess"] = false;
                        result["Msg"] += log.description;
                    }
                }

                try
                {
                    buaSyncInsideDao.Insert(log);
                    buaSyncInsideDao.UpdateMaxId();
                }
                catch (Exception ex)
                {
                }
            }
        }

        [Frame(false, false)]
        public virtual void BUAUserSyncToInside()
        {
            try
            {
                ServiceFactory factory = Beyondbit.BUA.Client.ServiceFactory.Instance();
                SMC_UserDao userDao = new SMC_UserDao(AppConfig.statisticDBKey);
                IUserService us = factory.GetUserService();
                IOrgService os = factory.GetOrgService();
                Org topOrg = os.GetTopOrg();
                Beyondbit.BUA.Client.User[] users = us.QueryUsersByObjectCode(ObjectType.Org, topOrg.OrgCode);
                string orgCode = topOrg.OrgCode;
                List<Org> orgs = new List<Org>();
                GetOrgs(os, orgCode, orgs);
                if (orgs.Count > 0)
                {
                    foreach (Org o in orgs)
                    {
                        Beyondbit.BUA.Client.User[] user = GetUsers(us, o.OrgCode);
                        if (users != null && users.Length > 0)
                        {
                            foreach (Beyondbit.BUA.Client.User _u in users)
                            {
                                SMC_User u = null;
                                u = userDao.GetUser(_u.UserUid);
                                if (u == null)
                                {
                                    u = new SMC_User();
                                    u.U_NAME = _u.UserFullName;
                                    u.U_PASSWORD = _u.UserPassword;
                                    u.U_UID = _u.UserUid;
                                    int id = userDao.GetMaxId() + 1;
                                    u.U_ID = id;
                                    string unitCode = os.GetUnitCode(ObjectType.User, u.U_UID);
                                    Org unitOrg = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
                                    u.u_unitname = unitOrg.OrgName;
                                    u.U_UNITCODE = unitCode;
                                    u.u_enable_status = UserEnabledStatus.UnAuthed;
                                    u.u_auth_submit_time = DateTime.Now;
                                    userDao.Insert(u);
                                    userDao.UpdateMaxId();
                                }
                                else
                                {
                                    u.U_NAME = _u.UserFullName;
                                    u.U_PASSWORD = _u.UserPassword;
                                    string unitCode = os.GetUnitCode(ObjectType.User, u.U_UID);
                                    Org unitOrg = os.GetOrgBaseInfo(ObjectType.Org, unitCode);
                                    u.u_unitname = unitOrg.OrgName;
                                    u.U_UNITCODE = unitCode;
                                    u.u_enable_status = UserEnabledStatus.UnAuthed;
                                    u.u_auth_submit_time = DateTime.Now;
                                    userDao.Update(u);
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (DalException ex)
            {
                throw new BOException("同步BUA用户出错", ex);
            }
        }

        private Beyondbit.BUA.Client.User[] GetUsers(IUserService us, string orgCode)
        {
            return us.QueryUsersByObjectCode(ObjectType.Org, orgCode);
        }
        private void GetOrgs(IOrgService os, string orgCode, List<Org> orgs)
        {
            Org[] _orgs = os.QuerySubOrgs(orgCode);
            if (_orgs != null && _orgs.Length > 0)
            {
                foreach (Org org in _orgs)
                {
                    orgs.Add(org);
                    GetOrgs(os, org.OrgCode, orgs);
                }
            }
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_BUAUserSyncToInside unit)
        {
            try
            {
                int i = 0;
                if (SMC_BUAUserSyncToInsideDao.Get(unit.busi_id) == null)
                {
                    //i = SMC_UnitDao.Insert(unit);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_BUAUserSyncToInside"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_BUAUserSyncToInside");
                        unit.busi_id = max_id + 1;
                    }
                    else
                    {
                        unit.busi_id = 1;
                    }
                    i = SMC_BUAUserSyncToInsideDao.Insert(unit);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_BUAUserSyncToInside");
                    }
                }
                else
                {
                    i = SMC_BUAUserSyncToInsideDao.Update(unit);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_BUAUserSyncToInside unit)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_BUAUserSyncToInside"))
                {
                    int max_id = autoDao.GetMaxID("SMC_BUAUserSyncToInside");
                    unit.busi_id = max_id + 1;
                }
                else
                {
                    unit.busi_id = 1;
                }
                int i = SMC_BUAUserSyncToInsideDao.Insert(unit);

                autoDao.UpdateMaxID("SMC_BUAUserSyncToInside");

                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_BUAUserSyncToInside unit)
        {
            try
            {
                int i = SMC_BUAUserSyncToInsideDao.Update(unit);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual SMC_BUAUserSyncToInside Get(int Unit_ID)
        {
            return SMC_BUAUserSyncToInsideDao.Get(Unit_ID);
        }



        [Frame(false, false)]
        public virtual void Delete(SMC_BUAUserSyncToInside entity)
        {

            try
            {
                SMC_BUAUserSyncToInsideDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_BUAUserSyncToInside> entities)
        {
            try
            {
                SMC_BUAUserSyncToInsideDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }
        
        [Frame(false, false)]
        public virtual void DeleteList(List<int> busi_ids)
        {
            try
            {
                SMC_BUAUserSyncToInsideDao.DeleteList(busi_ids);
            }
            catch (DalException ex)
            {
                throw new BOException("DeleteList", ex);
            }
        }
    }
}