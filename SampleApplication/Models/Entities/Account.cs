using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApplication.Models.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string Swift { get; set; }
        public string IBAN { get; set; }
    }
}
