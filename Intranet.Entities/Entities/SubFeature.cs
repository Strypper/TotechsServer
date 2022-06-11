using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class SubFeature : BaseEntity
    {
        public string? Icon { get; set; } = string.Empty;

        public virtual ICollection<FeatureTask> Tasks { get; set; } = new HashSet<FeatureTask>();
    }
}
