using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int CourseId { get; set; }

    public int SlotIndex { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<StudentSchedule> StudentSchedules { get; set; } = new List<StudentSchedule>();
}
