using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using Beyondbit.BUA.Client;
using SmartBox.Console.Bo;
using SmartBox.Console.Common;
using SmartBox.Console.Dao;

namespace SmartBox.Console.WinService
{
    public class AppAuthorizationSyncTask : WinService.WinService<PrivilegeUser>
    {
        public override List<PrivilegeUser> GetDataSource()
        {
            return null;
        }

        public override void Execute()
        {
            //AppPrivilegeDao appPrivilegeDao = new AppPrivilegeDao(AppConfig.mainDbKey);
            List<Tuple<string, string, object>> pars = new List<Tuple<string,string,object>>();
            pars.Add(new Tuple<string, string, object>("EnableSync", "=", "True"));

            List<AppPrivilege> appPrivileges = Bo.BoFactory.GetAppPrivilegeBO.QueryList(pars);
            string privilegeIds = "";
            if (appPrivileges != null && appPrivileges.Count > 0)
            {
                for (int i = 0; i < appPrivileges.Count; ++i) {
                    if (!String.IsNullOrEmpty(appPrivileges[i].BuaPrivilegeCode))
                    {
                        privilegeIds += appPrivileges[i].ID;
                        if (i != appPrivileges.Count - 1)
                            privilegeIds += ",";
                    }
                }
                
            }
            
            string[] privelegs = privilegeIds.Split(",".ToCharArray());


            try
            {
                if (privelegs != null && privelegs.Length > 0)
                {
                    bool err = false;
                    string errMsg = "";
                    string okMsg = "";
                    foreach (string pid in privelegs)
                    {
                        JsonReturnMessages _data = new JsonReturnMessages() { IsSuccess = true, Msg = "同步成功" };
                        bool issucced = _AsyncPrivilege(pid);
                        if (issucced)
                        {
                            if (String.IsNullOrEmpty(_data.Msg))
                                continue;
                            err = true;
                            //errMsg += "<br/>权限代码" + pid + "的授权访问用户同步失败，原因：" + _data.Msg + "！";
                        }
                        else
                        {
                            //okMsg += "<br/>权限代码" + pid + "的授权访问用户同步成功！";
                        }
                    }

                    //data.Msg = okMsg + "<br/>" + errMsg;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
        }

        private bool _AsyncPrivilege(string privilegeId)
        {
            if (String.IsNullOrEmpty(privilegeId))
            {
                return false;
            }

            BUAClientConfig config = RuntimeConfig.Instance;
            var backAppCode = config.ApplicationCode;
            try
            {
                if (privilegeId == "all")
                {
                    IList<AppPrivilege> List = BoFactory.GetVersionTrackBo.QueryAppPrivilegeList();
                    foreach (AppPrivilege ap in List)
                    {
                        //config.ApplicationCode = ap.BuaAppCode;
                        var users = ServiceFactory.Instance().GetAuthorizationService().QueryUsersByPrivilege(ap.BuaPrivilegeCode);
                        List<string> uids = new List<string>();
                        foreach (var user in users)
                        {
                            uids.Add(user.UserUid);
                        }
                        BoFactory.GetVersionTrackBo.AsyncPrivilege(ap.ID.ToString(), uids, "");
                    }
                }
                else
                {
                    var privilege = BoFactory.GetVersionTrackBo.GetAppPrivilege(privilegeId);
                    string privilegeCode = privilege.BuaPrivilegeCode;
                    //config.ApplicationCode = privilege.BuaAppCode;
                    var users = ServiceFactory.Instance().GetAuthorizationService().QueryUsersByPrivilege(privilegeCode);

                    List<string> uids = new List<string>();
                    if (users != null && users.Length > 0)
                        foreach (var user in users)
                        {
                            uids.Add(user.UserUid);
                        }
                    BoFactory.GetVersionTrackBo.AsyncPrivilege(privilegeId, uids, "");
                }
                return true;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
                return false;
            }
            finally
            {
                config.ApplicationCode = backAppCode;
            }
        }
    }
}
