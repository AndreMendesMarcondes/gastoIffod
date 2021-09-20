using GIF.Domain;
using GIF.Service.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(string bearerToken)
        {
            return View(await WrapperAPICall(bearerToken));
        }

        private static async Task<IFoodTotalOrderDTO> WrapperAPICall(string bearerToken)
        {
            IFoodTotalOrderDTO ifoodOrders = new();
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API"));
            HttpResponseMessage response = await httpClient.GetAsync($"/api/IFoodCaller?bearerToken={bearerToken}");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                ifoodOrders = JsonConvert.DeserializeObject<IFoodTotalOrderDTO>(result);
            }

            return ifoodOrders;
        }
    }
}
