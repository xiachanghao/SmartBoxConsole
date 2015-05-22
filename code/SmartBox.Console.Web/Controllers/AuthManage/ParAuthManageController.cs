using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Bo;
using SmartBox.Console.Common;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using SmartBox.Console.Common.Entities;
using System;

namespace SmartBox.Console.Web.Controllers.AuthManage
{
    public partial class AuthManageController : MyControllerBase
    {
        public ActionResult GetUnitListByUnitCode(FormCollection form, string unit_id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);

            data = BoFactory.GetSMC_UnitBo.QueryUnitByUnitCode(view, unit_id);
            return Json(data);
        }

        public ActionResult GetUnitListByUpperUnitCode(FormCollection form, string upper_unit_id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);

            bool isSystemManager = BoFactory.GetVersionTrackBo.IsSystemManager(this.User.Identity.Name);
            if (isSystemManager)
                data = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view, upper_unit_id);
            else
            {
                //单位管理员只看自己单位列表
                string Unit_ID = BoFactory.GetSMC_UserListBo.GetUnitIdByUID(this.User.Identity.Name);
                data = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCodeWithSelfUnit(view, upper_unit_id, Unit_ID);
            }
            ViewData["unit_id"] = upper_unit_id;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFunctionListByUpperFNID(FormCollection form, string upper_fn_id)
        {
            JsonFlexiGridData data = null;
            PageView view = new PageView(form);
            int _upper_fn_id = 0;
            if (upper_fn_id != null)
            {
                _upper_fn_id = int.Parse(upper_fn_id);
            }
            data = BoFactory.GetSMC_FunctionsBo.QueryFunctionByUpperFNID(view, _upper_fn_id);
            return Json(data);
        }

        public ActionResult DelFunction(int fn_id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_Functions fn = BoFactory.GetSMC_FunctionsBo.Get(fn_id);

            //判断权限是否有下一级权限，如有则不能删除
            var func=BoFactory.GetSMC_FunctionsBo.GetFunctionsByUpperId(fn_id);
            int i = func.Count;

            //判断有否有角色关联
            //BoFactory.GetSMC_FunctionsBo.QueryFunctionsByRoleID

            if (i > 0)
            {
                data.IsSuccess = false;
                data.Msg = "已有下级权限，请先删除下级权限!";
            }else
            if (fn != null)
            {
                BoFactory.GetSMC_FunctionRoleBo.DeleteByFID(fn_id);
                BoFactory.GetSMC_FunctionsBo.Delete(fn); 
  
                //删除图标
                string saveDir = HttpRuntime.AppDomainAppPath + AppConfig.FuncIconFolder;
                string savePath = "";
                string fileName = fn_id.ToString() + "_SIcon.png";
                savePath = System.IO.Path.Combine(saveDir, fileName);              
                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }
                fileName = fn_id.ToString() + "_Icon.png";
                savePath = System.IO.Path.Combine(saveDir, fileName);
                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }

            }
            return Json(data);
        }

        public ActionResult DelUnit(string unit_id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_Unit unit = BoFactory.GetSMC_UnitBo.Get(unit_id);
            if (unit != null)
            {
                //判断是否被关联引用
                PageView view = new PageView();
                view.PageIndex = 0;
                view.PageSize = 15;

                //看是否有下级组织机构
                var units = BoFactory.GetSMC_UnitBo.QueryUnitByUpperUnitCode(view,unit_id);
                if (units.total > 0)
                {
                    data.IsSuccess = false;
                    data.Msg = "该组织存在下级组织，请先删除下级组织后再执行此操作!";
                    return Json(data);
                }

                var rel = BoFactory.GetSMC_RoleBo.QueryRolesByUnitID(view,unit_id);
                if (rel.total > 0)
                {
                    data.IsSuccess = false;
                    data.Msg = "有角色的组织不能删除，请先删除组织下的角色后再执行此操作!";
                    return Json(data);
                }
                else 
                {
                    rel = BoFactory.GetSMC_UserListBo.QueryUserListByUnitID(view,unit_id);
                    if (rel.total > 0)
                    {
                        data.IsSuccess = false;
                        data.Msg = "有用户的组织不能删除，请先删除组织下的用户后再执行此操作!";
                        return Json(data);
                    }
                }


            }
            else
            {
                data.IsSuccess = false;
                data.Msg = "组织不存在";
            }

            try
            {
                //从同意授权
                Beyondbit.BUA.Client.IOrgService os = Beyondbit.BUA.Client.ServiceFactory.Instance().GetOrgService();
                os.DelOrg(unit_id);

                
            }
            catch (Exception e)
            {
                //ata.IsSuccess = false;
                //data.Msg = e.Message;
            }
            
            if (unit != null)
            {
                BoFactory.GetSMC_UnitBo.Delete(unit);
            }
            
            return Json(data);
        }

        public ActionResult DelRole(int role_id) 
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_Role role = BoFactory.GetSMC_RoleBo.Get(role_id);

            if (role != null)
            {
                //判断是否被关联引用
                PageView view = new PageView();
                view.PageIndex = 0;
                view.PageSize = 15;

                var rel=BoFactory.GetSMC_UserListBo.QueryUserListHasRole(view, role_id);
                if (rel.total > 0)
                {
                    data.IsSuccess = false;
                    data.Msg = "角色已经分配给用户，请先取消分配关系!";
                    return Json(data);
                }
                else
                {
                    if (BoFactory.GetSMC_FunctionRoleBo.IsFuncAssigned(role_id))
                    {
                        data.IsSuccess = false;
                        data.Msg = "角色已经分配了权限，请先取消分配关系!";
                        return Json(data);
                    }
                }
            }
            else
            {
                data.IsSuccess = false;
                data.Msg = "角色不存在";
            }
            if (role != null)
            {
                //BoFactory.GetSMC_UserListBo.DeleteByRoleID(role_id);
                BoFactory.GetSMC_RoleBo.Delete(role);
            }
            return Json(data);
        }

        public ActionResult DelUser(int ul_id)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_UserList ur = BoFactory.GetSMC_UserListBo.Get(ul_id);
            if (ur != null && CurrentUser.UserUId != ur.UL_UID)
            {
                BoFactory.GetSMC_UserListBo.Delete(ur);
            }
            else
            {
                data.IsSuccess = false;
                data.Msg = "不能删除自己的账号";
            }

            return Json(data);
        }

        public ActionResult AddModifyUnitSave(string Unit_ID, string Upper_Unit_ID, string Unit_Name, int Unit_Sequence, string Unit_Demo)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            SMC_Unit unit = null;
            if (String.IsNullOrEmpty(Unit_ID))
            {
                unit = new SMC_Unit();
                unit.Unit_CreatedTime = DateTime.Now;
            }
            else
            {
                unit = BoFactory.GetSMC_UnitBo.Get(Unit_ID);
            }
            unit.Unit_Demo = Unit_Demo;
            unit.Unit_Name = Unit_Name;
            unit.Unit_Sequence = Unit_Sequence;
            unit.Upper_Unit_ID = Upper_Unit_ID;
            unit.Unit_ID = Unit_ID;

            try
            {
                BoFactory.GetSMC_UnitBo.InsertOrUpdate(unit);
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Msg = ex.Message;
                return Json(data);
            }
            
            SMC_Unit _old_unit = unit;

            string pathString = unit.Unit_ID.ToString();
            while (unit != null && !String.IsNullOrEmpty(unit.Upper_Unit_ID))
            {
                pathString = unit.Upper_Unit_ID + ".." + pathString;
                if (String.IsNullOrEmpty(unit.Upper_Unit_ID))
                    unit = null;
                else
                    unit = BoFactory.GetSMC_UnitBo.Get(unit.Upper_Unit_ID);
            }
            _old_unit.Unit_Path = pathString;
            BoFactory.GetSMC_UnitBo.InsertOrUpdate(_old_unit);

            return Json(data);
        }

        public ActionResult FunctionList(int upper_fn_id)
        {
            return View();
        }

        public ActionResult AddModifyFunction(int? FN_ID)
        {
            SMC_Functions func = null;
            if (FN_ID != null && FN_ID.Value > 0)
            {
                func = BoFactory.GetSMC_FunctionsBo.Get(FN_ID.Value);
            }
            int Upper_FN_ID = int.Parse(Request.QueryString["Upper_FN_ID"]);
            string Upper_FN_Name = string.Empty;
            if (Upper_FN_ID != null && Upper_FN_ID > 0)
            {
                Upper_FN_Name = BoFactory.GetSMC_FunctionsBo.Get(Upper_FN_ID).FN_Name;
            }
            else 
            {
                Upper_FN_Name = "权限";
            }


            //权限列表
            List<SelectListItem> fnList = new List<SelectListItem>();
            IList<SMC_Functions> funcs = BoFactory.GetSMC_FunctionsBo.GetAllFunctions();
            if (Upper_FN_ID == 0)
            {
                fnList.Add(new SelectListItem { Text = "权限", Value = "0", Selected = true });
            }
            else            
            {
                fnList.Add(new SelectListItem { Text = "权限", Value = "0", Selected = false });
            }
            foreach (SMC_Functions f in funcs)
            {
                if (f.FN_ID != FN_ID)
                {
                    fnList.Add(new SelectListItem { Text = f.FN_Name, Value = f.FN_ID.ToString(), Selected = f.FN_ID == Upper_FN_ID });
                }
            }

            //权限类型列表
            List<SelectListItem> fn_type = new List<SelectListItem>();
            fn_type.Add(new SelectListItem { Text = "菜单", Value = "Menu"});
            fn_type.Add(new SelectListItem { Text = "权限", Value = "Func"});
            fn_type.Add(new SelectListItem { Text = "目录", Value = "Dir" });
            if (func != null)
            {
                foreach (var sitem in fn_type)
                {
                    if (sitem.Value == func.FN_Type)
                    {
                        sitem.Selected = true;
                        break;
                    }
                }
            }
            
            //可见类型列表
            List<SelectListItem> fn_visibletype = new List<SelectListItem>();
            fn_visibletype.Add(new SelectListItem { Text = "所有人可见", Value = "0" });
            fn_visibletype.Add(new SelectListItem { Text = "只单位管理员可见", Value = "1" });
            fn_visibletype.Add(new SelectListItem { Text = "只系统管理员可见", Value = "2" });

            //是否锁定
            List<SelectListItem> fn_disabled = new List<SelectListItem>();
            fn_disabled.Add(new SelectListItem { Text = "未锁定", Value = "0" });
            fn_disabled.Add(new SelectListItem { Text = "锁定", Value = "1" });

            ViewData["Upper_FN_Name"] = fnList;
            ViewData["fn_type"] = fn_type;
            ViewData["fn_visibletype"] = fn_visibletype;
            ViewData["fn_disabled"] = fn_disabled;
            //ViewData["Upper_FN_Name"] = Upper_FN_Name;
            return View(func);
        }

        public ActionResult AddModifyFunctionSave(string FN_ID, string Unit_ID, string FN_Code, string Upper_FN_ID,string Upper_FN_Name, string fn_type, string fn_disabled, string fn_visibletype, string FN_Name, string FN_Url, int FN_Sequence, string FN_Demo)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };
            if (BoFactory.GetSMC_FunctionsBo.ExistsByCode(FN_Code) && int.Parse(FN_ID) == 0)
            {
                data = new JsonReturnMessages() { IsSuccess = false, Msg = "权限Code已经存在！" };
                return Json(data);
            }
            

            SMC_Functions func = null;
            if (FN_ID == "0" || String.IsNullOrEmpty(FN_ID))
            {
                func = new SMC_Functions();
                func.FN_CreatedTime = DateTime.Now;
            }
            else
            {
                func = BoFactory.GetSMC_FunctionsBo.Get(int.Parse(FN_ID));
            }
            func.FN_VisibleType = fn_visibletype;
            func.FN_Disabled = fn_disabled == "1" ? true : false;
            func.FN_Demo = FN_Demo;
            func.FN_Name = FN_Name;
            func.FN_Code = FN_Code;
            func.FN_Sequence = FN_Sequence;
            int upper_fn_id = 0;
            if (!String.IsNullOrEmpty(Upper_FN_Name))
                upper_fn_id = int.Parse(Upper_FN_Name);
            //int unit_id = 0;
            //if (!String.IsNullOrEmpty(Unit_ID))
            //    unit_id = int.Parse(Unit_ID);

            func.Upper_FN_ID = upper_fn_id;
            func.FN_ID = int.Parse(FN_ID);
            func.FN_Url = FN_Url;
            func.FN_Type = fn_type;
            func.Unit_ID = Unit_ID;

            BoFactory.GetSMC_FunctionsBo.InsertOrUpdate(func);

            string saveDir = HttpRuntime.AppDomainAppPath + AppConfig.FuncIconFolder;
            string savePath = "";
            if (Request.Files.Count > 0)
            {
                if (Request.Files[0].ContentLength > 0)
                {
                    //有小图标文件
                    string fileName = func.FN_ID.ToString() + "_SIcon.png";
                    savePath = System.IO.Path.Combine(saveDir, fileName);
                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    Request.Files[0].SaveAs(savePath);
                }
                if (Request.Files[1].ContentLength > 0)
                {
                    //有图标文件
                    string fileName = func.FN_ID.ToString() + "_Icon.png";
                    savePath = System.IO.Path.Combine(saveDir, fileName);
                    if (!System.IO.Directory.Exists(saveDir))
                    {
                        System.IO.Directory.CreateDirectory(saveDir);
                    }
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    Request.Files[1].SaveAs(savePath);
                }
            }
           
            return Json(data, "text/html");
                 
        }
    }
}