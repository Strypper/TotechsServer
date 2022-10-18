using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Skill : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double SkillValue { get; set; } = 0;
        public string? MoreDetail { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; } = new HashSet<UserSkill>();
        public ICollection<SkillExpertise> SkillExpertises { get; set; } = new HashSet<SkillExpertise>();

    }
}
