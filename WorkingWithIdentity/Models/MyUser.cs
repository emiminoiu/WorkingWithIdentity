using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class MyUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public int NoOfCourses { get; set; }
        public decimal Budget { get; set; }
     
       
    }
}
