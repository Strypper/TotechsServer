using System;
using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class FoodDTO : BaseDTO<int>
    {
        public string FoodName { get; set; }
        public string FoodEnglishName { get; set; }
        public int MainIcon { get; set; }
        public int? SecondaryIcon { get; set; }
        public bool IsUnavailable { get; set; }
        public decimal Percentage { get; set; }
    }
}
