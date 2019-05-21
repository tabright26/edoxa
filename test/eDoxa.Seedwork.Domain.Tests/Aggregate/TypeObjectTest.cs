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

            var t = list.Any(x => x == qw);
        }

        private sealed class MockTypeObject : TypeObject<MockTypeObject, int>
        {
            public MockTypeObject(int value, bool validate = true) : base(value, validate)
            {
            }

            public static implicit operator int(MockTypeObject mockTypeObject)
            {
                return mockTypeObject.Value;
            }
        }
    }
}
