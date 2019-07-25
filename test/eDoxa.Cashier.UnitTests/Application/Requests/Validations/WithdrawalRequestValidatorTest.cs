// Filename: WithdrawalRequestValidatorTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Application.Requests.Validations;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Requests.Validations
{
    [TestClass]
    public sealed class WithdrawalRequestValidatorTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawalRequestValidator>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountQuery))
                .WithClassName("WithdrawalRequestValidator")
                .Assert();
        }
    }
}
