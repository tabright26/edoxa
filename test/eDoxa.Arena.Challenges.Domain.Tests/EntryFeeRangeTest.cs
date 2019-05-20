// Filename: EntryFeeRangeTest.cs
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

namespace eDoxa.Arena.Challenges.Domain.Tests
{
    [TestClass]
    public sealed class EntryFeeRangeTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "EntryFeeRange validation failed.";

            ConstructorTests<EntryFeeRange>.For(typeof(EntryFee), typeof(EntryFee))
                .WithName("EntryFeeRange")
                .Succeed(new object[] {new EntryFee(EntryFee.Min), new EntryFee(EntryFee.Max)}, message)
                .Fail(new object[] {new EntryFee(EntryFee.Max), new EntryFee(EntryFee.Min)}, typeof(ArgumentException), message)
                .Assert();
        }
    }
}