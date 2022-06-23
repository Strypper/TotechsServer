using Intranet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class ProjectTaskDTO : BaseDTO
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.MinValue;

        public DateTime? Deadline { get; set; } = DateTime.MaxValue;

        public Priority Priority { get; set; }
    }
}
