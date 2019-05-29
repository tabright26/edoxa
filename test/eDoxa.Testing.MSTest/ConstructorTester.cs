// Filename: ConstructorTester.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace eDoxa.Testing.MSTest
{
    public class ConstructorTester<T> : Tester<T>
    {
        private readonly IList<TestCase<T>> _testCases = new List<TestCase<T>>();

        private ConstructorInfo _info;

        public ConstructorTester(ConstructorInfo info)
        {
            _info = info;
        }

        public override Tester<T> WithName(string name)
        {
            TestCase<T> testCase = new NameTestCase<T>(_info, name);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> WithAttributes(params Type[] attributeTypes)
        {
            TestCase<T> testCase = new WithAttributesTestCase<T>(_info, attributeTypes);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> Fail(object[] args, Type exceptionType, string failMessage)
        {
            TestCase<T> testCase = new FailTestCase<T>(_info, args, exceptionType, failMessage);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> Succeed(object[] args, string failMessage)
        {
            TestCase<T> testCase = new SuccessTestCase<T>(_info, args, failMessage);

            _testCases.Add(testCase);

            return this;
        }

        public override void Assert()
        {
            var errors = new List<string>();

            this.ExecuteTestCases(errors);

            Assert(errors);
        }

        private void ExecuteTestCases(ICollection<string> errors)
        {
            foreach (var testCase in _testCases)
            {
                ExecuteTestCase(errors, testCase);
            }
        }

        private static void ExecuteTestCase(ICollection<string> errors, TestCase<T> testCase)
        {
            var error = testCase.Execute();

            if (!string.IsNullOrEmpty(error))
            {
                errors.Add("    ----> " + error);
            }
        }

        private static void Assert(List<string> errors)
        {
            if (errors.Count <= 0)
            {
                return;
            }

            var error = $"{errors.Count} error(s) occurred:\n{string.Join("\n", errors.ToArray())}";

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(error);
        }
    }
}
