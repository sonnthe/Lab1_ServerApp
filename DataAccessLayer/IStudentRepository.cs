using BusinessObjects.DTOs;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IStudentRepository
    {

        List<StudentDTO> GetStudentsByCourseIdAndScheduleId(int courseId, int scheduleId);

    }
}
