// Filename: AccountTransactionsControllerTest.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Testing.MSTest;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class AccountTransactionsControllerTest
    {
        private Mock<ITransactionQueries> _mockTransactionQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTransactionQueries = new Mock<ITransactionQueries>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountTransactionsController>.For(typeof(ITransactionQueries))
                .WithName("AccountTransactionsController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute), typeof(ApiExplorerSettingsAttribute))
                .Assert();
        }
    }
}