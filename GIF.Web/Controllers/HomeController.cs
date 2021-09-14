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
            return View();
        }

        [HttpPost]
        public IActionResult Index(string bearerToken)
        {
            if (!String.IsNullOrEmpty(bearerToken))
                _ifoodAPI.GetOrders(bearerToken);

            return View(bearerToken);
        }
    }
}
