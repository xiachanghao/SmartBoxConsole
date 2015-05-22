using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using Beyondbit.BUA.Client;

namespace SmartBox.Console.WinService
{
    public class SyncBUAUnitTask //: WinService.WinService<Beyondbit.BUA.Client.Org>
    {
        //List<SMC_PackageFAQ> packages = null;
        Beyondbit.BUA.Client.Org[] arr = null;
        IOrgService orgService = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();

        public SyncBUAUnitTask()
        {

        }

        public List<Beyondbit.BUA.Client.Org> GetDataSource()
        {
            //IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            //Tuple<string, string, object> par = new Tuple<string, string, object>("pe_authstatus", "=", 0);//已通过审核
            //pars.Add(par);

            //Tuple<string, string, object> par2 = new Tuple<string, string, object>("pe_id", "=", 75);//未锁定
            //pars.Add(par2);

            //Tuple<string, string, object> par3 = new Tuple<string, string, object>("u_need_sync", "=", 0);//不需要同步的，也就是还没有比较的
            //pars.Add(par3);

            //packages = Bo.BoFactory.GetSMC_PackageFAQBO.QueryList(pars);
            arr = orgService.QueryUnits("");

            if (arr != null && arr.Length > 0) {
                //packages = new List<SMC_PackageFAQ>();
                
            }

            
            return null;
        }

        Org topOrg = null;
        public void Execute()
        {
            SmartBox.Console.Common.Log4NetHelper.Info("同步统一授权组织机构开始！");
            if (arr != null && arr.Length > 0)
            {
                topOrg = orgService.GetTopOrg();
                SyncOrg(topOrg);                
            }
            System.Console.WriteLine("bua单位同步完成!");
            SmartBox.Console.Common.Log4NetHelper.Info("同步统一授权组织机构结束！");
        }

        private void SyncOrg(Org parentOrg)
        {
            arr = orgService.QuerySubOrgs(parentOrg.OrgCode); 
            
            Org[] units = orgService.QueryUnits("Unit_Org");

            foreach (Beyondbit.BUA.Client.Org org in arr)
            {
                if (parentOrg.OrgCode != topOrg.OrgCode && org.OrgType != "Unit_Org")
                    continue;
                if (topOrg.OrgCode == parentOrg.OrgCode)
                    insertOrUpdateOrg(org, null);
                else
                {
                    IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                    pars.Add(new KeyValuePair<string, object>("unit_id", parentOrg.OrgCode));
                    SMC_Unit parent_unit = Bo.BoFactory.GetSMC_UnitBo.Get(pars);

                    insertOrUpdateOrg(org, parent_unit);
                }
                SmartBox.Console.Common.Log4NetHelper.Info("单位："+org.OrgName + " " + org.OrgCode + "已同步");
                SyncOrg(org);
            }
        }

        private void insertOrUpdateOrg(Org org, SMC_Unit parentUnit)
        {
            try
            {
                if (org.OrgType.ToLower() != "unit_org" && !String.IsNullOrEmpty(org.ParentOrgCode))
                    return;
                IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                pars.Add(new KeyValuePair<string, object>("unit_id", org.OrgCode));

                SMC_Unit unit = Bo.BoFactory.GetSMC_UnitBo.Get(pars);
                //更新到内网
                if (unit != null)
                {
                    unit.Unit_Name = org.OrgName;
                    unit.Unit_Sequence = org.OrgSequence;
                    unit.Unit_UpdateTime = DateTime.Now;
                    unit.Unit_UpdateUser = "";
                    if (parentUnit == null)
                    {
                        unit.Unit_Path = org.OrgCode;
                        unit.Upper_Unit_ID = "";
                    }
                    else
                    {
                        unit.Unit_Path = parentUnit.Unit_Path + ".." + org.OrgCode;
                        unit.Upper_Unit_ID = parentUnit.Unit_ID;
                    }
                    Bo.BoFactory.GetSMC_UnitBo.Update(unit);
                }
                else
                {
                    unit = new SMC_Unit();
                    unit.Unit_CreatedTime = DateTime.Now;
                    unit.Unit_ID = org.OrgCode;
                    unit.Unit_CreatedUser = "";
                    unit.Unit_Demo = "";
                    unit.Unit_Name = org.OrgName;
                    if (parentUnit == null)
                    {
                        unit.Unit_Path = org.OrgCode;
                        unit.Upper_Unit_ID = "";
                    }
                    else
                    {
                        unit.Unit_Path = parentUnit.Unit_Path + ".." + org.OrgCode;
                        unit.Upper_Unit_ID = parentUnit.Unit_ID;
                    }
                    unit.Unit_Sequence = 0;
                    if (org != null)
                        unit.Unit_Sequence = org.OrgSequence;
                    unit.Unit_UpdateTime = DateTime.Now;
                    unit.Unit_UpdateUser = "";
                    unit.Upper_Unit_ID = "";

                    Bo.BoFactory.GetSMC_UnitBo.Insert(unit);
                }
            }
            catch (Exception ex)
            {
                SmartBox.Console.Common.Log4NetHelper.Error(ex);
            }  
        }
    }
}
