using System;

namespace Intranet.Entities;

public class QA : BaseEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}