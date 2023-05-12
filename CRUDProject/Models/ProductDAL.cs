using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CRUDProject.Models
{
    public class ProductDAL
    {
        ASPDBDataContext dc;
        public ProductDAL()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["ASPDBConnectionString"].ConnectionString;
            dc = new ASPDBDataContext();
        }
        public List<Product> GetProducts(bool? Status)
        {
            List<Product> products;
            if (Status == null)
            {
                products = dc.Products.ToList();
            }
            else
            {
                products = (from p in dc.Products where p.Status == Status select p).ToList();
            }
            return products;
        }
        public Product GetProduct(int ProductId, bool? Status)
        {
            Product product;

            if (Status == null)
            {
                product = (from p in dc.Products where p.ProductId == ProductId select p).Single();
            }
            else
            {
                product = (from p in dc.Products where p.ProductId == ProductId && p.Status == Status select p).Single();
               
            }
            return product;
        }
        public void AddProduct(Product product)
        {
            dc.Products.InsertOnSubmit(product);
            dc.SubmitChanges();
        }
        public void UpdateProduct(Product newValues)
        {
            Product oldValues = dc.Products.Single(P => P.ProductId == newValues.ProductId);
            oldValues.ProductName = newValues.ProductName;
            oldValues.CategoryId = newValues.CategoryId;
            oldValues.CategoryName = newValues.CategoryName;
            dc.SubmitChanges();
        }


        public void DeleteProduct(int ProductId)
        {
            Product oldValues = dc.Products.First(P => P.ProductId == ProductId);
            dc.Products.DeleteOnSubmit(oldValues); //Permenant Deletion 
            //oldValues.Status = false; //Updates the status in Database with-out deleting the record
            dc.SubmitChanges();

        }
    }
}