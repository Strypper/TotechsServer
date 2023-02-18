using System;

namespace Intranet.Entities;

public class Attendance : BaseEntity<int>
{
    public DateTime Attend { get; set; }
    public DateTime Leave { get; set; }
    public User AttendanceInfo { get; set; } = default!;
    public decimal ContributeAmount { get; set; }
    public bool IsApproved { get; set; }
}