using BusinessObjects;
using BusinessObjects.Models;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository = DIContainer.ServiceProvider.GetRequiredService<ICourseRepository>();
        public List<Course> GetCourses()
        {
            return _courseRepository.GetCourses();
        }
    }
}
