using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.ViewModels
{
    public class StudentCoursesViewModel
    {
        public string StudentId { get; set; }
        public List<string> CoursesIds { get; set; }
    }
}
