// Filename: NotificationDescriptionTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.NotificationAggregate
{
    [TestClass]
    public sealed class NotificationDescriptionTest
    {
        private readonly NotificationAggregateFactory _factory = NotificationAggregateFactory.Instance;

        [DataRow("user.email.updated", "User email updated", "This string contains no arguments.")]
        [DataTestMethod]
        public void Constructor_ShouldNotThrowException(string name, string title, string template)
        {
            // Act
            var action = new Action(() => _factory.CreateDescription(name, title, template));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [DataRow("  ")]
        [DataRow(null)]
        [DataTestMethod]
        public void Constructor_Title_ShouldThrowArgumentException(string name)
        {
            // Act
            var action = new Action(
                () => _factory.CreateDescription(
                    name,
                    NotificationAggregateFactory.NotificationDescriptionTitle,
                    NotificationAggregateFactory.NotificationDescriptionTemplate
                )
            );

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow("  ")]
        [DataRow(null)]
        [DataTestMethod]
        public void Constructor_Template_ShouldThrowArgumentException(string title)
        {
            // Act
            var action = new Action(
                () => _factory.CreateDescription(
                    NotificationAggregateFactory.NotificationDescriptionName,
                    title,
                    NotificationAggregateFactory.NotificationDescriptionTemplate
                )
            );

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow("  ")]
        [DataRow(null)]
        [DataTestMethod]
        public void Constructor_Name_ShouldThrowArgumentException(string template)
        {
            // Act
            var action = new Action(
                () => _factory.CreateDescription(
                    NotificationAggregateFactory.NotificationDescriptionName,
                    NotificationAggregateFactory.NotificationDescriptionTitle,
                    template
                )
            );

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(null, "This string contains no arguments.")]
        [DataRow(
            new string[]
            {
            },
            "This string contains no arguments."
        )]
        [DataTestMethod]
        public void FormatMessage_WithoutMetadata_ShouldBeTemplate(string[] arguments, string template)
        {
            // Arrange
            var description = new NotificationDescription(
                NotificationAggregateFactory.NotificationDescriptionName,
                NotificationAggregateFactory.NotificationDescriptionTitle,
                template
            );

            // Act
            var message = description.FormatMessage(_factory.CreateMetadata(arguments));

            // Assert
            message.Should().Be(template);
        }

        [DataRow(
            new[]
            {
                "value1"
            },
            "This string contains the following values: {0}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2"
            },
            "This string contains the following values: {0} and {1}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3"
            },
            "This string contains the following values: {0}, {1} and {2}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3", "value4"
            },
            "This string contains the following values: {0}, {1}, {2} and {3}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3", "value4", "value5"
            },
            "This string contains the following values: {0}, {1}, {2}, {3} and {4}."
        )]
        [DataTestMethod]
        public void FormatMessage_WithMetadata_ShouldContainAllArguments(string[] arguments, string template)
        {
            // Arrange
            var description = new NotificationDescription(
                NotificationAggregateFactory.NotificationName,
                NotificationAggregateFactory.NotificationDescriptionTitle,
                template
            );

            // Act
            var message = description.FormatMessage(_factory.CreateMetadata(arguments));

            // Assert
            message.Should().ContainAll(arguments);
        }

        [DataRow(
            new[]
            {
                "value1"
            },
            "This string contains the following values: {0} and {1}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2"
            },
            "This string contains the following values: {0}, {1} and {2}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3"
            },
            "This string contains the following values: {0}, {1}, {2} and {3}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3", "value4"
            },
            "This string contains the following values: {0}, {1}, {2}, {3} and {4}."
        )]
        [DataRow(
            new[]
            {
                "value1", "value2", "value3", "value4", "value5"
            },
            "This string contains the following values: {0}, {1}, {2}, {3}, {4} and {5}."
        )]
        [DataTestMethod]
        public void FormatMessage_WithMetadata_ShouldThrowFormatException(string[] arguments, string template)
        {
            // Arrange
            var description = new NotificationDescription(
                NotificationAggregateFactory.NotificationDescriptionName,
                NotificationAggregateFactory.NotificationDescriptionTitle,
                template
            );

            // Act
            var action = new Action(() => description.FormatMessage(_factory.CreateMetadata(arguments)));

            // Assert
            action.Should().Throw<FormatException>();
        }
    }
}