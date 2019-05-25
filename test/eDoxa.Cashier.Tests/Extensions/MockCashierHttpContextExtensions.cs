// Filename: MockCashierHttpContextExtensions.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Seedwork.Domain.Entities;

using Moq;

namespace eDoxa.Cashier.Tests.Extensions
{
    public static class MockCashierHttpContextExtensions
    {
        public static void SetupGetProperties(this Mock<ICashierHttpContext> mockCashierHttpContext)
        {
            mockCashierHttpContext.SetupGet(mock => mock.UserId).Returns(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));
        }
    }
}
