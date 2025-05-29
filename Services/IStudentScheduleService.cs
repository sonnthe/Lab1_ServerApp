using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStudentScheduleService
    {
        bool UpdateAttendance(List<int > studentIdList,int scheduleId);
        bool CanUpdate(DateOnly date, TimeOnly time);
    }
}
