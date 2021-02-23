using MVC.Models;
using MVC.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.DbDal;
namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("LoginPage");
        }
        public ActionResult LoginPage()
        {
            UserViewModel cvm = new UserViewModel();
            User objUser = new User();
            Dal dal = new Dal();
            List<User> objUsers = dal.Users.ToList();

            if (Request.Form.Count > 0)
            {
                objUser.Id = Request.Form["User.Id"].ToString();
                objUser.Password = Request.Form["User.Password"].ToString();

                foreach (User x in objUsers)//to make sure the username is valid
                {
                    if (x.Id == objUser.Id && x.Password == objUser.Password)
                    {
                        cvm.User = x;//add the current user to the CVM user
                        Session["currUserId"] = x.Id;
                        Session["currUserType"] = x.Type;
                        Session["currName"] = x.Name;
                        HomeViewModel hvm = new HomeViewModel();
                        return View("homePage", hvm);
                    }
                }
                cvm.User = new User();
                ViewBag.UserORPassError = "Id or Password incorrect! please try again";
                return View("LoginPage", cvm);//Id Or Pass not found
            }
            return View("LoginPage");
        }

        public ActionResult Logout()
        {
          
            Session["currUserId"] = null;//clean session items
            Session["currUserType"] = null;


            UserViewModel cvm = new UserViewModel();
            cvm.User = new User();//clean curr user

            return View("LoginPage", cvm);//username Or Pass not found
        }
    }
}