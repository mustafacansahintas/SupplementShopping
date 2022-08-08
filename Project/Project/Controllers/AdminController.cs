using BLL;
using Entities.Concrete;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Project.Controllers
{
    public class AdminController : Controller
    {
       
        private ProductManager pm=new ProductManager();
      
        public ActionResult Index()
        {
            return View(pm.List());
        }


        // GET: Admin
        public ActionResult ProductCreate()
        {
           
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id","CategoryName");
            ViewBag.BrandId = new SelectList(CacheHelper.GetBrandsFromCache(), "Id", "BrandName");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Product product)
        {
            if (ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "CategoryName", product.CategoryId);
                ViewBag.BrandId = new SelectList(CacheHelper.GetBrandsFromCache(), "Id", "BrandName", product.BrandId);
                pm.Insert(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "CategoryName", product.CategoryId);
            ViewBag.BrandId = new SelectList(CacheHelper.GetBrandsFromCache(), "Id", "BrandName", product.BrandId);

            return View(product);
            

            
        }
        
        public ActionResult ProductUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            Product product=pm.Find(x=> x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "CategoryName");
            ViewBag.BrandId = new SelectList(CacheHelper.GetBrandsFromCache(), "Id", "BrandName");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductUpdate(Product product)
        {
            if (ModelState.IsValid)
            {
                

                Product pro=pm.Find(x=>x.Id == product.Id);
                pro.ProductName = product.ProductName;
                pro.Description = product.Description;
                pro.UnitPrice=product.UnitPrice;
                pro.Stock = product.Stock;
                pro.ImageAdress=product.ImageAdress;
                pro.ImageDetailAdress=product.ImageDetailAdress;
                pro.Discounted=product.Discounted;
                pro.BrandId=product.BrandId;
                pro.CategoryId=product.CategoryId;

                pm.Update(pro);
              
                return RedirectToAction("Index");
                
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "CategoryName");
            ViewBag.BrandId = new SelectList(CacheHelper.GetBrandsFromCache(), "Id", "BrandName");

            return View(product);
        }


        public ActionResult ProductDelete(int id)
        {
            Product product=pm.Find(x=>x.Id == id);
            pm.Delete(product);
            CacheHelper.RemoveCategoriesFromCache();
            CacheHelper.RemoveBrandsFromCache();

            return RedirectToAction("Index");

        }

 

    }
}