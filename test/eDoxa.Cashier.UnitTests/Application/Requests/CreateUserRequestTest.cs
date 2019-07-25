// Filename: CreateUserRequestTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Requests
{
    [TestClass]
    public sealed class CreateUserRequestTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CreateUserRequest>.ForParameters(typeof(UserId)).WithClassName("CreateUserRequest").WithClassAttributes().Assert();
        }
    }
}
