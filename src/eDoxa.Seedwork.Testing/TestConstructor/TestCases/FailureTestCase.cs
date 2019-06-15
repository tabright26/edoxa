// Filename: FailureTestCase.cs
// Date Created: 2019-06-08
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
    public class FailureTestCase<T> : TestCase<T>
    {
        private Type _exceptionType;

        public FailureTestCase(
            ConstructorInfo constructorInfo,
            object[] parameters,
            Type exceptionType,
            string failMessage
        ) : base(constructorInfo, parameters, failMessage)
        {
            _exceptionType = exceptionType;
        }

        public override string Execute()
        {
            try
            {
                this.Invoke();

                return this.Failure($"{_exceptionType.Name} not thrown when expected.");
            }
            catch (Exception exception)
            {
                if (exception.GetType() != _exceptionType)
                {
                    return this.Failure($"{exception.GetType().Name} thrown when {_exceptionType.Name} was expected.");
                }
            }

            return this.Success();
        }
    }
}
