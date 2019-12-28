using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.Entity;
using slnCodeFirstAPIProduct.Models;

namespace slnCodeFirstAPIProduct.Models
{
    public class ProductDB:DbContext
    {
        public ProductDB() : base("name=ProductDB")
        {
        }
        public System.Data.Entity.DbSet<slnCodeFirstAPIProduct.Models.Product> Products { get; set; }
    }
}