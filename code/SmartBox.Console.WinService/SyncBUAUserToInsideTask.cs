using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using Beyondbit.BUA.Client;
using SmartBox.Console.Common;
using System.Xml;
using SmartBox.Console.Bo;
using System.Collections;
using System.Configuration;

namespace SmartBox.Console.WinService
{
    public class SyncBUAUserToInsideTask : WinService.WinService<Beyondbit.BUA.Client.User>
    {
        //List<SMC_PackageFAQ> packages = null;
        Beyondbit.BUA.Client.Org[] arr = null;
        IOrgService orgService = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
        IUserService userService = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();

        public SyncBUAUserToInsideTask()
        {

        }

        public override List<Beyondbit.BUA.Client.User> GetDataSource()
        {
            
        //    return new List<Beyondbit.BUA.Client.Org>();
        //    //IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
        //    //Tuple<string, string, object> par = new Tuple<string, string, object>("pe_authstatus", "=", 0);//已通过审核
        //    //pars.Add(par);

        //    //Tuple<string, string, object> par2 = new Tuple<string, string, object>("pe_id", "=", 75);//未锁定
        //    //pars.Add(par2);

        //    //Tuple<string, string, object> par3 = new Tuple<string, string, object>("u_need_sync", "=", 0);//不需要同步的，也就是还没有比较的
        //    //pars.Add(par3);

        //    //packages = Bo.BoFactory.GetSMC_PackageFAQBO.QueryList(pars);
        //    //arr = userService.QueryUnits("");

        //    if (arr != null && arr.Length > 0) {
        //        //packages = new List<SMC_PackageFAQ>();
                
        //    }

            
            return null;
        }

        Org topOrg = null;
        public override void Execute()
        {
            try
            {
                //Beyondbit.BUA.Client.Model.SearchUser su = new Beyondbit.BUA.Client.Model.SearchUser();
                //su.UserType = "GeneralUser";
                //Beyondbit.BUA.Client.User[] _users = userService.QueryUsersByPersonalInfo(su);
                //int i = 0;
                //return;
                Log4NetHelper.Info("同步bua用户开始");
                System.Console.WriteLine("同步bua用户开始");
                IList<SMC_Unit> units = Bo.BoFactory.GetSMC_UnitBo.GetAllUnits();

                string bua_org_code_root = ConfigurationManager.AppSettings["bua_org_code_root"];
                string orgs = orgService.QueryAllOrgTree(bua_org_code_root);

                if (!String.IsNullOrEmpty(orgs))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(orgs);
                    XmlNodeList nodes = doc.DocumentElement.SelectNodes("//root/node");
                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string orgcode = node.Attributes["id"].Value;
                            string orgname = node.Attributes["text"].Value;
                            Log4NetHelper.Info("同步单位" + orgname + "的用户开始");
                            System.Console.WriteLine("同步单位" + orgname + "的用户开始");
                            string users = userService.QueryUserTree(orgcode);
                            syncUsers(users);
                        }
                    }

