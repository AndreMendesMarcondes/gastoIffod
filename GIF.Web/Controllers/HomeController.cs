using GIF.Domain;
using GIF.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GIF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IIFoodAPI _ifoodAPI;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IIFoodAPI ifoodAPI)
        {
            _logger = logger;
            _configuration = configuration;
            _ifoodAPI = ifoodAPI;
        }

        public IActionResult Index()
        {
            ViewData["ShowNoOrders"] = false;
            return View(new IFoodTotalOrderDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string bearerToken)
        {
            return View(await WrapperAPICall(bearerToken));
        }

        private async Task<IFoodTotalOrderDTO> WrapperAPICall(string bearerToken)
        {
            IFoodTotalOrderDTO totalOrders = new();

            if (!String.IsNullOrEmpty(bearerToken))
            {
                bearerToken = bearerToken.Replace("Bearer ", "");
                totalOrders = _ifoodAPI.GetOrders(bearerToken);
            }

            ViewData["ShowNoOrders"] = totalOrders.OrderCount == 0;

            return totalOrders;
        }
    }
}
