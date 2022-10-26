using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class UserCertification : BaseEntity<int>
    {
        public string CertificationUniqueId { get; set; }
        public string VerificationUrl { get; set; }
        public DateTime IssueDate { get; set; }
        public string UserId { get; set; }
        public int CerificationId { get; set; }
        public User User { get; set; }
        public Certification Certification { get; set; }
         
    }
}
