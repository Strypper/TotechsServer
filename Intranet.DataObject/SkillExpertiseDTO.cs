using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class SkillExpertiseDTO
    {
        public int SkillId { get; set; }
        public int ExpertiseId { get; set; }
        public string Details { get; set; } = string.Empty;
        public int Value { get; set; } = 0;

    }
}
