using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IGoDeliverAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "员工登录界面";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "关于我们";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "联系我们";

            return View();
        }
    }
}
