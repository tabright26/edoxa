// Filename: ClassNameTestCase.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Seedwork.Testing.TestConstructor.Abstractions;

namespace eDoxa.Seedwork.Testing.TestConstructor.TestCases
{
    public sealed class ClassNameTestCase<T> : TestCase<T>
    {
        private readonly string _className;
        private readonly ConstructorInfo _constructorInfo;

        public ClassNameTestCase(ConstructorInfo constructorInfo, string className) : base(
            constructorInfo,
            Array.Empty<object>(),
            "The class name does not match"
        )
        {
            _constructorInfo = constructorInfo;
            _className = className;
        }

        public override string Execute()
        {
            return _className != _constructorInfo.DeclaringType.Name
                ? this.Failure($"The expected class name ({_className}) does not match the current class name ({_constructorInfo.DeclaringType.Name}).")
                : this.Success();
        }
    }
}
