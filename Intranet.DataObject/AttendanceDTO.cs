using System;

namespace Intranet.DataObject;

public class AttendanceDTO : BaseDTO<int>
{
    public DateTime Attend { get; set; }
    public DateTime Leave { get; set; }
    public UserDTO AttendanceInfo { get; set; }
    public decimal ContributeAmount { get; set; }
    public bool IsApproved { get; set; }
}