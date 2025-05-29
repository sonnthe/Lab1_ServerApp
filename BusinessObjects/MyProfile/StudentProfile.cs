using AutoMapper;
using BusinessObjects.DTOs;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.MyProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(s => s.Name))
                .ForMember(dest => dest.IsPresent, opt =>opt.MapFrom((src, dest, destMember, context) =>
                {
                    var scheduleId = (int)context.Items["ScheduleId"];
                    return src.StudentSchedules.Any(ss => ss.ScheduleId == scheduleId && ss.IsPresent);
                }));
        }
    }
}
