using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Course : BaseEntity
    {
        
        public string CourseName { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
        public List<Comment> Comments { get; set; }
        public string Image { get; set; }
        
    }
}
