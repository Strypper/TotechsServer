using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class UserCertification
    {
        public string UserId { get; set; }
        public int CertificationId { get; set; }
        public string CertificationUniqueId { get; set; }
        public string VerificationUrl { get; set; }
        public DateTime IssueDate { get; set; }
        public virtual User User { get; set; }
        public virtual Certification Certification { get; set; }
         
    }
}
