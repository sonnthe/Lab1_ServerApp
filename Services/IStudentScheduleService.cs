using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStudentScheduleService
    {
        bool UpdateAttendance(int studentId, int scheduleId, bool isPresent);
        bool CanUpdate(DateOnly date, TimeOnly time);
    }
}
