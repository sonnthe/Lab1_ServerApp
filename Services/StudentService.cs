using BusinessObjects;
using BusinessObjects.DTOs;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository = DIContainer.ServiceProvider.GetRequiredService<IStudentRepository>();

        public List<StudentDTO> GetStudentsByCourseIdAndScheduleId(int courseId, int scheduleId)
        {
            return _studentRepository.GetStudentsByCourseIdAndScheduleId(courseId, scheduleId);
        }

    }
}
