using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class StudentSchedule
{
    public int StudentId { get; set; }

    public int ScheduleId { get; set; }

    public bool IsPresent { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
