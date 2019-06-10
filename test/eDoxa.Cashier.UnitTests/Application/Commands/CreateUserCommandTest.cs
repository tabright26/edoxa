// Filename: CreateUserCommandTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Commands
{
    [TestClass]
    public sealed class CreateUserCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CreateUserCommand>.ForParameters(
                    typeof(UserId),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(int),
                    typeof(int),
                    typeof(int)
                )
                .WithClassName("CreateUserCommand")
                .WithClassAttributes()
                .Assert();
        }
    }
}
