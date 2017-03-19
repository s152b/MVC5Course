using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;   //不要using錯了

namespace MVC5Course.ActionFilterAttributes
{
    public class 紀錄Action執行時間 : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewBag.StartTime = DateTime.Now;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            filterContext.Controller.ViewBag.EndTime = DateTime.Now;
            filterContext.Controller.ViewBag.Duration = filterContext.Controller.ViewBag.EndTime - filterContext.Controller.ViewBag.StartTime;
        }
    }
}