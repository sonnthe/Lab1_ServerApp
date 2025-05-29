using BusinessObjects.DTOs;
using BusinessObjects.Models;


namespace Services
{
    public interface IStudentService
    {
            List<StudentDTO> GetStudentsByCourseIdAndScheduleId(int courseId, int scheduleId);
    }
}
