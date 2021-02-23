using MVC.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult homePage()
        {
            HomeViewModel hvm = new HomeViewModel();
            return View();
        }

    }
}