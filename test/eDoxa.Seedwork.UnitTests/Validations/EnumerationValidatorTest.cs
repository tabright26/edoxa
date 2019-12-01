// Filename: EnumerationValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.FluentValidation.Extensions;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using FluentValidation;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Validations
{
    public sealed class EnumerationValidatorTest
    {
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

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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
    }
}
