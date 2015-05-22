using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.WinService
{
    public abstract class WinService<T> where T : class, new()
    {
        public abstract List<T> GetDataSource();
        public abstract void Execute();
    }

    public class DeviceLockAutoExpireTask : WinService<Device>
    {
        bool device_unlock_auto_enabled = true;
        private List<Device> devices = null;

        public DeviceLockAutoExpireTask()
        {
            //GlobalParam parm = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("device_unlock_auto_enabled");
                
            //if (parm != null)
            //{
            //    device_unlock_auto_enabled = parm.ConfigValue == "1";
            //}                
        }
        
        public override List<Device> GetDataSource()
        {
            if (device_unlock_auto_enabled)
            {
                IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
                Tuple<string, string, object> par = new Tuple<string, string, object>("status", "=", 1);
                pars.Add(par);

                devices = Bo.BoFactory.GetDeviceBO.QueryList(pars);
            }
            else
            {
                devices = new List<Device>();
            }
            return devices;
        }

        public override void Execute()
        {
            if (devices != null && devices.Count > 0)
            {
                SmartBox.Console.Common.Log4NetHelper.Info("设备自动解锁任务开始！");
                if (device_unlock_auto_enabled)
                {
                    foreach (Device d in devices)
                    {
                        if (d.LockExpireHours > 0)
                        {
                            if (d.LockTime.AddHours(d.LockExpireHours) < DateTime.Now)
                            {
                                //如果设备锁定时间已过，则解除锁定
                                d.Status = 0;
                                d.UnLockTime = DateTime.Now;
                                Bo.BoFactory.GetDeviceBO.Update(d);
                            }
                        }
                    }
                }
                SmartBox.Console.Common.Log4NetHelper.Info("设备自动解锁任务结束！");
            }
            else
            {
                SmartBox.Console.Common.Log4NetHelper.Info("未查询到需要自动解锁的设备！");
            }
        }
    }
}
