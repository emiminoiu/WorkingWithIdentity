using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
  
    public class Grade : BaseEntity
    {
       
        public string GradeName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
