using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Intranet.DataObject;

public class UserDTO
{
    public string Guid { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string? Bio { get; set; }
    public string? Former { get; set; }
    public string? Hobby { get; set; }
    public string? CardPic { get; set; }
    public string? ProfilePic { get; set; }
    public bool IsDisable { get; set; }
    public string? SpecialAward { get; set; }
    public int? Like { get; set; }
    public int? Friendly { get; set; }
    public int? Funny { get; set; }
    public int? Enthusiastic { get; set; }
    public string? Relationship { get; set; }
    public string? SignalRConnectionId { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<SkillDTO>? Skills { get; set; } = Array.Empty<SkillDTO>();
}

public record UserLoginDTO(string username,
                           string password)
{ }

public record UserSignUpDTO(string username,
                            string password,
                            string firstname,
                            string lastname,
                            string email,
                            string phonenumber,
                            string[]? roles,
                            IFormFile? avatarfile)
{ }
