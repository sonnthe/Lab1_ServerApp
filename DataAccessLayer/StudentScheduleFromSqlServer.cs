using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentScheduleFromSqlServer : IStudentScheduleRepository
    {
        public bool UpdateAttendance(int studentId, int scheduleId, bool isPresent)
        {
            using (var context = new Lab1Prn222Context())
            {
                var studentSchedule = context.StudentSchedules
                    .FirstOrDefault(ss => ss.StudentId == studentId && ss.ScheduleId == scheduleId);
                if (studentSchedule != null && studentSchedule.IsPresent!=isPresent)
                {
                    studentSchedule.IsPresent = isPresent;
                    int changes= context.SaveChanges();
                    return changes > 0; 
                }              
            }
            return true;
        }
    }
}
