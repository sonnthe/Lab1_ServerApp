using BusinessObjects.DTOs;
using BusinessObjects.Models;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository = DIContainer.ServiceProvider.GetRequiredService<IScheduleRepository>();
        public List<ScheduleDTO> GetSchedulesByCourseId(int courseId)
        {
            return _scheduleRepository.GetSchedulesByCourseId(courseId);
        }
    }
}
