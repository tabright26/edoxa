﻿// Filename: EnumerationValidatorTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using FluentValidation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Validations
{
    [TestClass]
    public sealed class EnumerationValidatorTest
    {
        [TestMethod]
        public void Validate_Enumeration_ShouldBeTrue()
        {
            // Arrange
            var response = new MockResponse
            {
                Enumeration = MockEnumeration.MockEnumeration1
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(response);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Validate_AllEnumeration_ShouldBeFalse()
        {
            // Arrange
            var response = new MockResponse
            {
                Enumeration = MockEnumeration.All
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(response);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
        public void Validate_NoneEnumeration_ShouldBeFalse()
        {
            // Arrange
            var response = new MockResponse
            {
                Enumeration = new MockEnumeration()
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(response);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
        public void Validate_NullEnumeration_ShouldBeFalse()
        {
            // Arrange
            var response = new MockResponse
            {
                Enumeration = null
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(response);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        private class MockValidator : AbstractValidator<MockResponse>
        {
            public MockValidator()
            {
                this.Enumeration(model => model.Enumeration);
            }
        }

        private class MockResponse
        {
            public MockEnumeration Enumeration { get; set; }
        }

        private class MockEnumeration : Enumeration<MockEnumeration>
        {
            public static readonly MockEnumeration MockEnumeration1 = new MockEnumeration(1, nameof(MockEnumeration1));

            public MockEnumeration()
            {
            }

            private MockEnumeration(int value, string name) : base(value, name)
            {
            }
        }
    }
}
