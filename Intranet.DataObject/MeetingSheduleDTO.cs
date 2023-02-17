using System;
using System.Collections.Generic;

namespace Intranet.DataObject;

public class MeetingScheduleDTO : BaseDTO<int>
{
    public DateTime MeetingDate { get; set; }
    public DateTime EndTime { get; set; }
    public UserDTO? Planner { get; set; }
    public MeetingInfoDTO? MeetingInfo { get; set; }
    public ICollection<TodoTaskDTO> MustDoneTask { get; set; } = new HashSet<TodoTaskDTO>();
    public ICollection<AttendanceDTO> Attendances { get; set; } = new HashSet<AttendanceDTO>();
}