//----------------------------------------------------------------
// Copyright (C) 2014 Beyondbit
// 
// All rights reserved.
//
// SMC_UserListBo.cs
// 
// 
// 
// 2014-03-05 04:11:59
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
using Beyondbit.BUA.Client;


namespace SmartBox.Console.Bo
{

    public class SMC_UserListBo
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_UserListBo()
        {
        }
        private static VersionTrackBo varsionTrackBo = new VersionTrackBo();
        private SMC_UserListDao _SMC_UserListDao;
        protected SMC_UserListDao SMC_UserListDao
        {
            get
            {
                if (_SMC_UserListDao == null)
                {
                    _SMC_UserListDao = new SMC_UserListDao(AppConfig.statisticDBKey);
                }
                return _SMC_UserListDao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_UserList Get(int UL_ID)
        {
            return SMC_UserListDao.Get(UL_ID);
        }
        
        [Frame(false, false)]
        public virtual bool ExistsByUID(string uid)
        {
            return SMC_UserListDao.ExistsByUID(uid);
        }

        [Frame(false, false)]
        public virtual Dictionary<string, string> GetUnitByUL_UID(string UL_UID)
        {
            Dictionary<string, string> unitInfo = new Dictionary<string, string>();
             string unitId = SMC_UserListDao.GetUnitIdByUID(UL_UID);
             SMC_UnitDao unitDao = new SMC_UnitDao(AppConfig.statisticDBKey);

             SMC_Unit unit = unitDao.Get(unitId);
             if (unit != null)
             {
                 unitInfo.Add(unit.Unit_Name, unit.Unit_ID);
             }
             return unitInfo;
        }

        [Frame(false, false)]
        public virtual int Save(SMC_UserList entity)
        {
            if (SMC_UserListDao.Get(entity.UL_ID) == null)
                return SMC_UserListDao.Insert(entity);
            else
                return SMC_UserListDao.Update(entity);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_UserList entity)
        {

            try
            {
                SMC_UserListDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex); 
            }
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_UserList> entities)
        {

            try
            {
                SMC_UserListDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        [Frame(false, false)]
        public virtual void DeleteByRoleID(int role_id)
        {

            try
            {
                SMC_UserListDao.DeleteByRoleID(role_id);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex);
            }
        }

        /// <summary>
        /// 判断管理员是否有某权限
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="FN_ID"></param>
        /// <returns></returns>
        public virtual bool HasFunction(string UID, int FN_ID)
        {
            try
            {
                if (varsionTrackBo.IsSystemManager(UID))
                    return true;

                return SMC_UserListDao.HasFunction(UID, FN_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        public virtual bool HasFunction(string UID, string functionCode)
        {
            try
            {
                if (varsionTrackBo.IsSystemManager(UID))
                    return true;
                if (functionCode == "addManager")
                {
                    //是否有增加管理员权限
                    //BoFactory.GetVersionTrackBo.CheckUserName("","");
                    return false;
                }
                else
                {
                    return SMC_UserListDao.HasFunction(UID, functionCode);
                }
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }


        /// <summary>
        /// 按Unit_ID查询单位的用户列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="Unit_ID"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUserListByUnitID(PageView view, string Unit_ID)
        {            
            try
            {
                return SMC_UserListDao.QueryUserListByUnitID(view, Unit_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法QueryUserListByUnitID失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool IsUserListInRole(string UL_UID, int RoleId)
        {
            try
            {
                //系统管理员有最高权限

                if (varsionTrackBo.IsSystemManager(UL_UID))
                    return true;

                return SMC_UserListDao.IsUserListInRole(UL_UID, RoleId);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法QueryUserListByUnitID失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual Hashtable QueryPersonsTreeDataByUnit(string Unit_ID, int Role_ID)
        {
            try
            {
                Hashtable result = new Hashtable();
                SMC_UserListDao ulDao = new SMC_UserListDao(AppConfig.statisticDBKey);
                SMC_UnitDao unitDao = new SMC_UnitDao(AppConfig.statisticDBKey);

                IList<SMC_UserList> ulList = ulDao.QueryUserListByUnitID(Unit_ID);
                SMC_Unit tunit = null;
                if (!String.IsNullOrEmpty(Unit_ID))
                    tunit = unitDao.Get(Unit_ID);

                List<Hashtable> hashs = new List<Hashtable>();
                if (ulList.Count > 0)
                {
                    List<Hashtable> objs = new List<Hashtable>();
                    foreach (SMC_UserList u in ulList)
                    {
                        Hashtable uhash = new Hashtable();
                        uhash["id"] = "u_" + u.UL_ID;
                        uhash["name"] = u.UL_Name;
                        uhash["tp"] = "user";
                        uhash["checked"] = ulDao.IsUserListInRole(u.UL_ID, Role_ID);
                        hashs.Add(uhash);
                    }
                    //hash["children"] = objs;
                }
                
                IList<SMC_Unit> units = unitDao.QueryChildUnitsByUnitID(Unit_ID, 0);
                
                
                result["children"] = hashs;
                if (units != null && units.Count > 0)
                    foreach (SMC_Unit unit in units)
                    {
                        Hashtable hash = new Hashtable();
                        //bool isFnInRole = frDao.IsUserInRole(fn.FN_ID, Role_ID);
                        hash["id"] = unit.Unit_ID;
                        hash["name"] = unit.Unit_Name;
                        hash["open"] = true;
                        //hash["checked"] = isFnInRole;
                        List<Hashtable> objs = new List<Hashtable>();
                        parseChildTreeData(hash, unit, unitDao, ulDao, objs, Role_ID);
                        hashs.Add(hash);
                    }

                //if (tunit != null)
                {
                    result["id"] = String.IsNullOrEmpty(Unit_ID) ? Unit_ID : tunit.Unit_ID;
                    result["name"] = String.IsNullOrEmpty(Unit_ID) ? "全局" : tunit.Unit_Name;
                    result["open"] = true;

                    
                }

                return result;
            }
            catch (DalException ex)
            {
                throw new BOException("QueryFunctionsByUnitID", ex);
            }
        }

        private void parseChildTreeData(Hashtable pHash, SMC_Unit parent_unit, SMC_UnitDao unitDao, SMC_UserListDao ulDao, List<Hashtable> objs, int RoleId)
        {
            IList<SMC_UserList> users = ulDao.QueryUserListByUnitID(parent_unit.Unit_ID);
            foreach (SMC_UserList u in users)
            {
                Hashtable uhash = new Hashtable();
                uhash["id"] = "u_" + u.UL_ID;
                uhash["name"] = u.UL_Name;
                uhash["tp"] = "user";
                uhash["checked"] = ulDao.IsUserListInRole(u.UL_ID, RoleId);
                objs.Add(uhash);
                pHash["open"] = true;
            }

            IList<SMC_Unit> units = unitDao.QueryChildUnitsByUnitID(parent_unit.Unit_ID, -1);
            if (units != null && units.Count > 0)
            {
                //QueryUserListByUnitID
                
                foreach (SMC_Unit unit in units)
                {
                    Hashtable hash = new Hashtable();
                    hash["id"] = unit.Unit_ID;
                    hash["name"] = unit.Unit_Name;
                    hash["tp"] = "unit";
                    objs.Add(hash);

                    

                    
                    List<Hashtable> _objs = new List<Hashtable>();
                    parseChildTreeData(hash, unit, unitDao, ulDao, _objs, RoleId);
                    
                }
                
            }

            if (objs.Count > 0)
            {
                pHash["children"] = objs;
            }
        }

        /// <summary>
        /// 获取角色里的用户
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUserListHasRole(PageView view, int role_id)
        {

            try
            {
                return SMC_UserListDao.QueryUserListHasRole(view, role_id);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法QueryUserListHasRole失败", ex);
            }
        }

        /// <summary>
        /// 获取某单位属于某角色的用户
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual JsonFlexiGridData QueryUserListHasRole(PageView view, int role_id, string Unit_ID)
        {

            try
            {
                return SMC_UserListDao.QueryUserListHasRole(view, role_id, Unit_ID);
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法QueryUserListHasRole失败", ex);
            }
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_UserList userList)
        {
            try
            {
                IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
                IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
                Beyondbit.BUA.Client.User usr = null;
                try
                {
                    usr = us.GetUserInfo(userList.UL_UID);
                }
                catch
                {
                }

                //Beyondbit.BUA.Client.User[] _users = us.QueryUsersByObjectCode(ObjectType.User, userList.UL_UID);
                if (usr == null)
                {
                    try
                    {
                        //新增用户到统一授权
                        Beyondbit.BUA.Client.User u = new Beyondbit.BUA.Client.User();
                        u.UserUid = userList.UL_UID;
                        u.UserName = userList.UL_Name;
                        u.UserFullName = userList.UL_Name;
                        u.UserEmail = userList.UL_MailAddress == null ? "" : userList.UL_MailAddress;
                        u.UserPersonal = new UserPersonal();
                        u.UserPersonal.UserMobilePhone = userList.UL_MobilePhone;
                        u.UnitCode = userList.Unit_ID;
                        u.OrgCode = userList.Unit_ID;
                        u.UserPassword = userList.UL_PWD;
                        us.AddUser(u);
                    }
                    catch (Exception exe)
                    {
                    }
                }
                else
                {
                }


                int i = 0;
                if (SMC_UserListDao.Get(userList.UL_ID) == null)
                {
                    //i = SMC_UserListDao.Insert(userList);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_UserList"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_UserList");
                        userList.UL_ID = max_id + 1;
                    }
                    else
                    {
                        userList.UL_ID = 1;
                    }
                    i = SMC_UserListDao.Insert(userList);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_UserList");
                    }
                }
                else
                {
                    i = SMC_UserListDao.Update(userList);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_UserList userlist)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_UserList"))
                {
                    int max_id = autoDao.GetMaxID("SMC_UserList");
                    userlist.UL_ID = max_id + 1;
                }
                else
                {
                    userlist.UL_ID = 1;
                }
                int i = SMC_UserListDao.Insert(userlist);
                if (i > 0)
                {
                    autoDao.UpdateMaxID("SMC_UserList");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_UserList userlist)
        {
            try
            {
                int i = SMC_UserListDao.Update(userlist);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        public virtual bool CheckUserName(string userName, string password)
        {
            try
            {
                bool r = SMC_UserListDao.CheckUserName(userName, password);
                return r;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        public virtual string GetUnitIdByUID(string ul_uid)
        {
            try
            {
                string r = SMC_UserListDao.GetUnitIdByUID(ul_uid);
                return r;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }
    }
}
