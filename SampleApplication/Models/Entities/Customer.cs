using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApplication.Models.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string ShippingAddress { get; set; }
        public string TrusttId { get; set; }
        public ICollection<Card> Cards {get; set;}
        public ICollection<Account> Accounts {get; set;}
    }
}
