using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beyondbit.MVC;
using SmartBox.Console.Common;
using Beyondbit.BUA.Client;
using System.Configuration;
using SmartBox.Console.Bo;

namespace SmartBox.Console.Web.Controllers
{


    [HandleError]
    public class HomeController : MyControllerBase
    {
        #region 属性和变量

        private IAuthorizationService _IAuthorizationService;
        public IAuthorizationService AuthorizationService
        {
            get
            {
                if (_IAuthorizationService == null)
                {
                    _IAuthorizationService = ServiceFactory.Instance().GetAuthorizationService();
                }
                return _IAuthorizationService;
            }
        }

        #endregion
        //[Authorize()]
        public ActionResult Index()
        {
            //if (String.IsNullOrEmpty(Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid))
            //{
            //    string SSO_SignOnUrl = ConfigurationManager.AppSettings["SSO_SignOnUrl"];
            //    return Redirect(SSO_SignOnUrl);
            //}
            const string menucachekey = "__UserMenu";

            List<AccordionItem> menuList;
            SessionCache.Clear();
            if (SessionCache.Contains(menucachekey))
            {
                menuList = SessionCache.Get(menucachekey) as List<AccordionItem>;
                if (menuList == null)
                {
                    menuList = new List<AccordionItem>();
                }
            }
            else
            {
                if (Beyondbit.SingleSignOn.ClientForAspNet20.SSOClient.UserUid == null)
                {
                    menuList = SmartBox.Console.UserMenu.UserMenuFactory.GetUserMenuBO.GetCurrentUserMenu(this);
                    //menuList = GetCurrentUserMenu();
                }
                else
                {
                    menuList = GetCurrentUserMenuByBUA();
                }
                SessionCache.AddItem(menucachekey, menuList);
            }
            ViewData["CurrentUser"] = base.CurrentUser.FullName;
            ViewData["UserOrg"] = base.CurrentUser.OrgName;

            return View(menuList);
        }

        private List<AccordionItem> GetCurrentUserMenuByBUA()
        {
            List<AccordionItem> menulist = new List<AccordionItem>();
            string privilegeTree = AuthorizationService.QueryAllPrivilegeTree(base.CurrentUser.UserUId);
            //根据用户得顶级权限
            Beyondbit.BUA.Client.Privilege[] privilegeParent = AuthorizationService.QuerySubPrivileges(base.CurrentUser.UserUId);
            //根据用户和顶级权限，得第2级权限
            if (privilegeParent != null && privilegeParent.Length > 0)
            {
                foreach (Beyondbit.BUA.Client.Privilege pp in privilegeParent)
                {
                    AccordionItem _item = new AccordionItem();
                    _item.Text = pp.PrivilegeName;
                    _item.Code = pp.PrivilegeCode;
                    _item.IsExpand = false;
                    _item.Url = pp.MenuUrl;
                    _item.IcoSrc = "";


                    Beyondbit.BUA.Client.Privilege[] privilege = AuthorizationService.QuerySubPrivileges(base.CurrentUser.UserUId, privilegeParent[0].PrivilegeCode);
                    if (privilege.Length > 0 && privilege != null)
                    {
                        foreach (Beyondbit.BUA.Client.Privilege p in privilege)
                        {
                            //二级
                            AccordionItem item = new AccordionItem();
                            item.Text = p.PrivilegeName;
                            item.Code = p.PrivilegeCode;
                            item.IsExpand = false;
                            item.Url = p.MenuUrl;
                            item.IcoSrc = "";
                            //
                            Beyondbit.BUA.Client.Privilege[] child = AuthorizationService.QuerySubPrivileges(base.CurrentUser.UserUId, p.PrivilegeCode);
                            if (child != null)
                            {
                                foreach (Beyondbit.BUA.Client.Privilege pc in child)
                                {
                                    //三级
                                    AccordionItem citem = new AccordionItem();
                                    citem.Text = pc.PrivilegeName;
                                    citem.Code = pc.PrivilegeCode;
                                    citem.Url = pc.MenuUrl;
                                    citem.IcoSrc = "";
                                    item.Children.Add(citem);
                                }
                            }
                            _item.Children.Add(item);
                        }
                    }
                    menulist.Add(_item);
                }

            }

            return menulist;

        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ChangePassword(FormCollection form)
        {
            var result = new JsonReturnMessages();
            result.Msg = "操作成功";
            result.IsSuccess = true;
            try
            {
                var oldPwd = form["oldPassword"];
                var newPwd = form["newPassword"];
                var rePwd = form["renewPassword"];
                var useruid = Session["CurrentUser"].ToString();
                if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(rePwd))
                {
                    result.IsSuccess = false;
                    result.Msg = "未填写所有表单";
                    return Json(result);
                }
                if (!BoFactory.GetVersionTrackBo.CheckUserName(useruid, oldPwd))
                {
                    result.IsSuccess = false;
                    result.Msg = "旧密码验证失败";
                    return Json(result);
                }
                if (!newPwd.Equals(rePwd))
                {
                    result.IsSuccess = false;
                    result.Msg = "新密码与确认新密码不一致";
                    return Json(result);
                }
                BoFactory.GetVersionTrackBo.ChangeUserPassword(useruid, newPwd);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = "操作失败";
            }
            return Json(result);
        }


        public ActionResult NotImpl(string viewtext)
        {
            ViewData["ViewText"] = viewtext;
            return View();
        }

        public ActionResult about()
        {
            return View();
        }
    }
}
