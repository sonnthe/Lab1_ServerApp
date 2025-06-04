using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Lab1_ServerApp.Handlers
{
    public class UpdateAttendanceHandler : IRequestHandler
    {
        public bool CanHandle(string request) => request.StartsWith("UpdateAttendance:");
        public string Handle(string request)
        {
            var parts = request.Split(':');
            string studentIdListStr = parts[1];
            List<int> studentIdList = JsonSerializer.Deserialize<List<int>>(studentIdListStr) ?? [];
            int scheduleIdStr = int.Parse(parts[2]);
            try
            {
                var _attendanceService = DIContainer.Resolve<IStudentScheduleService>();
                    return _attendanceService.UpdateAttendance(studentIdList, scheduleIdStr) ? "Successful" : "Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating attendance: {ex.Message}");
            }
            return "Failed";
        }
    }
}
