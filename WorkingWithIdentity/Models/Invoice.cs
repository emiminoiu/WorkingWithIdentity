using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class Invoice : BaseEntity
    {
       
        public decimal Total { get; set; }
        public string Concept { get; set; }
        public string ClientId { get; set; }
        public IdentityUser Client { get; set; }
        
    }
}
