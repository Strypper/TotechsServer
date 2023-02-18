namespace Intranet.DataObject;

public class FoodDTO : BaseDTO<int>
{
    public string FoodName { get; set; } = default!;
    public string FoodEnglishName { get; set; } = default!;
    public int MainIcon { get; set; }
    public int? SecondaryIcon { get; set; }
    public bool IsUnavailable { get; set; }
    public decimal Percentage { get; set; }
}
