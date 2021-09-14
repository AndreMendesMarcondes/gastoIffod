using Domain;
using GIF.Domain;
using GIF.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GIF.Service.Imp
{
    public class IFoodBusiness : IIFoodBusiness
    {
        private static string LAST_STATUS = "CONCLUDED";
        public IFoodTotalOrderDTO FilterOrders(List<IFoodOrderDTO> orders)
        {
            IFoodTotalOrderDTO totalOrderDTO = new();
            List<IFoodOrderFilteredDTO> ifoodOrdersFiltered = new();
            orders = orders.Where(c => c.lastStatus == LAST_STATUS).ToList();

            foreach (var order in orders)
            {
                IFoodOrderFilteredDTO filteredOrder = FilterOrder(order);
                ifoodOrdersFiltered.Add(filteredOrder);

                totalOrderDTO.OrderCount++;
                totalOrderDTO.Price += filteredOrder.TotalPrice;
            }

            totalOrderDTO.IFoodTotalByMonthOrders = GroupOrdersByMonth(ifoodOrdersFiltered);

            return totalOrderDTO;
        }

        private static IFoodOrderFilteredDTO FilterOrder(IFoodOrderDTO order)
        {
            return new IFoodOrderFilteredDTO
            {
                TotalPrice = order.bag.total.value,
                CreatedAt = order.createdAt.Date,
                PlaceName = order.merchant.name
            };
        }

        private static List<IFoodTotalByMonthOrderDTO> GroupOrdersByMonth(List<IFoodOrderFilteredDTO> filteredOrders)
        {
            List<IFoodTotalByMonthOrderDTO> foodTotalByMonthOrders = new();
            var months = filteredOrders.OrderBy(c => c.CreatedAt)
                                                       .Select(c => c.CreatedAt.ToString("MM/yy"))
                                                       .Distinct();

            foreach (var month in months)
            {
                IFoodTotalByMonthOrderDTO foodTotalByMonthOrder = new();
                List<IFoodOrderFilteredDTO> ordersByMonth = filteredOrders.Where(c => c.CreatedAt.ToString("MM/yy") == month)
                                                                          .ToList();

                foodTotalByMonthOrder.Month = month;
                foodTotalByMonthOrder.OrderCount = ordersByMonth.Count();
                foodTotalByMonthOrder.Price = ordersByMonth.Sum(c => c.TotalPrice);

                foodTotalByMonthOrders.Add(foodTotalByMonthOrder);
            }

            return foodTotalByMonthOrders;
        }
    }
}
