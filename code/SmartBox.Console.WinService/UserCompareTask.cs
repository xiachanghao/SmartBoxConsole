using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using Beyondbit.BUA.Client;

namespace SmartBox.Console.WinService
{
    public class UserCompareTask : WinService.WinService<SMC_User>
    {
        List<SMC_User> users = null;
        public override List<SMC_User> GetDataSource()
        {
            IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            Tuple<string, string, object> par = new Tuple<string, string, object>("u_enable_status", "=", 1);//已启用
            pars.Add(par);

            Tuple<string, string, object> par2 = new Tuple<string, string, object>("u_lock_status", "=", 0);//未锁定
            pars.Add(par2);

            Tuple<string, string, object> par3 = new Tuple<string, string, object>("u_need_sync", "=", 0);//不需要同步的，也就是还没有比较的
            pars.Add(par3);

            users = Bo.BoFactory.GetSMC_UserBo.QueryList(pars);
            return users;
        }

        public override void Execute()
        {
            if (users != null && users.Count > 0)
            {
                SmartBox.Console.Common.Log4NetHelper.Info("统一授权用户与本地用户比对开始！");
                foreach (SMC_User d in users)
                {
                    ServiceFactory factory = Beyondbit.BUA.Client.ServiceFactory.Instance();
                    IUserService us = factory.GetUserService();

                    Beyondbit.BUA.Client.User _u = null;
                    try
                    {
                        _u = us.GetUserInfo(d.U_UID);
                    }
                    catch (Exception ex)
                    {
                        SmartBox.Console.Common.Log4NetHelper.Error(ex);
                        continue;
                    }

                    if (_u != null)
                    {
                        if (d.U_UNITCODE != _u.UnitCode || d.U_NAME != _u.UserFullName)
                        {
                            d.u_need_sync = true;
                            d.u_need_sync_compare_time = DateTime.Now;
                            Bo.BoFactory.GetSMC_UserBo.Update(d);
                        }
                    }
                }
                SmartBox.Console.Common.Log4NetHelper.Info("统一授权用户与本地用户比对结束！");
            }
            else
            {
                SmartBox.Console.Common.Log4NetHelper.Info("未查询到需要比对的用户，将不执行比对任务！");
            }
        }
    }
}
