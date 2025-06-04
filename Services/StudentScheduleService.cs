using BusinessObjects;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StudentScheduleService : IStudentScheduleService
    {
        private readonly IStudentScheduleRepository _studentScheduleRepository = DIContainer.ServiceProvider.GetRequiredService<IStudentScheduleRepository>();

        public bool CanUpdate(DateOnly date, TimeOnly time)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

            return date >= today && (date > today || time >= currentTime);
        }

        public bool UpdateAttendance(List<int> studentIdList, int scheduleId)
        {
            return _studentScheduleRepository.UpdateAttendance(studentIdList, scheduleId);
        }
    }
}
