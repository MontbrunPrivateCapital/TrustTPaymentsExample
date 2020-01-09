using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustTPaymentSDK.Models;

namespace SampleApplication.Models.ViewModels
{
    public class MyAccountStatus : Account
    {
        public IList<Gold> Gold { get; set; }
    } // class
}
