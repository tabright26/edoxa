// Filename: CreateBankAccountCommandValidatorTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Commands.Validations;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Validations
{
    [TestClass]
    public sealed class CreateBankAccountCommandValidatorTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CreateBankAccountCommandValidator>.ForParameters(typeof(IHttpContextAccessor), typeof(IUserQuery))
                .WithClassName("CreateBankAccountCommandValidator")
                .Assert();
        }
    }
}
