using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Intranet.Entities;
public class User : IdentityUser
{
    public string? ProfilePic { get; set; } = String.Empty;
    public string? CardPic { get; set; } = String.Empty;
    public string? Bio { get; set; } = String.Empty;
    public string? Former { get; set; }
    public string? Hobby { get; set; }
    public string SpecialAward { get; set; } = "No Award Yet";
    public bool IsDisable { get; set; }
    public string? Skills { get; set; }
    public string? Relationship { get; set; }
    public string? SignalRConnectionId { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<ChatMessage> ChatMessages { get; set; }

}

