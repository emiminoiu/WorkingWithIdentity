using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
