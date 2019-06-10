using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Student : BaseEntity
    {
        
        public string Name { get; set; }
        public string GradeName { get; set; }
        public string GradeId { get; set; }
        public Grade Grade { get; set; }
        public byte[] Image { get; set; }
        public List<CourseStudent> CourseStudent { get; set; }
        public StudentAddress StudentAddress { get; set; }
    }

}
