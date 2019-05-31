// Filename: DepositCommandValidatorTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Commands.Validations;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Stripe.Abstractions;
using eDoxa.Testing.MSTest;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Commands.Validations
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
