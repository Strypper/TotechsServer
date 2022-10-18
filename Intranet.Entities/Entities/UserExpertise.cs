using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class UserExpertise
    {
        public int ExpertiseId { get; set; }
        public string UserId { get; set; }

        [Range(0, 10, ErrorMessage = "Min exp: 0, max exp: 10")]
        public byte Exp { get; set; } = 0;

        public virtual User User { get; set; }
        public virtual Expertise Expertise { get; set; }
    }
}
