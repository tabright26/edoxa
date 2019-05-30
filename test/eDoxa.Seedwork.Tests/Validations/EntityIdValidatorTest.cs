// Filename: EntityIdValidatorTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

using FluentAssertions;

using FluentValidation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Tests.Validations
{
    [TestClass]
    public sealed class EntityIdValidatorTest
    {
        [TestMethod]
        public void Validate_EntityId_ShouldBeTrue()
        {
            // Arrange
            var model = new MockViewModel
            {
                EntityId = new MockEntityId()
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Validate_NullEntityId_ShouldBeFalse()
        {
            // Arrange
            var model = new MockViewModel
            {
                EntityId = null
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
        public void Validate_EmptyEntityId_ShouldBeFalse()
        {
            // Arrange
            var model = new MockViewModel
            {
                EntityId = new MockEmptyEntityId()
            };

            var validator = new MockValidator();

            // Act
            var result = validator.Validate(model);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        private class MockValidator : AbstractValidator<MockViewModel>
        {
            public MockValidator()
            {
                this.EntityId(model => model.EntityId);
            }
        }

        private class MockViewModel
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
    }
}
