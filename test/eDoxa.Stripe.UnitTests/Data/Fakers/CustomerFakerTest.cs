using System;

using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Data.Fakers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Data.Fakers
{
    [TestClass]
    public sealed class CustomerFakerTest
    {
        [TestMethod]
        public void M()
        {
            var customerFaker = new CustomerFaker();
            
            var customer = customerFaker.Generate();

            Console.WriteLine(customer.DumbAsJson());
        }
    }
}
