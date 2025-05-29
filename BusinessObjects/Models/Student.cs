using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudentSchedule> StudentSchedules { get; set; } = new List<StudentSchedule>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
