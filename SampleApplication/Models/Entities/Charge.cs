using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApplication.Common;

namespace SampleApplication.Models.Entities
{
    public enum ChargeType
    {
        Card, Bank
    }

    public enum ChargeStatus
    {
        InProcess, Complete, Rejected, Canceled
    }

    public class Charge : BaseEntity
    {
        public Guid TrusttTransactionId { get; set; }
        public ChargeStatus ChargeStatus { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime BeginTime { get; set; }
        public PaymentCurrency Currency {get; set;}
        public long Amount { get; set; }
        public ChargeType ChargeType { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
