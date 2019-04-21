// Filename: TestCase.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

namespace eDoxa.Testing.MSTest
{
    public abstract class TestCase<T>
    {
        private object[] _arguments;
        private string _failMessage;
        private ConstructorInfo _info;

        protected TestCase(ConstructorInfo info, object[] args, string failMessage)
        {
            _info = info;
            _arguments = args;
            _failMessage = failMessage;
        }

        public abstract string Execute();

        protected T InvokeConstructor()
        {
            try
            {
                return (T) _info.Invoke(_arguments);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        protected string Fail(string message)
        {
            return $"Test failed ({_failMessage}): {message}";
        }

        protected string Success()
        {
            return string.Empty;
        }
    }
}