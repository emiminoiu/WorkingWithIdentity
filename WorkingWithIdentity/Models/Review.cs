using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Review : BaseEntity
    {
        public decimal ReviewScore { get; set; } 
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public List<CourseReview> CourseReviews { get; set; }

    }
}
