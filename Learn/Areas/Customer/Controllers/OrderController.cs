using Learn.Data;
using Learn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learn.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly OnlineShopJoyContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _he;
        public OrderController(OnlineShopJoyContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment he)
        {
            _context = context;
            _he = he;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Order o)
        {
           
                List<Products> products = HttpContext.Session.GetObjectFromJson<List<Products>>("products");


                if(products!=null)
                {
                    foreach (var product in products)
                    {
                        OrderDetails orderDetails=new OrderDetails();
                        orderDetails.PorductId = product.Id;
                        o.OrderDetails.Add(orderDetails);
                    }
                }

                o.OrderNo=GetOrderNumber();
                _context.Orders.Add(o);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetObjectAsJson("products",null);

            
            return View();
           
        }


        public string GetOrderNumber()
        {
            int row = _context.Orders.ToList().Count() + 1;
            return row.ToString("000");
        }
    }
}
