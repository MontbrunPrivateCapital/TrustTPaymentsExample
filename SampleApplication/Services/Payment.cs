using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApplication.Models.Entities;
using TrustTPaymentSDK;
using SDK = TrustTPaymentSDK.Models;

namespace SampleApplication.Services
{
    public class Payment
    {
        private readonly TrusttAPI _trustt;

        public Payment(TrusttAPI trustt)
        {
            _trustt = trustt;
        }


        /// <summary>
        /// Create a customer in trustt payment system
        /// </summary>
        /// <param name="customer">local customer (entity)</param>
        /// <returns>customer with trustt id</returns>
        public Customer CreateCustomer(Customer customer)
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
            return customer;
        }


    } // class
}
