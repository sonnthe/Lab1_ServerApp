using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IStudentScheduleRepository
    {
        bool UpdateAttendance(List<int> studentIdList, int scheduleId);
    }
}
