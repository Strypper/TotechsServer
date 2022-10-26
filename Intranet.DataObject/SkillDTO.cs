using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class SkillDTO : BaseDTO<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MoreDetail { get; set; } = string.Empty;
        public double SkillValue { get; set; } = 0;
        public ICollection<SkillExpertiseDTO> SkillExpertises { get; set; } = Array.Empty<SkillExpertiseDTO>();
    }
}
