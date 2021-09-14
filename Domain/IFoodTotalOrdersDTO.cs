using System.Collections.Generic;

namespace GIF.Domain
{
    public class IFoodTotalOrderDTO
    {
        public string Bearer { get; set; }
        public int OrderCount { get; set; }
        public long Price { get; set; }
        public List<IFoodTotalByMonthOrderDTO> IFoodTotalByMonthOrders { get; set; }
    }
}
