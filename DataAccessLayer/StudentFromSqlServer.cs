using AutoMapper;
using BusinessObjects.DTOs;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentFromSqlServer : IStudentRepository
    {
        private readonly IMapper? _mapper = MyMapper.Instance.Mapper;
        public List<StudentDTO> GetStudentsByCourseIdAndScheduleId(int courseId, int scheduleId)
        {
            using (var context = new Lab1Prn222Context())
            {
                List<Student> students = [.. context.Students
                    .Where(
                    s => s.Courses.Any(c => c.Id == courseId)
                    )
                    .Include(c => c.StudentSchedules)];

                return _mapper!.Map<List<StudentDTO>>(students, opts => opts.Items["ScheduleId"] = scheduleId);
            }
        }
    }
}
