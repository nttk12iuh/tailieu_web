using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace slnCodeFirstAPIProduct.Models
{
    public class DataInitialier:
        System.Data.Entity.CreateDatabaseIfNotExists<ProductDB>
    {
        protected override void Seed(ProductDB context)

        {
            context.SaveChanges();

            context.Products.Add(new Product { NameProduct = "Pepsi", Price = 5000 });
            context.Products.Add(new Product { NameProduct = "Sprite", Price = 51000 });
            context.Products.Add(new Product { NameProduct = "Cocacola", Price = 35000 });
            context.Products.Add(new Product { NameProduct = "7up", Price = 52000 });
            context.Products.Add(new Product { NameProduct = "Fanta", Price = 45000 });
            context.Products.Add(new Product { NameProduct = "Twister", Price = 25000 });
            context.Products.Add(new Product { NameProduct = "Fanta", Price = 15000 });

            base.Seed(context);
        }
       
         
    }
}