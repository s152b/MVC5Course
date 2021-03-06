﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using PagedList.Mvc;
using System.Net.Http.Formatting;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    //[Authorize] 已經加在BaseController
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        //改成由Repository處理資料
        //repo.UnitOfWork 跟連線有關 
        //確認異動資料 repo.UnitOfWork.Commit();
        //var db = repo.UnitOfWork.Context; 取得原本的FabricsEntities
        //RepositoryHelper.GetProductRepository()取得Product實體
        //ProductRepository repoProsd = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(string sortBy, string keyword, int pageNo = 1)
        {
            var data = repoProds.All().AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }
            if (sortBy == "+Price")
            {
                data = data.OrderBy(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.Price);
            }
            ViewBag.keyword = keyword;
            ViewBag.pageNo = pageNo;
            return View(data.ToPagedList(pageNo, 10));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProds.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult ProdOrderLine(int id)
        {
            Product product = repoProds.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.OrderLine);
        }
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                repoProds.Add(product);
                //db.SaveChanges();
                repoProds.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProds.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View = "Error_DbEntityValidationException", ExceptionType = typeof(DbEntityValidationException))]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {

            //if (ModelState.IsValid)
            //{
            //    var db = repoProds.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    repoProds.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}
            //return View(product);

            //改TryUpdateModel
            var prod = repoProds.Find(id);
            if (TryUpdateModel(prod, new string[] { "ProductName", "Price" }))
            {
            }

            repoProds.UnitOfWork.Commit();
            return RedirectToAction("Index");
            // return View(prod);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProds.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            Product product = repoProds.Find(id);
            //db.Product.Remove(product);
            repoProds.Delete(product);
            //db.SaveChanges();
            repoProds.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Index2(string sortBy, string keyword, int pageNo = 1)
        {
            FindAllProd(sortBy, keyword, pageNo);

            return View();
        }

        private void FindAllProd(string sortBy, string keyword, int pageNo)
        {
            var data = repoProds.All().AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }
            if (sortBy == "+Price")
            {
                data = data.OrderBy(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.Price);
            }
            ViewBag.keyword = keyword;
            ViewBag.pageNo = pageNo;
            ViewData.Model = data.ToPagedList(pageNo, 10);
        }

        [HttpPost]
        public ActionResult Index2(IList<Product> data, string sortBy, string keyword, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var prod = repoProds.Find(item.ProductId);
                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;

                }
                repoProds.UnitOfWork.Commit();
                //return RedirectToAction("Index2");
                FindAllProd(sortBy, keyword, pageNo);
                return View();
            }
            FindAllProd(sortBy, keyword, pageNo);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
