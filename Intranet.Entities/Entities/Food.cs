using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Food : BaseEntity
    {
        public string FoodName { get; set; }
        public string FoodEnglishName { get; set; }
        public int MainIcon { get; set; }
        public int? SecondaryIcon { get; set; }
        public decimal Percentage { get; set; }
    }
}
