using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserFoodDTO : BaseDTO
    {
        public UserDTO User { get; set; }
        public FoodDTO Food { get; set; }
    }

    public class CreateUpdateUserFoodDTO
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
    }
}
