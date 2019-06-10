// Filename: FakeUserCsv.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Api.Application.Data.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public class UserFakerTest
    {
        [TestMethod]
        public void Test()
        {
            var userFaker = new UserFaker();

            var users = userFaker.FakeNewUsers(99);

            //Console.WriteLine(users.DumbAsJson());

            //var user = userFaker.FakeAdminUser();

            Console.WriteLine(users.DumbAsJson());
        }

        [TestMethod]
        public void Test1()
        {
            var roleFaker = new RoleFaker();

            var roles = roleFaker.FakeRoles();

            Console.WriteLine(roles.DumbAsJson());
        }
    }
}
