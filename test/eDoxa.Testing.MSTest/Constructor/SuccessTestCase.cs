// Filename: SuccessTestCase.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

namespace eDoxa.Testing.MSTest.Constructor
{
    public sealed class SuccessTestCase<T> : TestCase<T>
    {
        public SuccessTestCase(ConstructorInfo info, object[] args, string failMessage) : base(info, args, failMessage)
        {
        }

        public override string Execute()
        {
            try
            {
                this.InvokeConstructor();
            }
            catch (Exception exception)
            {
                return this.Fail($"{exception.GetType().Name} occurred: {exception.Message}");
            }

            return this.Success();
        }
    }
}
