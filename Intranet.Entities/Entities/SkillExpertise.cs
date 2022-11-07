using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class SkillExpertise
    {
        public int SkillId { get; set; }
        public int ExpertiseId { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Expertise Expertise { get; set; }
        public string? Details { get; set; } = string.Empty;
        public int Value { get; set; } = 0;

    }
}
