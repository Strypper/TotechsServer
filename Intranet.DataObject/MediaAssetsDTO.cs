﻿using Intranet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class MediaAssetsDTO : BaseDTO
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public MediaAssestType MediaAssestType { get; set; }
    }

    public class CreateMediaAssetsDTO
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public MediaAssestType MediaAssestType { get; set; }
    }
}
