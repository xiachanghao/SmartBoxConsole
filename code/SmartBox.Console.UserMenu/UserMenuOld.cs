﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Common;

namespace SmartBox.Console.UserMenu
{
    public class UserMenuOld : IUserMenu
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
                        item.IcoSrc = "";
                        SmartBox.Console.Common.Privilege privilege = p;
                        var child = list.Where(x => x.ParentID == privilege.PrivilegeCode);
                        if (child != null)
                        {
                            foreach (SmartBox.Console.Common.Privilege pc in child)
                            {
                                AccordionItem citem = new AccordionItem();
                                citem.Text = pc.PrivilegeName;
                                citem.Code = pc.PrivilegeCode;
                                citem.Url = pc.Uri;
                                citem.IcoSrc = "";
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
            IList<SmartBox.Console.Common.Privilege> list = new List<SmartBox.Console.Common.Privilege>();
            SmartBox.Console.Common.Privilege p = new SmartBox.Console.Common.Privilege();
            p.HaveChildren = false;
            p.ParentID = "0";
            p.PrivilegeType = PrivilegeType.Menu;
            p.PrivilegeCode = "mainManage";
            p.PrivilegeName = "程序管理";
            list.Add(p);
            SmartBox.Console.Common.Privilege p1 = new SmartBox.Console.Common.Privilege();
            p1.HaveChildren = false;
            p1.ParentID = "mainManage";
            p1.PrivilegeType = PrivilegeType.Menu;
            p1.PrivilegeCode = "PluginInfoManage";
            p1.PrivilegeName = "插件管理";
            p1.Uri = controller.Url.Action("PluginInfoManage", "PluginInfoManage");
            list.Add(p1);
            SmartBox.Console.Common.Privilege p2 = new SmartBox.Console.Common.Privilege();
            p2.HaveChildren = false;
            p2.ParentID = "mainManage";
            p2.PrivilegeType = PrivilegeType.Menu;
            p2.PrivilegeCode = "MianInfoManage";
            p2.PrivilegeName = "主程序管理";
            p2.Uri = controller.Url.Action("MainInfoManage", "MainInfoManage");
            list.Add(p2);
            SmartBox.Console.Common.Privilege p3 = new SmartBox.Console.Common.Privilege();
            p3.HaveChildren = false;
            p3.ParentID = "mainManage";
            p3.PrivilegeType = PrivilegeType.Menu;
            p3.PrivilegeCode = "UpdaterManage";
            p3.PrivilegeName = "升级程序管理";
            p3.Uri = controller.Url.Action("UpdaterManage", "UpdaterManage");
            list.Add(p3);
            SmartBox.Console.Common.Privilege p4 = new SmartBox.Console.Common.Privilege();
            p4.HaveChildren = false;
            p4.ParentID = "mainManage";
            p4.PrivilegeType = PrivilegeType.Menu;
            p4.PrivilegeCode = "LogInfoManage";
            p4.PrivilegeName = "日志查看";
            p4.Uri = controller.Url.Action("LogInfoManage", "LogInfoManage");
            list.Add(p4);
            SmartBox.Console.Common.Privilege p5 = new SmartBox.Console.Common.Privilege();
            p5.HaveChildren = false;
            p5.ParentID = "mainManage";
            p5.PrivilegeType = PrivilegeType.Menu;
            p5.PrivilegeCode = "UserInfoManage";
            p5.PrivilegeName = "用户摘要";
            p5.Uri = controller.Url.Action("UserInfoManage", "UserInfoManage");
            list.Add(p5);
            SmartBox.Console.Common.Privilege p6 = new SmartBox.Console.Common.Privilege();
            p6.HaveChildren = false;
            p6.ParentID = "mainManage";
            p6.PrivilegeType = PrivilegeType.Menu;
            p6.PrivilegeCode = "StyleManage";
            p6.PrivilegeName = "登录页面资源管理";
            p6.Uri = controller.Url.Action("StyleManage", "StyleManage");
            list.Add(p6);
            SmartBox.Console.Common.Privilege p7 = new SmartBox.Console.Common.Privilege();
            p7.HaveChildren = false;
            p7.ParentID = "mainManage";
            p7.PrivilegeType = PrivilegeType.Menu;
            p7.PrivilegeCode = "SkinManage";
            p7.PrivilegeName = "皮肤资源管理";
            p7.Uri = controller.Url.Action("SkinManage", "SkinManage");
            //list.Add(p7);
            SmartBox.Console.Common.Privilege p8 = new SmartBox.Console.Common.Privilege();
            p8.HaveChildren = false;
            p8.ParentID = "mainManage";
            p8.PrivilegeType = PrivilegeType.Menu;
            p8.PrivilegeCode = "ConferenceManage";
            p8.PrivilegeName = "即时通讯群列表";
            p8.Uri = controller.Url.Action("IMGroupManage", "IMGroupManage");
            list.Add(p8);
            SmartBox.Console.Common.Privilege p9 = new SmartBox.Console.Common.Privilege();
            p9.HaveChildren = false;
            p9.ParentID = "0";
            p9.PrivilegeType = PrivilegeType.Menu;
            p9.PrivilegeCode = "appManage";
            p9.PrivilegeName = "应用管理";
            list.Add(p9);
            SmartBox.Console.Common.Privilege p10 = new SmartBox.Console.Common.Privilege();
            p10.HaveChildren = false;
            p10.ParentID = "appManage";
            p10.PrivilegeType = PrivilegeType.Menu;
            p10.PrivilegeCode = "ApplicationManage";
            p10.PrivilegeName = "应用管理";
            p10.Uri = controller.Url.Action("ApplicationManage", "ApplicationManage");
            list.Add(p10);

            SmartBox.Console.Common.Privilege p16 = new Common.Privilege();
            p16.HaveChildren = false;
            p16.ParentID = "appManage";
            p16.PrivilegeType = PrivilegeType.Menu;
            p16.PrivilegeCode = "IOSOutsideAppManage";
            p16.PrivilegeName = "IOS外部应用管理";
            p16.Uri = controller.Url.Action("IOSOutsideAppManage", "IOSOutsideAppManage");
            list.Add(p16);

            SmartBox.Console.Common.Privilege p17 = new Common.Privilege();
            p17.HaveChildren = false;
            p17.ParentID = "appManage";
            p17.PrivilegeType = PrivilegeType.Menu;
            p17.PrivilegeCode = "MainGlobalInfoManage";
            p17.PrivilegeName = "全局配置管理";
            p17.Uri = controller.Url.Action("MainGlobalInfoManage", "MainInfoManage");
            list.Add(p17);

            SmartBox.Console.Common.Privilege p11 = new SmartBox.Console.Common.Privilege();
            p11.HaveChildren = false;
            p11.ParentID = "appManage";
            p11.PrivilegeType = PrivilegeType.Menu;
            p11.PrivilegeCode = "WebApplicationManage";
            p11.PrivilegeName = "Web应用管理";
            p11.Uri = controller.Url.Action("WebApplicationManage", "ApplicationManage");
            list.Add(p11);
            SmartBox.Console.Common.Privilege p12 = new SmartBox.Console.Common.Privilege();
            p12.HaveChildren = false;
            p12.ParentID = "appManage";
            p12.PrivilegeType = PrivilegeType.Menu;
            p12.PrivilegeCode = "ApplicationPackageManage";
            p12.PrivilegeName = "应用扩展管理";
            p12.Uri = controller.Url.Action("ApplicationPackageManage", "ApplicationManage");
            list.Add(p12);
            SmartBox.Console.Common.Privilege p13 = new SmartBox.Console.Common.Privilege();
            p13.HaveChildren = false;
            p13.ParentID = "appManage";
            p13.PrivilegeType = PrivilegeType.Menu;
            p13.PrivilegeCode = "PrivilegeManage";
            p13.PrivilegeName = "应用权限管理";
            p13.Uri = controller.Url.Action("PrivilegeManage", "ApplicationManage");
            list.Add(p13);
            SmartBox.Console.Common.Privilege p14 = new SmartBox.Console.Common.Privilege();
            p14.HaveChildren = false;
            p14.ParentID = "appManage";
            p14.PrivilegeType = PrivilegeType.Menu;
            p14.PrivilegeCode = "ApplicationCategoryManage";
            p14.PrivilegeName = "应用分类管理";
            p14.Uri = controller.Url.Action("ApplicationCategoryManage", "ApplicationManage");
            list.Add(p14);
            SmartBox.Console.Common.Privilege p15 = new SmartBox.Console.Common.Privilege();
            p15.HaveChildren = false;
            p15.ParentID = "appManage";
            p15.PrivilegeType = PrivilegeType.Menu;
            p15.PrivilegeCode = "HomePlanManage";
            p15.PrivilegeName = "Home布局管理";
            p15.Uri = controller.Url.Action("HomePlanList", "ApplicationManage");
            list.Add(p15);

            SmartBox.Console.Common.Privilege p20 = new SmartBox.Console.Common.Privilege();
            p20.HaveChildren = false;
            p20.ParentID = "0";
            p20.PrivilegeType = PrivilegeType.Menu;
            p20.PrivilegeCode = "StatisticManage";
            p20.PrivilegeName = "统计";
            list.Add(p20);

            SmartBox.Console.Common.Privilege p18 = new SmartBox.Console.Common.Privilege();
            p18.HaveChildren = false;
            p18.ParentID = "StatisticManage";
            p18.PrivilegeType = PrivilegeType.Menu;
            p18.PrivilegeCode = "StatisticManage";
            p18.PrivilegeName = "数据访问量统计";
            p18.Uri = controller.Url.Action("ShowIndex", "StatisticManage");
            list.Add(p18);

            SmartBox.Console.Common.Privilege p19 = new SmartBox.Console.Common.Privilege();
            p19.HaveChildren = false;
            p19.ParentID = "StatisticManage";
            p19.PrivilegeType = PrivilegeType.Menu;
            p19.PrivilegeCode = "StatisticManage";
            p19.PrivilegeName = "子系统访问量统计";
            p19.Uri = controller.Url.Action("AppNameIndex", "StatisticManage");
            list.Add(p19);

            SmartBox.Console.Common.Privilege p21 = new SmartBox.Console.Common.Privilege();
            p21.HaveChildren = false;
            p21.ParentID = "StatisticManage";
            p21.PrivilegeType = PrivilegeType.Menu;
            p21.PrivilegeCode = "StatisticManage";
            p21.PrivilegeName = "在线时长统计";
            p21.Uri = controller.Url.Action("TimeIndex", "StatisticManage");
            list.Add(p21);

            SmartBox.Console.Common.Privilege p22 = new SmartBox.Console.Common.Privilege();
            p22.HaveChildren = false;
            p22.ParentID = "StatisticManage";
            p22.PrivilegeType = PrivilegeType.Menu;
            p22.PrivilegeCode = "StatisticManage";
            p22.PrivilegeName = "用户访问量统计";
            p22.Uri = controller.Url.Action("UserIndex", "StatisticManage");
            list.Add(p22);

            SmartBox.Console.Common.Privilege p23 = new SmartBox.Console.Common.Privilege();
            p23.HaveChildren = false;
            p23.ParentID = "appManage";
            p23.PrivilegeType = PrivilegeType.Menu;
            p23.PrivilegeCode = "ApplyDeviceBind";
            p23.PrivilegeName = "申请设备绑定";
            p23.Uri = controller.Url.Action("ApplyDeviceBind", "DeviceBindManage");
            list.Add(p23);

            SmartBox.Console.Common.Privilege p24 = new SmartBox.Console.Common.Privilege();
            p24.HaveChildren = false;
            p24.ParentID = "appManage";
            p24.PrivilegeType = PrivilegeType.Menu;
            p24.PrivilegeCode = "ViewDeviceBind";
            p24.PrivilegeName = "已绑定设备管理";
            p24.Uri = controller.Url.Action("ViewDeviceBind", "DeviceBindManage");
            list.Add(p24);

            //SmartBox.Console.Common.Privilege p25 = new SmartBox.Console.Common.Privilege();
            //p25.HaveChildren = false;
            //p25.ParentID = "0";
            //p25.PrivilegeType = PrivilegeType.Menu;
            //p25.PrivilegeCode = "AuthManage";
            //p25.PrivilegeName = "账号管理";
            //list.Add(p25);

            //SmartBox.Console.Common.Privilege p26 = new SmartBox.Console.Common.Privilege();
            //p26.HaveChildren = false;
            //p26.ParentID = "AuthManage";
            //p26.PrivilegeType = PrivilegeType.Menu;
            //p26.PrivilegeCode = "UnitList";
            //p26.PrivilegeName = "组织管理";
            //p26.Uri = Url.Action("UnitList", "AuthManage");
            //list.Add(p26);

            return list;
        }
    }
}
