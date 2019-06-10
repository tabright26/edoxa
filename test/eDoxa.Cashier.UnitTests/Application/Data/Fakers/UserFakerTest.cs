// Filename: UserFakerTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Api.Application.Data.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class UserFakerTest
    {
        [TestMethod]
        public void FakeNewUsers()
        {
            var userFaker = new UserFaker();

            var users = userFaker.FakeNewUsers(3);

            Console.WriteLine(users.DumbAsJson());

            users = userFaker.FakeNewUsers(3);

            Console.WriteLine(users.DumbAsJson());
        }
    }
}
