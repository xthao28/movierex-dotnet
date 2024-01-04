using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRex.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentUser
        {
            get
            {
                return HttpContext.Session.GetString("USER_NAME");
            }
            set
            {
                HttpContext.Session.SetString("USER_NAME", value);
            }
        }

        public bool IsLogin
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentUser);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.UserName = CurrentUser;
        }
    }
}

