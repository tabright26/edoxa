// Filename: DepositCommandValidatorTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Commands.Validations;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Testing.Constructor;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Commands.Validations
{
    [TestClass]
    public sealed class DepositCommandValidatorTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositCommandValidator>
                .For(typeof(IHttpContextAccessor), typeof(IAccountRepository), typeof(IUserRepository), typeof(IStripeService))
                .WithName("DepositCommandValidator")
                .Assert();
        }
    }
}
