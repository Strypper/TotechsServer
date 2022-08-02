using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class SkillDTO : BaseDTO<int>
    {
        public string Name { get; set; }
        public double SkillValue { get; set; }
    }
}
