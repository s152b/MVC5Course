using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View1()
        {
            return View("Index");
        }
        public ActionResult File1()
        {
            return File(Server.MapPath("~/Content/A.jpg"),"image/jpeg");
        }

        public ActionResult File2()
        {
            //直接下載
            return File(Server.MapPath("~/Content/A.jpg"), "image/jpeg", "檔案.jpg");
        }
    }
}