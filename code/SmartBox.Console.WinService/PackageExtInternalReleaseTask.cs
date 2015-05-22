using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.WinService
{
    public class PackageExtInternalReleaseTask : WinService.WinService<SMC_PackageExt>
    {
        List<SMC_PackageExt> packages = null;
        public override List<SMC_PackageExt> GetDataSource()
        {
            IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            //Tuple<string, string, object> par = new Tuple<string, string, object>("pe_authstatus", "=", 0);//已通过审核
            //pars.Add(par);

            Tuple<string, string, object> par2 = new Tuple<string, string, object>("pe_id", "=", 75);//未锁定
            pars.Add(par2);

            //Tuple<string, string, object> par3 = new Tuple<string, string, object>("u_need_sync", "=", 0);//不需要同步的，也就是还没有比较的
            //pars.Add(par3);

            packages = Bo.BoFactory.GetSMC_PackageExtBO.QueryList(pars);
            return packages;
        }

        public override void Execute()
        {
            if (packages != null && packages.Count > 0)
            {
                SmartBox.Console.Common.Log4NetHelper.Info("app内部同步开始！");
                foreach (SMC_PackageExt ext in packages)
                {
                    try
                    {
                        Bo.BoFactory.GetCommonBO.SMC_PackageExtInternalRelease(ext);
                    }
                    catch (Exception ex)
                    {
                        SmartBox.Console.Common.Log4NetHelper.Error(ex);
                    }
                }
                SmartBox.Console.Common.Log4NetHelper.Info("app内部同步结束！");
            }
            else
            {
                SmartBox.Console.Common.Log4NetHelper.Info("未查询到需要内部app，将不执行内部同步任务！");
            }
        }
    }
}
