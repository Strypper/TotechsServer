using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class UserSkill
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public string? Confirmation { get; set; }
        public virtual User User { get; set; }
        public virtual Skill Skill { get; set; }
        public Level Level { get; set; } = Level.FRESHER;
    }

    public enum Level { FRESHER, JUNIOR, MID, SENIOR, EXPERT}
}
