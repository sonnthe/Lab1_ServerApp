using AutoMapper;
using BusinessObjects.DTOs;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ScheduleFromSqlServer : IScheduleRepository
    {
        private readonly IMapper _mapper =MyMapper.Instance.Mapper;
        public List<ScheduleDTO> GetSchedulesByCourseId(int courseId)
        {
            using (var context = new Lab1Prn222Context())
            {
                List<Schedule> schedules = context.Schedules
                    .Where(s => s.CourseId == courseId)
                    .ToList();
                return [.._mapper.Map<List<ScheduleDTO>>(schedules)];
            }
        }
    }
}
