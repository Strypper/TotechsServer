using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class Expertise : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? LogoURL { get; set; }
        public string? MoreDetails { get; set; }

        public ICollection<UserExpertise> UserExpertises { get; set; } = new HashSet<UserExpertise>();
        public ICollection<SkillExpertise> SkillExpertises { get; set; } = new HashSet<SkillExpertise>();
    }
}
