using GIF.Domain;
using GIF.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GIF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IFoodCallerController : ControllerBase
    {
        private readonly IIFoodAPI _ifoodAPI;

        public IFoodCallerController(IIFoodAPI ifoodAPI)
        {
            _ifoodAPI = ifoodAPI;
        }

        [HttpGet]
        public IActionResult Get(string bearerToken)
        {
            IFoodTotalOrderDTO totalOrders = new();

            if (!String.IsNullOrEmpty(bearerToken))
            {
                bearerToken = bearerToken.Replace("Bearer ", "");
                totalOrders = _ifoodAPI.GetOrders(bearerToken);
            }

            totalOrders.Bearer = bearerToken;
            return Ok(totalOrders);
        }
    }
}
