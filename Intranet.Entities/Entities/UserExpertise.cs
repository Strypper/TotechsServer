using System;
using System.ComponentModel.DataAnnotations;

namespace Intranet;

public class UserExpertise
{
    public int ExpertiseId { get; set; }
    public string UserId { get; set; }

    [Range(0, 10, ErrorMessage = "Min exp: 0, max exp: 10")]
    public byte Exp { get; set; } = 0;
}
