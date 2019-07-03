using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Course : BaseEntity
    {
        public string AuthorName { get; set; }
        public string CourseName { get; set; }
        public decimal RatingScore { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CourseReview> CourseReviews { get; set; }
        public string Image { get; set; }
        public List<UserCourse> UserCourses { get; set; }
    }
}
