// Filename: EntryFeeTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests
{
    [TestClass]
    public sealed class EntryFeeTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "EntryFee validation failed.";

            ConstructorTests<EntryFee>.For(typeof(decimal), typeof(bool))
                .WithName("EntryFee")
                .Fail(new object[] {EntryFee.Min - 1, true}, typeof(ArgumentException), message)
                .Fail(new object[] {EntryFee.Max + 1, true}, typeof(ArgumentException), message)
                .Succeed(new object[] {EntryFee.Min, true}, message)
                .Succeed(new object[] {EntryFee.Max, true}, message)
                .Assert();
        }
    }
}