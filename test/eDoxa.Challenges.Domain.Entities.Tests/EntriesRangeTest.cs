// Filename: EntriesRangeTest.cs
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

namespace eDoxa.Challenges.Domain.Entities.Tests
{
    [TestClass]
    public sealed class EntriesRangeTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "EntriesRange validation failed.";

            ConstructorTests<EntriesRange>.For(typeof(Entries), typeof(Entries))
                .WithName("EntriesRange")
                .Succeed(new object[] {new Entries(Entries.Min), new Entries(Entries.Max)}, message)
                .Fail(new object[] {new Entries(Entries.Max), new Entries(Entries.Min)}, typeof(ArgumentException), message)
                .Assert();
        }
    }
}