using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Intranet.Entities;
public class User : IdentityUser
{
    public string? ProfilePic { get; set; } = "https://i.imgur.com/deS4147.png";
    public string? CardPic { get; set; }
    public string? Bio { get; set; }
    public string? Former { get; set; }
    public string? Hobby { get; set; }
    public string SpecialAward { get; set; } = "No Award Yet";
    public bool IsDisable { get; set; }
    public string? Skills { get; set; }
    public string? Relationship { get; set; }
    public string? SignalRConnectionId { get; set; }
    public bool IsDeleted { get; set; }



    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();

}

