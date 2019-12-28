using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTOs;
using slnCodeFirstAPIProduct.Models;

namespace slnCodeFirstAPIProduct.Controllers
{
    public class ProductController : ApiController
    {
        // GET: api/Product
        public IHttpActionResult  GetAllProducts()
        {
            IList<ProductDTO> products = null;
            using (var ctx = new ProductDB())
            {
                products = ctx.Products.Select(p => new ProductDTO()
                {
                    ID = p.ID,
                    NameProduct = p.NameProduct,
                    Price = p.Price            

                }).ToList<ProductDTO>();
            }
            if(products.Count==0)

            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/Product/5
        public IHttpActionResult GetProductById(int id)
        {
            ProductDTO productDTO = null;
            using (var ctx = new ProductDB())
            {
                productDTO = ctx.Products.Where(p => p.ID == id)
                    .Select(p => new ProductDTO()
                    {
                        ID=p.ID,
                        NameProduct=p.NameProduct,
                        Price=p.Price

                    }).FirstOrDefault<ProductDTO>();
            }
            if (productDTO == null)
            {
                return NotFound();
            }
            return Ok(productDTO);
        }

        //POST: api/Product
        private Product GetNewProduct(ProductDTO p)
        {
           
                Product product = new Product()
                {
                    NameProduct = p.NameProduct,
                    Price = p.Price,
               };
                return product;
           
        }
        public IHttpActionResult PostNewProduct(ProductDTO productDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            using (var ctx = new ProductDB())
            {
                Product product = GetNewProduct(productDTO);
                if (product != null)
                {
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                    return BadRequest("Invalid data");
            }
        }


        private bool UpdateProduct(Product p, ProductDTO pDTO)
        {
            
                p.NameProduct = pDTO.NameProduct;
                p.Price = pDTO.Price;
               
                return true;
           
        }
        //// PUT: api/Product/5
        public IHttpActionResult PutProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            using (var ctx = new ProductDB())
            {
                var existingPro = ctx.Products.Where(p => p.ID == product.ID).FirstOrDefault<Product>();
                if(existingPro!=null)
                {
                    if(UpdateProduct(existingPro,product))
                    {
                        ctx.SaveChanges();
                        return Ok();

                    }
                    return BadRequest("Not a valid model");
                }
                return NotFound();
            }
        }

        //// DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest("Not a valid id");
            using (var ctx = new ProductDB())
            {
                var pro = ctx.Products.Where(p => p.ID == id).FirstOrDefault();
                ctx.Entry(pro).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();

            }
            return Ok();
        }
    }
}
