using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_ServerApp.Handlers
{
    public class LoadStudentHandler : IRequestHandler
    {
        public bool CanHandle(string request) => request.StartsWith("LoadStudentsByCIdAndSId:");

        public string Handle(string request)
        {
            var parts = request.Split(':');
            int courseId = int.Parse(parts[1]);
            int scheduleId = int.Parse(parts[2]);
            Console.WriteLine($"Loading students for course ID: {courseId}, schedule ID: {scheduleId}");

            try
            {
                var studentService = DIContainer.Resolve<IStudentService>();
                var students = studentService.GetStudentsByCourseIdAndScheduleId(courseId, scheduleId);
                if (students != null)
                {
                    return System.Text.Json.JsonSerializer.Serialize(students);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading students: {ex.Message}");
            }
            return String.Empty;
        }
    }
}
