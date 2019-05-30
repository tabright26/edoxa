// Filename: TypeObjectTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Tests.Aggregate
{
    [TestClass]
    public sealed class TypeObjectTest
    {
        [TestMethod]
        public void M()
        {
            var qw = new MockTypedObject(15);

            var list = new List<MockTypedObject>
            {
                qw,
                new MockTypedObject(60),
                new MockTypedObject(1),
                new MockTypedObject(2),
                new MockTypedObject(5),
                new MockTypedObject(100),
                new MockTypedObject(50)
            };

            var order = list.OrderBy(x => x).ToList();

            var t = list.Any(x => x.Equals(qw));
        }

        [TestMethod]
        public void M1()
        {
            var types = MockTypedObject.GetValues();
        }

        private sealed class MockTypedObject : TypedObject<MockTypedObject, int>
        {
            public static readonly MockTypedObject Object1 = new MockTypedObject(1);
            public static readonly MockTypedObject Object2 = new MockTypedObject(2);
            public static readonly MockTypedObject Object3 = new MockTypedObject(3);
            public static readonly MockTypedObject Object4 = new MockTypedObject(4);
            public static readonly MockTypedObject Object5 = new MockTypedObject(5);
            public static readonly MockTypedObject Object6 = new MockTypedObject(6);

            public MockTypedObject(int value)
            {
                Value = value;
            }
        }
    }
}
