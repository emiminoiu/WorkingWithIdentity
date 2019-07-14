using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.ViewModels
{
    public class CoursesViewModel : BaseEntity
    {
        public string AuthorName { get; set; }
        public string CourseName { get; set; }
        public decimal RatingScore { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public int NoOfStudents { get; set; }
    }
}
