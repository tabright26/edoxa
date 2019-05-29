// Filename: EnumerationTest.cs
// Date Created: 2019-05-20
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
        private sealed class MockEnumeration : Enumeration
        {
            public static readonly MockEnumeration Enumeration1 = new MockEnumeration(1 << 0, nameof(Enumeration1));
            public static readonly MockEnumeration Enumeration2 = new MockEnumeration(1 << 1, nameof(Enumeration2));
            public static readonly MockEnumeration Enumeration4 = new MockEnumeration(1 << 2, nameof(Enumeration4));
            public static readonly MockEnumeration Enumeration8 = new MockEnumeration(1 << 3, nameof(Enumeration8));
            public static readonly MockEnumeration Enumeration16 = new MockEnumeration(1 << 4, nameof(Enumeration16));

            private MockEnumeration(int value, string name) : base(value, name)
            {
            }
        }
    }
}
