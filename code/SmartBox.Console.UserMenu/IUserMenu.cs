using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;

namespace SmartBox.Console.UserMenu
{
    public interface IUserMenu
    {
        List<AccordionItem> GetCurrentUserMenu(System.Web.Mvc.Controller controller);
        IList<SmartBox.Console.Common.Privilege> GetList(System.Web.Mvc.Controller controller);
    }
}
