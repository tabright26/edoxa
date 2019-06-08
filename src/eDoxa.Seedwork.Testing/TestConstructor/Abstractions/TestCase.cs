// Filename: TestCase.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

namespace eDoxa.Seedwork.Testing.TestConstructor.Abstractions
{
    public abstract class TestCase<T>
    {
        private object[] _parameters;
        private string _failMessage;
        private ConstructorInfo _constructorInfo;

        protected TestCase(ConstructorInfo constructorInfo, object[] parameters, string failMessage)
        {
            _constructorInfo = constructorInfo;
            _parameters = parameters;
            _failMessage = failMessage;
        }

        public abstract string Execute();

        protected T Invoke()
        {
            try
            {
                return (T) _constructorInfo.Invoke(_parameters);
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        protected string Failure(string message)
        {
            return $"Test failed ({_failMessage}): {message}";
        }

        protected string Success()
        {
            return string.Empty;
        }
    }
}
