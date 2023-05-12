using CRUDProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDProject.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL obj = new ProductDAL();

        public ViewResult DisplayProducts()
        {
            return View(obj.GetProducts(null));
        }
        public ViewResult DisplayProduct(int ProductId)
        {
            return View(obj.GetProduct(ProductId, true));
        }
        [HttpGet]
        public ViewResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddProduct(Product product)
        {
            product.Status = true;
            obj.AddProduct(product);
            return RedirectToAction("DisplayProducts");
        }
        public ViewResult EditProduct(int ProductId)
        {

            Product product1 = obj.GetProduct(ProductId, true);
            return View(product1);
        }
        public RedirectToRouteResult UpdateProduct(Product product)
        {
            obj.UpdateProduct(product);
            return RedirectToAction("DisplayProducts");
        }
        public RedirectToRouteResult DeleteProduct(int ProductId)
        {

            obj.DeleteProduct(ProductId);
            return RedirectToAction("DisplayProducts");
        }
    }
}