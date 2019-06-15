// Filename: AllowValueAttributeTest.cs
// Date Created: 2019-06-15
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
using eDoxa.Seedwork.Domain.Attributes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Attributes
{
    [TestClass]
    public sealed class AllowValueAttributeTest
    {
        [TestMethod]
        public void ValueObject_GetAllowValues_ShouldBeAllowValues()
        {
            // Act
            var valueObjects = ValueObject.GetAllowValues<AllowValueObject>().ToList();

            // Assert
            valueObjects.Should().HaveCount(1);
            valueObjects.Should().Contain(AllowValueObject.AllowValue1);
        }

        [TestMethod]
        public void ValueObject_GetValues_ShouldBeAllValues()
        {
            // Act
            var valueObjects = ValueObject.GetValues<AllowValueObject>().ToList();

            // Assert
            valueObjects.Should().HaveCount(2);
            valueObjects.Should().Contain(AllowValueObject.AllowValue1);
            valueObjects.Should().Contain(AllowValueObject.AllowValue2);
        }

        [TestMethod]
        public void Enumeration_GetAllAllow_ShouldBeAllAllow()
        {
            // Act
            var enumerations = AllowValueEnumeration.GetEnumerations(true).ToList();

            // Assert
            enumerations.Should().HaveCount(1);
            enumerations.Should().Contain(AllowValueEnumeration.AllowValue1);
        }

        [TestMethod]
        public void Enumeration_GetAll_ShouldBeAll()
        {
            // Act
            var enumerations = AllowValueEnumeration.GetEnumerations().ToList();

            // Assert
            enumerations.Should().HaveCount(2);
            enumerations.Should().Contain(AllowValueEnumeration.AllowValue1);
            enumerations.Should().Contain(AllowValueEnumeration.AllowValue2);
        }

        private sealed class AllowValueObject : ValueObject
        {
            [AllowValue(true)] public static readonly AllowValueObject AllowValue1 = new AllowValueObject(1);
            [AllowValue(false)] public static readonly AllowValueObject AllowValue2 = new AllowValueObject(2);

            public AllowValueObject(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Value;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private sealed class AllowValueEnumeration : Enumeration<AllowValueEnumeration>
        {
            [AllowValue(true)] public static readonly AllowValueEnumeration AllowValue1 = new AllowValueEnumeration(1, nameof(AllowValue1));
            [AllowValue(false)] public static readonly AllowValueEnumeration AllowValue2 = new AllowValueEnumeration(2, nameof(AllowValue2));

            public AllowValueEnumeration()
            {
            }

            private AllowValueEnumeration(int value, string name) : base(value, name)
            {
            }
        }
    }
}
