// Filename: NamingTestCase.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

namespace eDoxa.Testing.MSTest
{
    public sealed class NameTestCase<T> : TestCase<T>
    {
        private readonly string _className;
        private readonly ConstructorInfo _info;

        public NameTestCase(ConstructorInfo info, string className) : base(info, Array.Empty<object>(), "Invalid class name.")
        {
            _info = info;
            _className = className;
        }

        public override string Execute()
        {
            return _className != _info.DeclaringType.Name
                ? this.Fail($"The expected class name ({_className}) does not match the current class name ({_info.DeclaringType.Name}).")
                : this.Success();
        }
    }
}