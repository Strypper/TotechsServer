using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class SubFeatureDTO : BaseDTO<int>
    {
        public string? Icon { get; set; } = string.Empty;
    }
}
