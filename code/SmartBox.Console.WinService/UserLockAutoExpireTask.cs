using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.WinService
{
    public class UserLockAutoExpireTask : WinService<SMC_User>
    {
        bool user_unlock_auto_enabled = true;
        private List<SMC_User> devices = null;

        public UserLockAutoExpireTask()
        {
            //GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("device_unlock_auto_enabled");

            //if (parm != null)
            //{
            //    device_unlock_auto_enabled = parm.ConfigValue == "1";
            //}                
        }

        public override List<SMC_User> GetDataSource()
        {
            if (user_unlock_auto_enabled)
            {
                IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                Tuple<string, string, object> par = new Tuple<string, string, object>("u_lock_status", "=", 1);
                pars.Add(par);

                devices = Bo.BoFactory.GetSMC_UserBo.QueryList(pars);
            }
            else
            {
                devices = new List<SMC_User>();
            }
            return devices;
        }

        public override void Execute()
        {
            if (devices != null && devices.Count > 0)
            {
                SmartBox.Console.Common.Log4NetHelper.Info("用户自动解锁任务开始！");
                if (user_unlock_auto_enabled)
                {
                    foreach (SMC_User d in devices)
                    {
                        if (d.u_lock_status == Common.UserLockedStatus.Locked && d.u_lock_expire_time <= DateTime.Now)
                        {

                                //如果设备锁定时间已过，则解除锁定
                                d.u_lock_status = Common.UserLockedStatus.UnLocked;
                                d.u_lock_expire_time = DateTime.Now;
                                d.u_unlock_time = DateTime.Now;
                                Bo.BoFactory.GetSMC_UserBo.Update(d);
                                Bo.BoFactory.GetUserInfoBO.UnLockUser(d.U_UID);

                        }
                    }
                }
                SmartBox.Console.Common.Log4NetHelper.Info("用户自动解锁任务结束！");
            }
            else
            {
                SmartBox.Console.Common.Log4NetHelper.Info("未查询到需要自动解锁的用户！");
            }
        }
    }
}
