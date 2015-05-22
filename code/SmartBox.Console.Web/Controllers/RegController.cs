using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartBox.Console.Bo;
using SmartBox.Console.Common;
using System.Web.Script.Serialization;
using System.Collections;
using SmartBox.Console.Common.Entities;
using System.Data;

namespace SmartBox.Console.Web.Controllers
{
    public class RegController : Controller
    {
        //
        // GET: /Reg/

        public ActionResult RegUser()
        {
            PageView view =new PageView();
            view.PageIndex = 0;
            view.PageSize = 35;
            view.RecordCount = 0;
            JsonFlexiGridData data = Bo.BoFactory.GetSMC_UnitBo.QueryUnitByUnitCode(view, "");
            
            List<SelectListItem> unitList = new List<SelectListItem>();
            foreach (FlexiGridRow r in data.rows)
            {
                SelectListItem u = new SelectListItem();
                u.Value =r.cell[0];
                u.Text = r.cell[1];
                unitList.Add(u);
            }

            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem { Text = "男", Value = "male" });
            genderList.Add(new SelectListItem { Text = "女", Value = "female" });
            
            ViewData["Unit_ID"] = unitList;
            ViewData["UL_Gender"] = genderList;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegUser(FormCollection form)
        {
            JsonReturnMessages data = new JsonReturnMessages() { IsSuccess = true, Msg = "操作成功" };

            SMC_UserList ur = new SMC_UserList();

            ur.UL_ID = -1;
            ur.UL_UID = form[0];
            ur.UL_Name = form[1];
            ur.UL_PWD = form[2];
            ur.UL_MobilePhone = form[4];
            ur.UL_MailAddress = form[5];
            ur.Unit_ID =form[6];
            ur.UL_Sequence = Int32.Parse(form[7]);
            ur.UL_Demo = form[8];
            ur.UL_Gender = form[9];

            BoFactory.GetSMC_UserListBo.InsertOrUpdate(ur);

            return Json(data);            
        }

    }
}
