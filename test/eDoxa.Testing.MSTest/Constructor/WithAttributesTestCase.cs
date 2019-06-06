// Filename: WithAttributesTestCase.cs
// Date Created: 2019-05-20
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

namespace eDoxa.Testing.MSTest.Constructor
{
    public sealed class WithAttributesTestCase<T> : TestCase<T>
    {
        private readonly Type[] _attributeTypes;
        private readonly ConstructorInfo _info;

        public WithAttributesTestCase(ConstructorInfo info, Type[] attributeTypes) : base(info, Array.Empty<object>(), "Invalid class name.")
        {
            _info = info;
            _attributeTypes = attributeTypes;
        }

        public override string Execute()
        {
            var types = _info.DeclaringType.CustomAttributes.Select(x => x.AttributeType).ToArray();
            var attributeTypes = _attributeTypes;

            return !types.SequenceEqual(attributeTypes)
                ? this.Fail(
                    $"The expected class attributes ({_attributeTypes}) does not match the current class attributes ({_info.DeclaringType.CustomAttributes})."
                )
                : this.Success();
        }
    }
}
