using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTOs;
using System.Net.Http;


namespace prjProductMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductDTO> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55317/api/");
                var responseTask = client.GetAsync("product");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDTO>>();
                    readTask.Wait();
                    products = readTask.Result;
                }
                else
                {
                    products = Enumerable.Empty<ProductDTO>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            
            return View(products);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductDTO product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55317/api/");
                var postTask = client.PostAsJsonAsync<ProductDTO>("product", product);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error");
               
            }

                return View(product);
        }

        public ActionResult Edit(int id)
        {
            ProductDTO pro = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55317/api/");
                var responseTask = client.GetAsync("product?id="+id.ToString());
                responseTask.Wait();
                var proResult = responseTask.Result;
                if (proResult.IsSuccessStatusCode)
                {
                    var readTask = proResult.Content.ReadAsAsync<ProductDTO>();
                    readTask.Wait();
                    pro = readTask.Result;
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error");


            }
             return View(pro);
        }

        [HttpPost]
        public ActionResult Edit(ProductDTO pro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55317/api/");
                //htttpost
                var putTask = client.PutAsJsonAsync<ProductDTO>("product", pro);
                putTask.Wait();
                var result = putTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
                return View(pro);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55317/api/");
                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}