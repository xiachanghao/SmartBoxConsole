using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.Diagnostics;
using Beyondbit.Framework.Core.Proxy;

namespace SmartBox.Console.WinService
{
    public class Program
    {
        static void CloseProcess(string ArrayProcessName, bool CloseSelfProcess)
        {
            string[] processName = ArrayProcessName.Split(',');
            foreach (string appName in processName)
            {
                Process[] localByNameApp = Process.GetProcessesByName(appName);//获取程序名的所有进程
                if (localByNameApp.Length > 0)
                {
                    foreach (var app in localByNameApp)
                    {
                        if (!CloseSelfProcess)
                        {
                            //关闭当前进程
                            if (Process.GetCurrentProcess().Id == app.Id)
                                continue;
                        }
                        if (!app.HasExited)
                        {
                            app.Kill();//关闭进程
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            CloseProcess("SmartBox.Console.WinService", false);//不关闭当前进程
            

            //锁定用户自动解锁
            UserLockAutoExpireTask ulet = new UserLockAutoExpireTask();
            ulet.GetDataSource();
            ulet.Execute();

            //锁定设备自动解锁
            DeviceLockAutoExpireTask t = new DeviceLockAutoExpireTask();
            t.GetDataSource();
            t.Execute();

            
            
            SyncBUAUnitTask sbut = new SyncBUAUnitTask();
            try
            {
                sbut.GetDataSource();

                sbut.Execute();
            }
            catch
            {
            }

            //===================================================================================================
            //GlobalParam packUploadFolder = Bo.BoFactory.GetGlobalParamBO.GetGlobalParam("packUploadFolder");
            //PackageExtInternalReleaseTask irt = new PackageExtInternalReleaseTask();
            //irt.GetDataSource();
            //irt.Execute();
            //===================================================================================================

            //同步应用中心问题反馈至内网
            SyncOutsideFAQToInsideTask soft = new SyncOutsideFAQToInsideTask();
            soft.GetDataSource();
            soft.Execute();            

            //比较本地用户和统一授权用户
            UserCompareTask ut = new UserCompareTask();
            ut.GetDataSource();
            ut.Execute();

            //应用授权同步代码
            AppAuthorizationSyncTask authTask = new AppAuthorizationSyncTask();
            authTask.Execute();

            //同步bua用户至本地数据库（包括内、外网）
            SyncBUAUserToInsideTask sbtit = new SyncBUAUserToInsideTask();
            sbtit.Execute();

            //============================================================================================================================
            //原来SyncInsideDataToOutside项目的代码
            //IProxy proxy = ProxyFactory.CreateProxy();
            //SmartBox.Console.Bo.AppCenter.AppCenterBO bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();

            //SmartBox.Console.Service.ApplicationCenterWS.WebService ws = new SmartBox.Console.Service.ApplicationCenterWS.WebService();
            //SyncProgram.SyncPackages(bo, ws);
            //SyncProgram.SyncUnits(bo, ws);
            //SyncProgram.SyncUsers(bo, ws);
            //============================================================================================================================

            System.Console.WriteLine("所有任务执行完毕!");
            CloseProcess("SmartBox.Console.WinService", true);//关闭当前进程
        }
    }
}
