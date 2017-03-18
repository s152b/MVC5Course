using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    //在BaseController 的 class 上加入[Authorize] ，
    //未來只要繼承在BaseController就會一體適用
    [Authorize]
    public abstract class BaseController : Controller
    {
        //class 加入 abstract 抽象化 可以防止在網頁上執行Base這隻Controller

        public ProductRepository repoProds = RepositoryHelper.GetProductRepository();

        protected override void HandleUnknownAction(string actionName)
        {
            //找不到action就往這邊跑
            base.HandleUnknownAction(actionName);
        }
    }
}