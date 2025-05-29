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
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.SlotIndex))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(s => "Slot "+ s.SlotIndex ));
        }
    }
}
