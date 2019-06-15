// Filename: AttributesTestCase.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Reflection;

using eDoxa.Seedwork.Testing.TestConstructor.Abstractions;

namespace eDoxa.Seedwork.Testing.TestConstructor.TestCases
{
    public sealed class AttributesTestCase<T> : TestCase<T>
    {
        private readonly Type[] _classAttributeTypes;
        private readonly ConstructorInfo _constructorInfo;

        public AttributesTestCase(ConstructorInfo constructorInfo, Type[] classAttributeTypes) : base(
            constructorInfo,
            Array.Empty<object>(),
            "The class attribute types do not match any constructor."
        )
        {
            _constructorInfo = constructorInfo;
            _classAttributeTypes = classAttributeTypes;
        }

        public override string Execute()
        {
            var types = _constructorInfo.DeclaringType.CustomAttributes.Select(data => data.AttributeType).ToArray();
            var classAttributeTypes = _classAttributeTypes;

            return !types.SequenceEqual(classAttributeTypes)
                ? this.Failure(
                    $"The expected class attributes ({_classAttributeTypes}) does not match the current class attributes ({_constructorInfo.DeclaringType.CustomAttributes})."
                )
                : this.Success();
        }
    }
}
