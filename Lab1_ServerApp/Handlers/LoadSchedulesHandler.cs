using BusinessObjects;
using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_ServerApp.Handlers
{
    public class LoadSchedulesHandler : IRequestHandler
    {
        public bool CanHandle(string request) => request.StartsWith("LoadSchedulesBySelectedCourse:");
        public string Handle(string request)
        {
            int courseId = int.Parse(request.Split(':')[1]);
            try
            {
                var _scheduleService = DIContainer.Resolve<IScheduleService>();
                var schedulesList = _scheduleService!.GetSchedulesByCourseId(courseId);
                if (schedulesList != null)
                {
                    return System.Text.Json.JsonSerializer.Serialize(schedulesList);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading schedules for course: {ex.Message}");
            }
            return String.Empty;
        }
    }
}
