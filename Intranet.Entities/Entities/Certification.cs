using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class Certification : BaseEntity<int>
    {
        public string   Title           { get; set; }
        public string?  Description     { get; set; }
        public string?  Issuer          { get; set; }
        public string?  ImageURL        { get; set; }
        public ICollection<UserCertification> UserCertifications { get; set; } = new HashSet<UserCertification>(); 
    }
}
