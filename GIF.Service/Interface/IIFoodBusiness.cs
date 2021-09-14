using Domain;
using GIF.Domain;
using System.Collections.Generic;

namespace GIF.Service.Interface
{
    public interface IIFoodBusiness
    {
        IFoodTotalOrderDTO FilterOrders(List<IFoodOrderDTO> orders);
    }
}
