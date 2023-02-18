namespace Intranet.DataObject;

public class UserFoodDTO : BaseDTO<int>
{
    public UserDTO User { get; set; } = default!;
    public FoodDTO Food { get; set; } = default!;
}

public class CreateUpdateUserFoodDTO
{
    public string UserId { get; set; } = default!;
    public int FoodId { get; set; }
}
