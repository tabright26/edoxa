using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Aggregate
{
    [TestClass]
    public sealed class TypeObjectTest
    {
        [TestMethod]
        public void M()
        {
            var qw = new MockTypeObject(15);

            var list = new List<MockTypeObject>
            {
                qw,
                new MockTypeObject(60),
                new MockTypeObject(1),
                new MockTypeObject(2),
                new MockTypeObject(5),
                new MockTypeObject(100),
                new MockTypeObject(50)
            };

            var order = list.OrderBy(x => x).ToList();

            var t = list.Any(x => x.Equals(qw));
        }

        [TestMethod]
        public void M1()
        {
            var types = MockTypeObject.GetValues();
        }

        private sealed class MockTypeObject : TypeObject<MockTypeObject, int>
        {
            public static readonly MockTypeObject Object1 = new MockTypeObject(1);
            public static readonly MockTypeObject Object2 = new MockTypeObject(2);
            public static readonly MockTypeObject Object3 = new MockTypeObject(3);
            public static readonly MockTypeObject Object4 = new MockTypeObject(4);
            public static readonly MockTypeObject Object5 = new MockTypeObject(5);
            public static readonly MockTypeObject Object6 = new MockTypeObject(6);

            public MockTypeObject(int value) : base(value)
            {
            }
        }
    }
}
