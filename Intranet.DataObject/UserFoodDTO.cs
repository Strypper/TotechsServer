namespace Intranet.DataObject;

public class UserFoodDTO : BaseDTO<int>
{
    public UserDTO User { get; set; }
    public FoodDTO Food { get; set; }
}

public class CreateUpdateUserFoodDTO
{
    public string UserId { get; set; }
    public int FoodId { get; set; }
}
