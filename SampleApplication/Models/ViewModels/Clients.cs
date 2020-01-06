using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApplication.Models.Entities;

namespace SampleApplication.Models.ViewModels
{
    public class Clients
    {
        IEnumerable<Customer> Customers { get; set; }
        IEnumerable<Card> Cards { get; set; }
        IEnumerable<Account> Accounts { get; set; }
    }
}
