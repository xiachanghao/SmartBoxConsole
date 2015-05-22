using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.UserMenu
{
    public class UserMenuNew : IUserMenu
    {
        public List<Common.AccordionItem> GetCurrentUserMenu(System.Web.Mvc.Controller controller)
        {
            List<AccordionItem> menulist = new List<AccordionItem>();
            IList<SmartBox.Console.Common.Privilege> list = GetList(controller);
            if (list != null)
            {
                var topmenu = list.Where(x => x.ParentID == "0");

                if (topmenu != null)
                {
                    foreach (SmartBox.Console.Common.Privilege p in topmenu)
                    {
                        AccordionItem item = new AccordionItem();
                        item.Text = p.PrivilegeName;
                        item.Code = p.PrivilegeCode;
                        item.IsExpand = false;
                        item.Url = p.Uri;
                        item.ID = p.ID;
                        item.IcoSrc = "";
                        SmartBox.Console.Common.Privilege privilege = p;
                        var child = list.Where(x => x.ParentID == privilege.ID.ToString());
                        if (child != null)
                        {
                            foreach (SmartBox.Console.Common.Privilege pc in child)
                            {
                                AccordionItem citem = new AccordionItem();
                                citem.Text = pc.PrivilegeName;
                                citem.Code = pc.PrivilegeCode;
                                citem.Url = pc.Uri;
                                citem.IcoSrc = "";
                                citem.ID = pc.ID;
                                citem.Type = pc.PrivilegeType.ToString();
                             
                                //检查citem的节点，如有则添加
                                SmartBox.Console.Common.Privilege lefprivilege = pc;
                                var childleaf = list.Where(x => x.ParentID == pc.ID.ToString());
                                foreach (SmartBox.Console.Common.Privilege pcleaf in childleaf)
                                {
                                    AccordionItem litem = new AccordionItem();
                                    litem.Text = pcleaf.PrivilegeName;
                                    litem.Code = pcleaf.PrivilegeCode;
                                    litem.Url = pcleaf.Uri;
                                    litem.IcoSrc = "";
                                    litem.ID = pcleaf.ID;

                                    citem.Children.Add(litem);
                                }

                                item.Children.Add(citem);
                            }
                        }
                        menulist.Add(item);
                    }
                }
            }
            return menulist;
        }

        public IList<SmartBox.Console.Common.Privilege> GetList(System.Web.Mvc.Controller controller)
        {
            string uid = controller.User.Identity.Name;
            List<SMC_Functions> funcs = (List<SMC_Functions>)SmartBox.Console.Bo.BoFactory.GetSMC_FunctionsBo.QueryFunctionsListByUID(uid);
            IList<SmartBox.Console.Common.Privilege> result = new List<SmartBox.Console.Common.Privilege>();
            foreach (SMC_Functions func in funcs)
            {
                SmartBox.Console.Common.Privilege p = new SmartBox.Console.Common.Privilege();
                p.HaveChildren = false;
                p.ParentID = func.Upper_FN_ID.ToString();
                p.PrivilegeType = (PrivilegeType)Enum.Parse(typeof(PrivilegeType), func.FN_Type);// PrivilegeType.Menu;
               
                p.PrivilegeCode = func.FN_Code;
                p.ID = func.FN_ID;
                p.PrivilegeName = func.FN_Name;
                p.Uri = func.FN_Url;

                
                result.Add(p);
            }
            return result;
        }
    }
}
