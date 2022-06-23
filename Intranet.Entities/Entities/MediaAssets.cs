using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class MediaAssets : BaseEntity
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public MediaAssestType MediaAssestType { get; set; }
    }

    public enum MediaAssestType { }
}
