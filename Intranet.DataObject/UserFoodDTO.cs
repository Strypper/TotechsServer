﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserFoodDTO : BaseDTO
    {
        public UserDTO User { get; set; }
        public FoodDTO Food { get; set; }
    }
}
