﻿// Filename: EnumerationTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Aggregate
{
    [TestClass]
    public sealed class EnumerationTest
    {
        [TestMethod]
        public void M()
        {
            var values = MockEnumeration.GetValues();

            var names = MockEnumeration.GetNames();

            var enums = MockEnumeration.GetEnums();

            var none = MockEnumeration.None;

            var all = MockEnumeration.All;
        }

        private sealed class MockEnumeration : Enumeration<MockEnumeration>
        {
            public static readonly MockEnumeration Enumeration1 = new MockEnumeration(1 << 0, nameof(Enumeration1));
            public static readonly MockEnumeration Enumeration2 = new MockEnumeration(1 << 1, nameof(Enumeration2));
            public static readonly MockEnumeration Enumeration4 = new MockEnumeration(1 << 2, nameof(Enumeration4));
            public static readonly MockEnumeration Enumeration8 = new MockEnumeration(1 << 3, nameof(Enumeration8));
            public static readonly MockEnumeration Enumeration16 = new MockEnumeration(1 << 4, nameof(Enumeration16));

            public MockEnumeration()
            {
            }

            private MockEnumeration(int value, string name) : base(value, name)
            {
            }
        }
    }
}