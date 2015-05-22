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
using System.Xml;


namespace SmartBox.Console.Bo
{

    public class SMC_UserBo : BaseBO<SMC_User>
    {

        /// <summary>
        /// ?????????
        /// </summary>
        public SMC_UserBo()
        {
            base._dao = this.SMC_UserDao;
        }

        

        private SMC_UserDao _SMC_UserDao;
        protected SMC_UserDao SMC_UserDao
        {
            get
            {
                if (_SMC_UserDao == null)
                {
                    _SMC_UserDao = new SMC_UserDao(AppConfig.statisticDBKey);
                }
                return _SMC_UserDao;
            }
        }


        [Frame(false, false)]
        public virtual SMC_User Get(int U_ID)
        {
            return SMC_UserDao.Get(U_ID);
        }
        
        //[Frame(false, false)]
        //public virtual bool ExistsByUID(string uid)
        //{
        //    return SMC_UserDao.ExistsByUID(uid);
        //}



        [Frame(false, false)]
        public virtual int Save(SMC_User entity)
        {
            if (SMC_UserDao.Get(entity.U_ID) == null)
                return SMC_UserDao.Insert(entity);
            else
                return SMC_UserDao.Update(entity);
        }

        [Frame(false, false)]
        public virtual void Delete(SMC_User entity)
        {

            try
            {
                SMC_UserDao.Delete(entity);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????", ex); 
            }
        }

