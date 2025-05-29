using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int ScheduleId { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        override
        public String ToString()
        {
            return $" {Title} - {Date} - {Time}";
        }
    }
}
