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
        public bool UpdateAttendance(List<int> studentIdList, int scheduleId)
        {
            using (var context = new Lab1Prn222Context())
            {
                for(var i = 0; i < studentIdList.Count; i++)
                {
                    var studentId = studentIdList[i];
                    var attendance = context.StudentSchedules.FirstOrDefault(a => a.StudentId == studentId && a.ScheduleId == scheduleId);
                    if (attendance != null)
                    {
                        attendance.IsPresent = !attendance.IsPresent;
                        context.SaveChanges();
                        int updatedCount = context.SaveChanges();
                        if (updatedCount <= 0)
                        {
                            Console.WriteLine($"Failed to update attendance for student ID: {studentId} in schedule ID: {scheduleId}");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
