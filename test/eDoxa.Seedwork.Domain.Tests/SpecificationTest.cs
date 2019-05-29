// Filename: SpecificationTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Specifications;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests
{
    [TestClass]
    public sealed class SpecificationTest
    {
        [TestMethod]
        public void IsSatisfiedBy_Entity_ShouldBeTrue()
        {
            // Arrange
            var entity = new MockEntity();
            var specification = new MockSpecification();

            // Act
            var isSatisfiedBy = specification.IsSatisfiedBy(entity);

            // Assert
            isSatisfiedBy.Should().BeTrue();
        }

        [TestMethod]
        public void Not_IsNotSatisfiedByEntity_ShouldBeFalse()
        {
            // Arrange
            var entity = new MockEntity();
            var specification = new MockSpecification();

            // Act
            var notSpecification = specification.Not();

            // Assert
            notSpecification.IsSatisfiedBy(entity).Should().BeFalse();
        }

        [TestMethod]
        public void And_IsSatisfiedByEntity_ShouldBeTrue()
        {
            // Arrange
            var entity = new MockEntity();
            var specification1 = new MockSpecification();
            var specification2 = new MockSpecification();

            // Act
            var andSpecification = specification1.And(specification2);

            // Assert

            andSpecification.IsSatisfiedBy(entity).Should().BeTrue();
        }

        [TestMethod]
        public void And_IsNotSatisfiedByEntity_ShouldBeFalse()
        {
            // Arrange
            var entity = new MockEntity();
            var specification1 = new MockSpecification();
            var specification2 = new MockSpecification().Not();

            // Act
            var andSpecification = specification1.And(specification2);

            // Assert
            andSpecification.IsSatisfiedBy(entity).Should().BeFalse();
        }

        [TestMethod]
        public void Or_IsSatisfiedByEntity_ShouldBeTrue()
        {
            // Arrange
            var entity = new MockEntity();
            var specification1 = new MockSpecification();
            var specification2 = new MockSpecification().Not();

            // Act
            var orSpecification = specification1.Or(specification2);

            // Assert
            orSpecification.IsSatisfiedBy(entity).Should().BeTrue();
        }

        [TestMethod]
        public void Or_IsNotSatisfiedByEntity_ShouldBeFalse()
        {
            // Arrange
            var entity = new MockEntity();
            var specification1 = new MockSpecification().Not();
            var specification2 = new MockSpecification().Not();

            // Act
            var orSpecification = specification1.Or(specification2);

            // Assert
            orSpecification.IsSatisfiedBy(entity).Should().BeFalse();
        }

        private sealed class MockEntityId : EntityId<MockEntityId>
        {
        }

        private sealed class MockEntity : Entity<MockEntityId>, IAggregateRoot
        {
        }

        private sealed class MockSpecification : Specification<MockEntity>
        {
            public override Expression<Func<MockEntity, bool>> ToExpression()
            {
                return _ => true;
            }
        }
    }
}
