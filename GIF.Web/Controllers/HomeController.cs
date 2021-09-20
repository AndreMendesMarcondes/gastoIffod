using GIF.Domain;
using GIF.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GIF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIFoodAPI _ifoodAPI;

        public HomeController(ILogger<HomeController> logger, IIFoodAPI ifoodAPI)
        {
            _logger = logger;
            _ifoodAPI = ifoodAPI;
        }

        public IActionResult Index()
        {
            ViewData["ShowNoOrders"] = false;
            return View(new IFoodTotalOrderDTO());
        }

        [HttpPost]
        public IActionResult Index(string bearerToken)
        {
            IFoodTotalOrderDTO totalOrders = new();

            if (!String.IsNullOrEmpty(bearerToken))
                totalOrders = _ifoodAPI.GetOrders(bearerToken);

            totalOrders.Bearer = bearerToken;

            ViewData["ShowNoOrders"] = totalOrders.OrderCount == 0;
            return View(totalOrders);
        }
    }
}
