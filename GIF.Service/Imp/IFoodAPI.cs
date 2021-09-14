using Domain;
using GIF.Domain;
using GIF.Service.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using Method = RestSharp.Method;

namespace GIF.Service.Imp
{
    public class IFoodAPI : IIFoodAPI
    {
        private readonly RestClient _client;
        private readonly IIFoodBusiness _business;

        public IFoodAPI(IConfiguration configuration, IIFoodBusiness business)
        {
            _client = new RestClient(configuration["IFoodAPIBaseAddress"]);
            _business = business;
        }

        public IFoodTotalOrderDTO GetOrders(string bearerToken)
        {
            bool hasMoreOrders = true;
            int pageNumber = 0;
            List<IFoodOrderDTO> ordersList = new();

            while (hasMoreOrders)
            {
                hasMoreOrders = GetNextOrders(bearerToken, pageNumber, ordersList);
                pageNumber++;
            }

            IFoodTotalOrderDTO totalOrders = _business.FilterOrders(ordersList);
            return totalOrders;
        }

        private bool GetNextOrders(string bearerToken, int pageNumber, List<IFoodOrderDTO> ordersList)
        {
            _client.Timeout = -1;
            List<IFoodOrderDTO> currentOrderList = new();
            var request = new RestRequest($"/v4/customers/me/orders?page={pageNumber}&size=10", Method.GET);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            IRestResponse response = _client.Execute(request);
            pageNumber++;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                currentOrderList = JsonConvert.DeserializeObject<List<IFoodOrderDTO>>(response.Content);
                ordersList.AddRange(currentOrderList);
            }

            return HasMoreOrders(currentOrderList);
        }

        private static bool HasMoreOrders(List<IFoodOrderDTO> currentOrderList)
        {
            if (currentOrderList.Count == 10)
                return true;

            return false;
        }
    }
}
