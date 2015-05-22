using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Biz;
using Beyondbit.MVC;
using SmartBox.Console.Common;

namespace SmartBox.Console.Bo
{
    public class UserService : IUserService
    {

        public virtual IUser GetUserInfo(string uid)
        {
            IUser currentUser = new User();
            string _userUid = string.Empty;
            try
            {
                _userUid = Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid;                
            }
            catch (Exception ex)
            {
                throw new BOException("根据用户标识查询用户出错!", ex);
            }
            currentUser.UserUId = _userUid;
            currentUser.FullName = _userUid;
            currentUser.OrgCode = "";
            currentUser.OrgName = "";    //
            return currentUser;
        }

        /// <summary>
        /// 获取单点登录的用户标识，此处无单点登录返回空即可
        /// </summary>
        /// <returns></returns>
        public virtual string GetUserUid()
        {         
            return Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid; 
        }

        public virtual bool HasRight(string uid, string rightCode, string attrCode)
        {
            return HasRight(uid, rightCode);
        }

        public virtual bool HasRight(string uid, string rightCode)
        {
            return false;
        }

        public virtual bool IsInRole(string uid, string roleCode)
        {
            return false;
        }

    }
}
