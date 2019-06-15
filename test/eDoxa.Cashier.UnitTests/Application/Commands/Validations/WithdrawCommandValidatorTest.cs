// Filename: WithdrawCommandValidatorTest.cs
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
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Validations
{
    [TestClass]
    public sealed class WithdrawCommandValidatorTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawCommandValidator>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountRepository))
                .WithClassName("WithdrawCommandValidator")
                .Assert();
        }
    }
}