                    nodes = doc.DocumentElement.SelectNodes("//root/node/node");
                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string orgcode = node.Attributes["id"].Value;
                            string orgname = node.Attributes["text"].Value;
                            Log4NetHelper.Info("同步单位" + orgname + "的用户开始");
                            System.Console.WriteLine("同步单位" + orgname + "的用户开始");
                            string users = userService.QueryUserTree(orgcode);
                            syncUsers(users);
                        }
                    }

                    nodes = doc.DocumentElement.SelectNodes("//root/node/node/node");
                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string orgcode = node.Attributes["id"].Value;
                            string orgname = node.Attributes["text"].Value;
                            Log4NetHelper.Info("同步单位" + orgname + "的用户开始");
                            System.Console.WriteLine("同步单位" + orgname + "的用户开始");
                            string users = userService.QueryUserTree(orgcode);
                            syncUsers(users);
                        }
                    }

                    nodes = doc.DocumentElement.SelectNodes("//root/node/node/node/node");
                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string orgcode = node.Attributes["id"].Value;
                            string orgname = node.Attributes["text"].Value;
                            Log4NetHelper.Info("同步单位" + orgname + "的用户开始");
                            System.Console.WriteLine("同步单位" + orgname + "的用户开始");
                            string users = userService.QueryUserTree(orgcode);
                            syncUsers(users);
                        }
                    }

                    nodes = doc.DocumentElement.SelectNodes("//root/node/node/node/node/node");
                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string orgcode = node.Attributes["id"].Value;
                            string orgname = node.Attributes["text"].Value;
                            Log4NetHelper.Info("同步单位" + orgname + "的用户开始");
                            System.Console.WriteLine("同步单位" + orgname + "的用户开始");
                            string users = userService.QueryUserTree(orgcode);
                            syncUsers(users);
                        }
                    }
                }
                Log4NetHelper.Info("同步bua用户结束");
                System.Console.WriteLine("同步bua用户结束");
                //return;
                //if (units != null && units.Count > 0)
                //{
                //    foreach (SMC_Unit u in units)
                //    {
                //        Log4NetHelper.Info("同步bua用户开始");
                //        string users = userService.QueryUserTree(u.Unit_ID);

                //        syncUsers(users);
                //    }
                //    Log4NetHelper.Info("同步bua用户结束");
                //    System.Console.WriteLine("同步bua用户结束");
                //}
                //else
                //{
                //    Log4NetHelper.Info("请先同步单位后再同步bua用户");
                //}
                //
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex);
            }
            //if (arr != null && arr.Length > 0)
            //{
            //    topOrg = orgService.GetTopOrg();
            //    SyncOrg(topOrg);                
            //}
            //System.Console.WriteLine("bua单位同步完成!");
            
        }

        private void syncUsers(string users)
        {
            if (!String.IsNullOrEmpty(users))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(users);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("//root/node");
                StringBuilder builder = new StringBuilder();
                if (nodes != null)
                    foreach (XmlNode node in nodes)
                    {
                        string uid = node.Attributes["id"].Value;
                        builder.Append(uid);
                        builder.Append(",");
                    }
                SMC_BUAUserSyncToInsideBO buaSyncBO = Bo.BoFactory.GetSMC_BUAUserSyncToInsideBO;

                string ids = builder.ToString();
                try
                {
                    Hashtable result = new Hashtable();
                    buaSyncBO.BUAUserSyncToInside(ids, result);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }

                try
                {
                    buaSyncBO.BUAUserSyncToOutside(ids);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }
            }
        }

        //private void SyncOrg(Org parentOrg)
        //{
        //    arr = orgService.QuerySubOrgs(parentOrg.OrgCode);

        //    foreach (Beyondbit.BUA.Client.Org org in arr)
        //    {
        //        if (parentOrg.OrgCode != topOrg.OrgCode && org.OrgType != "Unit_Org")
        //            continue;
        //        if (topOrg.OrgCode == parentOrg.OrgCode)
        //            insertOrUpdateOrg(org, null);
        //        else
        //        {
        //            IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
        //            pars.Add(new KeyValuePair<string, object>("unit_id", parentOrg.OrgCode));
        //            SMC_Unit parent_unit = Bo.BoFactory.GetSMC_UnitBo.Get(pars);

        //            insertOrUpdateOrg(org, parent_unit);
        //        }
        //        SyncOrg(org);
        //    }
        //}

        //private void insertOrUpdateOrg(Org org, SMC_Unit parentUnit)
        //{
        //    try
        //    {
        //        IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
        //        pars.Add(new KeyValuePair<string, object>("unit_id", org.OrgCode));

        //        SMC_Unit unit = Bo.BoFactory.GetSMC_UnitBo.Get(pars);
        //        //更新到内网
        //        if (unit != null)
        //        {
        //            unit.Unit_Name = org.OrgName;
        //            unit.Unit_UpdateTime = DateTime.Now;
        //            unit.Unit_UpdateUser = "";
        //            if (parentUnit == null)
        //            {
        //                unit.Unit_Path = org.OrgCode;
        //                unit.Upper_Unit_ID = "";
        //            }
        //            else
        //            {
        //                unit.Unit_Path = parentUnit.Unit_Path + ".." + org.OrgCode;
        //                unit.Upper_Unit_ID = parentUnit.Unit_ID;
        //            }
        //            Bo.BoFactory.GetSMC_UnitBo.Update(unit);
        //        }
        //        else
        //        {
        //            unit = new SMC_Unit();
        //            unit.Unit_CreatedTime = DateTime.Now;
        //            unit.Unit_ID = org.OrgCode;
        //            unit.Unit_CreatedUser = "";
        //            unit.Unit_Demo = "";
        //            unit.Unit_Name = org.OrgName;
        //            if (parentUnit == null)
        //            {
        //                unit.Unit_Path = org.OrgCode;
        //                unit.Upper_Unit_ID = "";
        //            }
        //            else
        //            {
        //                unit.Unit_Path = parentUnit.Unit_Path + ".." + org.OrgCode;
        //                unit.Upper_Unit_ID = parentUnit.Unit_ID;
        //            }
        //            unit.Unit_Sequence = 0;
        //            unit.Unit_UpdateTime = DateTime.Now;
        //            unit.Unit_UpdateUser = "";
        //            unit.Upper_Unit_ID = "";

        //            Bo.BoFactory.GetSMC_UnitBo.Insert(unit);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SmartBox.Console.Common.Log4NetHelper.Error(ex);
        //    }  
        //}
    }
}
