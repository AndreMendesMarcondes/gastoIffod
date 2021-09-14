using GIF.Domain;

namespace GIF.Service.Interface
{
    public interface IIFoodAPI
    {
        IFoodTotalOrderDTO GetOrders(string bearerToken);
    }
}
