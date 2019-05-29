// Filename: ParticipantExternalAccountTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Domain;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.AggregateModels.ParticipantAggregate
{
    [TestClass]
    public sealed class ParticipantExternalAccountTest
    {
        [TestMethod]
        public void Constructor_TypeOfString_Tests()
        {
            const string message = "Linked account validation failed.";

            ConstructorTests<ExternalAccount>.For(typeof(string))
                .WithName("ExternalAccount")
                .Fail(new object[] {null}, typeof(ArgumentException), message)
                .Fail(new object[] {"  "}, typeof(ArgumentException), message)
                .Fail(new object[] {"9i8h7g 6f5e4d3 c2b1a0"}, typeof(ArgumentException), message)
                .Fail(new object[] {"!@#$%^&*()"}, typeof(ArgumentException), message)
                .Succeed(new object[] {"9876543210"}, message)
                .Succeed(new object[] {"abcdefghijklmnopqrstuvwxyz"}, message)
                .Succeed(new object[] {"ABCDEFGHIJKLMNOPQRSTUVWXYZ"}, message)
                .Succeed(new object[] {"9i8h7g6f5e4d3c2b1a0"}, message)
                .Succeed(new object[] {"9i8h7g-6f5e4d3_c2b1a0"}, message)
                .Assert();
        }

        [TestMethod]
        public void Constructor_TypeOfGuid_Tests()
        {
            const string message = "Participant external account validation failed.";

            ConstructorTests<ExternalAccount>.For(typeof(Guid))
                .WithName("ExternalAccount")
                .Fail(new object[] {Guid.Empty}, typeof(ArgumentException), message)
                .Succeed(new object[] {Guid.NewGuid()}, message)
                .Assert();
        }
    }
}
