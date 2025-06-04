using BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_ServerApp.Handlers
{
    public class CourseListHandler : IRequestHandler
    {
        private ICourseService? _courseService;
        public bool CanHandle(string request) => request.Equals("CourseList");

        public string Handle(string request)
        {
            try
            {
                 _courseService = DIContainer.Resolve<ICourseService>();
                var courseList = _courseService!.GetCourses();
                if (courseList != null)
                {
                    return System.Text.Json.JsonSerializer.Serialize(courseList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading courses: {ex.StackTrace}");
            }
            return String.Empty;
        }
     }
    }
