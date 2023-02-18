using System;
using System.Collections.Generic;

namespace Intranet.Entities;

public class MeetingSchedule : BaseEntity<int>
{
    public DateTime MeetingDate { get; set; }
    public DateTime EndTime { get; set; }
    public User Planner { get; set; } = default!;
    public MeetingInfo MeetingInfo { get; set; } = default!;
    public ICollection<TodoTask> MustDoneTask { get; set; } = new HashSet<TodoTask>();
    public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
}