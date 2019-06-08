// Filename: ConstructorTester.cs
// Date Created: 2019-06-08
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

using eDoxa.Seedwork.Testing.TestConstructor.Abstractions;
using eDoxa.Seedwork.Testing.TestConstructor.TestCases;

namespace eDoxa.Seedwork.Testing.TestConstructor.Testers
{
    public sealed class ConstructorTester<T> : Tester<T>
    {
        private readonly IList<TestCase<T>> _testCases = new List<TestCase<T>>();

        private ConstructorInfo _constructorInfo;

        public ConstructorTester(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo;
        }

        public override Tester<T> WithClassName(string className)
        {
            TestCase<T> testCase = new ClassNameTestCase<T>(_constructorInfo, className);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> WithClassAttributes(params Type[] classAttributeTypes)
        {
            TestCase<T> testCase = new AttributesTestCase<T>(_constructorInfo, classAttributeTypes);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> Failure(object[] parameters, Type exceptionType, string failureMessage)
        {
            TestCase<T> testCase = new FailureTestCase<T>(_constructorInfo, parameters, exceptionType, failureMessage);

            _testCases.Add(testCase);

            return this;
        }

        public override Tester<T> Success(object[] parameters, string failureMessage)
        {
            TestCase<T> testCase = new SuccessTestCase<T>(_constructorInfo, parameters, failureMessage);

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
