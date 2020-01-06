using System;

namespace SampleApplication.Models.Entities
{
    public class Card : BaseEntity
    {
        public string Number { get; set; }
        public string CVV    { get; set; }
        public string Year   { get; set; }
        public string Month  { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