        /*
        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUserEnableAuthorizationSys(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SMC_UserDao dao = new SMC_UserDao(AppConfig.statisticDBKey);
            return dao.GetUserEnableAuthorizationSys(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }*/
        
        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetSelectUser(string uid, string username, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int deviceAuthStatus, string orderby, int pageSize, int pageIndex)
        {
            SMC_UserDao dao = new SMC_UserDao(AppConfig.statisticDBKey);
            return dao.GetSelectUser(uid, username, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, deviceAuthStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationExDictionary GetUserRetryLock(string uid, string username, string u_unitcode, string u_lock_time_start, string u_lock_time_end, int lockStatus, string orderby, int pageSize, int pageIndex)
        {
            SMC_UserDao dao = new SMC_UserDao(AppConfig.statisticDBKey);
            return dao.GetUserRetryLock(uid, username, u_unitcode, u_lock_time_start, u_lock_time_end, lockStatus, orderby, pageSize, pageIndex);
        }

        [Frame(false, false)]
        public virtual void DeleteList(IList<SMC_User> entities)
        {

            try
            {
                SMC_UserDao.DeleteList(entities);
            }
            catch (DalException ex)
            {
                throw new BOException("????????????????", ex);
            }
        }

        

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool InsertOrUpdate(SMC_User userList)
        {
            try
            {
                //IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
                //IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
                //Beyondbit.BUA.Client.User usr = null;
                //try
                //{
                //    usr = us.GetUserInfo(userList.UL_UID);
                //}
                //catch
                //{
                //}
                
                ////Beyondbit.BUA.Client.User[] _users = us.QueryUsersByObjectCode(ObjectType.User, userList.UL_UID);
                //if (usr == null)
                //{
                //    //新增用户到统一授权
                //    Beyondbit.BUA.Client.User u = new Beyondbit.BUA.Client.User();
                //    u.UserUid = userList.UL_UID;
                //    u.UserName = userList.UL_Name;
                //    u.UserFullName = userList.UL_Name;
                //    u.UserEmail = userList.UL_MailAddress == null ? "" : userList.UL_MailAddress;
                //    u.UserPersonal = new UserPersonal();
                //    u.UserPersonal.UserMobilePhone = userList.UL_MobilePhone;
                //    u.UnitCode = userList.Unit_ID;
                //    u.OrgCode = userList.Unit_ID;
                //    u.UserPassword = userList.UL_PWD;
                //    us.AddUser(u);
                //}
                //else
                //{
                //}
                int i = 0;
                if (SMC_UserDao.Get(userList.U_ID) == null)
                {
                    //i = SMC_UserListDao.Insert(userList);
                    SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                    if (autoDao.HasMaxID("SMC_User"))
                    {
                        int max_id = autoDao.GetMaxID("SMC_User");
                        userList.U_ID = max_id + 1;
                    }
                    else
                    {
                        userList.U_ID = 1;
                    }
                    i = SMC_UserDao.Insert(userList);
                    if (true)
                    {
                        autoDao.UpdateMaxID("SMC_User");
                    }
                }
                else
                {
                    i = SMC_UserDao.Update(userList);
                }

                return true;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法InsertOrUpdate失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Insert(SMC_User userlist)
        {
            try
            {
                SMC_AutoTableIDDao autoDao = new SMC_AutoTableIDDao(AppConfig.statisticDBKey);
                if (autoDao.HasMaxID("SMC_User"))
                {
                    int max_id = autoDao.GetMaxID("SMC_User");
                    userlist.U_ID = max_id + 1;
                }
                else
                {
                    userlist.U_ID = 1;
                }
                int i = SMC_UserDao.Insert(userlist);
                if (i > 0)
                {
                    autoDao.UpdateMaxID("SMC_User");
                }
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Insert失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual bool Update(SMC_User userlist)
        {
            try
            {
                int i = SMC_UserDao.Update(userlist);
                return i > 0;
            }
            catch (DalException ex)
            {
                throw new BOException("调用方法Update失败", ex);
            }
        }

        [Frame(false, false)]
        public virtual SelectPagnationEx<SMC_User> GetUserAuthorizationList(string uid, string uname, string u_unitcode, string u_auth_submit_time_start, string u_auth_submit_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            return SMC_UserDao.GetUserAuthorizationList(uid, uname, u_unitcode, u_auth_submit_time_start, u_auth_submit_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationEx<SMC_User> GetUserComparerationList(string uid, string uname, string u_unitcode, string u_compare_time_start, string u_compare_time_end, string orderby, int pageSize, int pageIndex)
        {
            return SMC_UserDao.GetUserComparerationList(uid, uname, u_unitcode, u_compare_time_start, u_compare_time_end, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationEx<SMC_User> GetUserManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            return SMC_UserDao.GetUserManageList(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationEx<_OnLineUser> GetUserInfoManageList(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            return SMC_UserDao.GetUserInfoManage(uid, uname, u_unitcode, orderby, pageSize, pageIndex);
            //return SMC_UserDao.GetUserInfoManageList(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationEx<UserOnline> GetUserDeviceOnline(string uid, string uname, string u_unitcode, string u_disable_time_start, string u_disable_time_end, string u_enable_time_start, string u_enable_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            UserOnlineDao uoDao = new UserOnlineDao(AppConfig.mainDbKey);
            return uoDao.GetUserDeviceOnline(uid, uname, u_unitcode, orderby, pageSize, pageIndex);
            //return SMC_UserDao.GetUserInfoManageList(uid, uname, u_unitcode, u_disable_time_start, u_disable_time_end, u_enable_time_start, u_enable_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);
        }
        
        [Frame(false, false)]
        public virtual SelectPagnationEx<SMC_User> GetUserLoginUnLockList(string uid, string uname, string u_unitcode, string u_lock_time_start, string u_lock_time_end, string u_lock_expire_time_start, string u_lock_expire_time_end, int lockStatus, int enabledStatus, int authStatus, string orderby, int pageSize, int pageIndex)
        {
            return SMC_UserDao.GetUserLoginUnLockList(uid, uname, u_unitcode, u_lock_time_start, u_lock_time_end, u_lock_expire_time_start, u_lock_expire_time_end, lockStatus, enabledStatus, authStatus, orderby, pageSize, pageIndex);
        }
        
        

        /// <summary>
        /// 通过审核用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool PassUser(string uid)
        {
            SMC_User u = SMC_UserDao.GetUser(uid);
            if (u != null)
            {
                //u.u_auth_status = UserAuthStatus.Passed;
                u.u_auth_time = DateTime.Now;
                u.u_enable_status = UserEnabledStatus.Enabled;
                u.u_enable_time = DateTime.Now;
                SMC_UserDao.Update(u);
                return true;
            }
            else
                return false;
        }
        
        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool UnLockUser(string uid)
        {
            SMC_User u = SMC_UserDao.GetUser(uid);
            if (u != null)
            {
                //u.u_auth_status = UserAuthStatus.Passed;
                u.u_lock_status = UserLockedStatus.UnLocked;
                u.u_lock_expire_time = DateTime.Now;
                u.u_unlock_time = DateTime.Now;
                SMC_UserDao.Update(u);
                return true;
            }
            else
                return false;
        }
        
        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool LockUser(string uid)
        {
            SMC_User u = SMC_UserDao.GetUser(uid);
            if (u != null)
            {
                //u.u_auth_status = UserAuthStatus.Passed;
                u.u_lock_status = UserLockedStatus.Locked;
                u.u_lock_expire_time = DateTime.Now.AddHours(1);//1小时后自动解锁
                u.u_lock_time = DateTime.Now;
                SMC_UserDao.Update(u);

                try
                {
                    SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();
                    client.ForceQuitUsers(new string[] { uid });
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }
                return true;
            }
            else
                return false;
        }
        
        /// <summary>
        ///  拒绝通过审核用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Frame(false, false)]
        public virtual bool NotPassUser(string uid)
        {
            SMC_User u = SMC_UserDao.GetUser(uid);
            if (u != null)
            {
                //u.u_auth_status = UserAuthStatus.Rejected;
                u.u_auth_time = DateTime.Now;
                u.u_enable_status = UserEnabledStatus.Disabled;
                u.u_disable_time = DateTime.Now;
                u.u_lock_expire_time = DateTime.MaxValue;//给一个比较大的时间值
                SMC_UserDao.Update(u);

                try
                {
                    SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();
                    client.ForceQuitUsers(new string[] { uid });
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }
                return true;
            }
            else
                return false;
        }

        public virtual void KickoutUser(string uid, Hashtable result, SmartBox.Console.Service.ServiceReference1.ManagerServiceClient client)
        {
            if (client == null)
                client = new SmartBox.Console.Service.ServiceReference1.ManagerServiceClient();
            try
            {
                string[] sessionids = new string[] { uid };
                client.ForceQuitUsers(sessionids);
                result["d"] = "强制用户退出成功";
                result["r"] = true;
            }
            catch (Exception e)
            {
                result["r"] = false;
                result["d"] += "强制用户退出失败，详细信息：" + e.Message;
            }
            
        }

        public Beyondbit.BUA.Client.User[] GetBUAUsersByOrgCode(string orgCode, int pageSize, int pageIndex)
        {
            Beyondbit.BUA.Client.IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
            Beyondbit.BUA.Client.IUserService us = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
            string orgTree = os.QueryOrgTree(orgCode);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(orgTree);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//root/node");
            Beyondbit.BUA.Client.User[] users = us.QueryUsersByObjectCode(ObjectType.Org, orgCode, "", orgCode, true, UserLockedType.UnLocked, pageSize + 10000, pageIndex);
            List<Beyondbit.BUA.Client.User> ulist = users.ToList<Beyondbit.BUA.Client.User>();
            if (nodes != null && nodes.Count > 0)
            {
                foreach (XmlNode node in nodes) {
                    string _orgCode = node.Attributes["id"].Value;
                    Beyondbit.BUA.Client.User[] ul = us.QueryUsersByObjectCode(ObjectType.Org, _orgCode, "", _orgCode, true, UserLockedType.UnLocked, 5000, 1);
                    ulist.AddRange(ul);
                }
            }
            return ulist.ToArray();
        }
    }
}
