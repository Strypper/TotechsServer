using System;

namespace Intranet.Entities.Entities
{
    public class Project : BaseEntity
    {
        public string   ProjectName { get; set; }
        public string?  Clients     { get; set; } = string.Empty;
        public string?  About       { get; set; } = string.Empty;
        public string?  GithubLink  { get; set; } = string.Empty;
        public string?  FigmaLink   { get; set; } = string.Empty;

        public DateTime  StartTime    { get; set; }
        public DateTime? Deadline     { get; set; }
        public int       TechLead     { get; set; }

    }
}
