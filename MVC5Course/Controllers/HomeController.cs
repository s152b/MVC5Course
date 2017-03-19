using MVC5Course.ActionFilterAttributes;
using MVC5Course.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [紀錄Action執行時間]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult About(string ex="")
        {
            ViewBag.Message = "Your application description page.";
            if (ex.ToUpper().Contains("ERR"))
            {
                throw new OutOfMemoryException("故意的例外錯誤");
            }
            return View();
        }

        [僅在本機開發測試用Attribute]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                ///////////////////////////////
                //string userData = "ApplicationSpecific data for this user";

                //string strUsername = "你想要存放在 User.Identy.Name 的值，通常是使用者帳號";
                //bool isPersistent = true;
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                //  strUsername,
                //  DateTime.Now,
                //  DateTime.Now.AddMinutes(30),
                //  isPersistent,
                //  userData,
                //  FormsAuthentication.FormsCookiePath);

                //// Encrypt the ticket.
                //string encTicket = FormsAuthentication.Encrypt(ticket);

                //// Create the cookie.
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //所以要取得登入時設定的 userData 的方式，可以參考以下程式片段
                //FormsIdentity id = (FormsIdentity)User.Identity;
                //FormsAuthenticationTicket ticket = id.Ticket;

                //cookiePath.Text = ticket.CookiePath;
                //expireDate.Text = ticket.Expiration.ToString();
                //expired.Text = ticket.Expired.ToString();
                //isPersistent.Text = ticket.IsPersistent.ToString();
                //issueDate.Text = ticket.IssueDate.ToString();
                //name.Text = ticket.Name;
                //userData.Text = ticket.UserData;
                //version.Text = ticket.Version.ToString();

                /////////////////////////////
                FormsAuthentication.RedirectFromLoginPage(login.Username,false);
                TempData["LoginInfo"] = login;

                if (ReturnUrl !=null && ReturnUrl.StartsWith("/"))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
                    return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}