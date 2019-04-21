// Filename: FailTestCase.cs
// Date Created: 2019-04-20
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
    public class FailTestCase<T> : TestCase<T>
    {
        private Type _exceptionType;

        public FailTestCase(ConstructorInfo info, object[] args, Type exceptionType, string failMessage) : base(info, args, failMessage)
        {
            _exceptionType = exceptionType;
        }

        public override string Execute()
        {
            try
            {
                this.InvokeConstructor();

                return this.Fail($"{_exceptionType.Name} not thrown when expected.");
            }
            catch (Exception exception)
            {
                if (exception.GetType() != _exceptionType)
                {
                    return this.Fail($"{exception.GetType().Name} thrown when {_exceptionType.Name} was expected.");
                }
            }

            return this.Success();
        }
    }
}