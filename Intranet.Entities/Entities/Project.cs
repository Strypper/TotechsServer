using System;

namespace Intranet.Entities;

public class Project : BaseEntity<int>
{
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectLogo { get; set; } = string.Empty;
    public string? ProjectBackground { get; set; } = string.Empty;
    public string? Clients { get; set; } = string.Empty;
    public string? About { get; set; } = string.Empty;
    public string? GithubLink { get; set; } = string.Empty;
    public string? FigmaLink { get; set; } = string.Empty;
    public string? MicrosoftStoreLink { get; set; } = string.Empty;
    public string? GooglePlayLink { get; set; } = string.Empty;
    public string? AppStoreLink { get; set; } = string.Empty;


    public DateTime StartTime { get; set; }
    public DateTime? Deadline { get; set; }
    public int TechLead { get; set; }

}
