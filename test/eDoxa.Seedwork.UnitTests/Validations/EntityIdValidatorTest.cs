// Filename: EntityIdValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using FluentValidation;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Validations
{
    public sealed class EntityIdValidatorTest
    {
        private class MockValidator : AbstractValidator<MockResponse>
        {
            public MockValidator()
            {
                this.EntityId(model => model.EntityId);
            }
        }

        private class MockResponse
        {
            public MockEntityId EntityId { get; set; }
        }

        private class MockEntityId : EntityId<MockEntityId>
        {
        }

        private class MockEmptyEntityId : MockEntityId
        {
            public MockEmptyEntityId()
            {
                Value = Guid.Empty;
            }
        }

        [Fact]
        public void Validate_EmptyEntityId_ShouldBeFalse()
        {
            // Arrange
            var response = new MockResponse
            {
                EntityId = new MockEmptyEntityId()
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(response);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void Validate_EntityId_ShouldBeTrue()
        {
            // Arrange
            var model = new MockResponse
            {
                EntityId = new MockEntityId()
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_NullEntityId_ShouldBeFalse()
        {
            // Arrange
            var response = new MockResponse
            {
                EntityId = null
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
