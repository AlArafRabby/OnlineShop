using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[Route("customer/[controller]/[action]")]
    //[Authorize(Roles ="Customer")]
    public class HomeController : Controller
    {
        //[Route("index")]
        public IActionResult Index()
        {
            ViewData["index"] = "Customer".ToString();
            ViewData["routeInfo"] = ControllerContext.RouteData.ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["index"] = "Customer".ToString();
            ViewData["routeInfo"] = ControllerContext.RouteData.ToString();
            return View();
        }

        public IActionResult Error()
        {
            ViewData["index"] = "Customer".ToString();
            ViewData["routeInfo"] = ControllerContext.RouteData.ToString();
            return View();
        }
    }
}
