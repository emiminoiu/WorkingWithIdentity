using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Comment : BaseEntity
    {    
        public string UserId { get; set; }
        public string CourseId { get; set; }
        public string CommentContent { get; set; }
        public Course Course { get; set; }
        public IdentityUser User { get; set; }
        public string TimeStamp { get; set; }
    }
}
