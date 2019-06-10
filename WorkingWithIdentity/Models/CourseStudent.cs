using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithIdentity.Models
{
    public class CourseStudent
    {
        
        public string CourseId {get;set;}
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
    
}