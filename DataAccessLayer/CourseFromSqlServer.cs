using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CourseFromSqlServer : ICourseRepository
    {
        public List<Course> GetCourses()
        {
            using (var context = new Lab1Prn222Context())
            {
                return [.. context.Courses];
            }
        }
    }
}
