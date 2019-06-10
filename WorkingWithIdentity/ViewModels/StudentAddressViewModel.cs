using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.ViewModels
{
    public class StudentAddressViewModel
    {
        public string Address { get; set; }
        public Student Student { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }

    }
}
