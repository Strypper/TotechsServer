using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class FeatureTask : BaseEntity
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.MinValue;

        public User Author { get; set; }

        public DateTime? Deadline { get; set; } = DateTime.MaxValue;

        public Priority Priority { get; set; }

        public virtual SubFeature? SubFeature { get; set; } = null;

        public int? SubFeatureId { get; set; } = null;
    }

    public enum FeatureTaskStatus { }

    public enum TaskPriority { }
}
