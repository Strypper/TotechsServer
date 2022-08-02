using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Food : BaseEntity<int>
    {
        public string FoodName { get; set; }
        public string FoodEnglishName { get; set; }
        public int MainIcon { get; set; }
        public int? SecondaryIcon { get; set; }
        public bool IsUnavailable { get; set; }
        public decimal Percentage { get; set; }
    }
}
