// Filename: SuccessTestCase.cs
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
    public sealed class SuccessTestCase<T> : TestCase<T>
    {
        public SuccessTestCase(ConstructorInfo constructorInfo, object[] parameters, string failMessage) : base(constructorInfo, parameters, failMessage)
        {
        }

        public override string Execute()
        {
            try
            {
                this.Invoke();
            }
            catch (Exception exception)
            {
                return this.Failure($"{exception.GetType().Name} occurred: {exception.Message}");
            }

            return this.Success();
        }
    }
}
