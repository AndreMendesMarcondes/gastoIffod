using System;

namespace GIF.Domain
{
    public class IFoodOrderFilteredDTO
    {
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PlaceName { get; set; }
    }
}
