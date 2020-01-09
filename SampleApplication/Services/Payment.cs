using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Common;
using SampleApplication.Data;
using SampleApplication.Models.Entities;
using TrustTPaymentSDK;
using SDK = TrustTPaymentSDK.Models;

namespace SampleApplication.Services
{
    public class Payment
    {
        private readonly TrusttAPI _trustt;
        private readonly DataContext _context; // TODO fix this, breaking architecture

        public Payment(
            DataContext context,
            TrusttAPI trustt)
        {
            _context = context;
            _trustt = trustt;
        }


        public Charge Charge(Charge charge)
        {
            var tc = new SDK.Charge
            {
                Amount = charge.Amount,
                Currency = SDK.ChargeCurrency.EUR,
                CustomerId = charge.CustomerId
            };

            var transaction = _trustt.Charge(tc);

            if (transaction != default && transaction.IsOk)
            {
                charge.TrusttTransactionId = _trustt.Charge(tc).Payload.Id;
                charge.ChargeStatus = ChargeStatus.Complete;
            }
            else
            {
                charge.ChargeStatus = ChargeStatus.Rejected;
            }

            charge.EndTime = DateTime.Now;
            return charge;

        } // method


        /// <summary>
        /// Create a bank account related to a customer.
        /// </summary>
        /// <param name="account">bank account info</param>
        /// <returns>created bank account ID at trustt</returns>
        public Guid CreateBankAccount(Account account)
        {
            var tba = new SDK.BankAccount
            {                 // TODO _context should not be here
                CustomerId = _context.Customers.FirstOrDefault(c => c.Id == account.CustomerId).TrusttId,
                BeneficiaryName = account.Name,
                Swift = account.Swift,
                IBAN = account.IBAN
            };

            var response = _trustt.AddBankAccount(tba);

            // handle request's error
            // throw exception if was not posible to create
            if (response.IsError)
                throw new OperationCanceledException
                    (response.ErrorMessage);

            return response.Payload.Id;
        }



        /// <summary>
        /// Add a card to trustt api.
        /// </summary>
        /// <param name="card">card entity holding data</param>
        /// <returns>created trustt card id</returns>
        public Guid CreateCard(Card card)
        {
            var tc = new SDK.Card
            {
                CVV = card.CVV,
                ExpirationMonth = card.Month,
                ExpirationYear = card.Year, // TODO _context should not be here
                CustomerId = _context.Customers.FirstOrDefault(c => c.Id == card.CustomerId).TrusttId
            };

            var response = _trustt.AddCard(tc);

            // handle request's error
            // throw exception if was not posible to create
            if (response.IsError)
                throw new OperationCanceledException
                    (response.ErrorMessage);

            return response.Payload.Id;
        }


        /// <summary>
        /// Create a customer in trustt payment system
        /// </summary>
        /// <param name="customer">local customer (entity)</param>
        /// <returns>customer with trustt id</returns>
        public Guid CreateCustomer(Customer customer)
        {
            // customer as requiere SDK
            var tc = new SDK.Customer
            {
                Email = customer.Email,
                Name = customer.Name
            };

            // customer as was created in sdk
            var response  = _trustt.AddCustomer(tc);

            // handle request's error
            // throw exception if was not posible to create
            if (response.IsError)
                throw new OperationCanceledException
                    (response.ErrorMessage);

            // update customer
            customer.TrusttId = response.Payload.Id;

            // get back customer
            return customer.Id;
        }


    } // class
}
