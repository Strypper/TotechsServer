using System;
using System.Collections.Generic;

namespace Intranet.DataObject;

public class QADTO : BaseDTO<int>
{
    public string Title { get; set; } = string.Empty;
    public string? Detail { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<string> Photos { get; set; } = new();
}
