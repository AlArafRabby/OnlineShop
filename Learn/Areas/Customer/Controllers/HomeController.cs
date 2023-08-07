using Learn.Data;
using Learn.DTO;
using Learn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Learn.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[Route("customer/[controller]/[action]")]
    //[Authorize(Roles ="Customer")]
    public class HomeController : Controller
    {
        private readonly OnlineShopJoyContext _db;


        public HomeController(OnlineShopJoyContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? page)
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        //GET product detail acation method

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }

            var p = _db.Products.Include(p => p.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(defaultValue => defaultValue.Id == id);



            if (p == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.GetObjectFromJson<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(p);
            HttpContext.Session.SetObjectAsJson("products", products);
            return RedirectToAction(nameof(Index));
            //return View(products);

            //var p = _db.Products.Include(p => p.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(defaultValue => defaultValue.Id == id);
            //products.Add(p);
            //HttpContext.Session.SetObjectAsJson("products", products);
            //return RedirectToAction(nameof(Index));
        }
        //GET Remove action methdo
        [HttpGet]
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            //List<Products> products = HttpContext.Session.GetObjectFromJson<List<Products>>("products");
            //if (products != null)
            //{
            //    var product = products.FirstOrDefault(c => c.Id == id);
            //    if (product != null)
            //    {
            //        products.Remove(product);
            //        HttpContext.Session.SetObjectAsJson("products", product);
            //    }
            //}
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.GetObjectFromJson<List<Products>>("products");
            
            if (products != null)
            {
                var p = products.FirstOrDefault(c => c.Id == id);
                if (p != null)
                {
                    products.Remove(p);
                    HttpContext.Session.SetObjectAsJson("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET product Cart action method

        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.GetObjectFromJson<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }

    }

}
