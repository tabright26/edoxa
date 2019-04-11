﻿// Filename: ChallengeTimelineTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTimelineTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Act
            var timeline = new ChallengeTimeline();

            // Assert
            timeline.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 200);
            timeline.PublishedAt.Should().BeNull();
            timeline.RegistrationPeriod.Should().BeNull();
            timeline.ExtensionPeriod.Should().BeNull();
            timeline.StartedAt.Should().BeNull();
            timeline.EndedAt.Should().BeNull();
            timeline.ClosedAt.Should().BeNull();
            timeline.State.Should().HaveFlag(ChallengeState.Draft);
        }

        [TestMethod]
        public void PublishedAt_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline();

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.PublishedAt), null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void PublishedAt_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                ChallengeTimeline.MinPublishedAt.AddTicks(-1), ChallengeTimeline.MaxPublishedAt.AddTicks(1)
            };

            foreach (var publishedAt in rowData)
            {
                // Arrange
                var timeline = _factory.CreateChallengeTimeline();

                // Act
                var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.PublishedAt), publishedAt));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void RegistrationPeriod_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline();

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.RegistrationPeriod), null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void RegistrationPeriod_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                ChallengeTimeline.MinRegistrationPeriod - TimeSpan.FromTicks(1), ChallengeTimeline.MaxRegistrationPeriod + TimeSpan.FromTicks(1)
            };

            foreach (var registrationPeriod in rowData)
            {
                // Arrange
                var timeline = _factory.CreateChallengeTimeline();

                // Act
                var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.RegistrationPeriod), registrationPeriod));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void ExtensionPeriod_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline();

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.ExtensionPeriod), null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ExtensionPeriod_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                ChallengeTimeline.MinExtensionPeriod - TimeSpan.FromTicks(1), ChallengeTimeline.MaxExtensionPeriod + TimeSpan.FromTicks(1)
            };

            foreach (var extensionPeriod in rowData)
            {
                // Arrange
                var timeline = _factory.CreateChallengeTimeline();

                // Act
                var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.ExtensionPeriod), extensionPeriod));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void ExtensionPeriod_WithNullReferenceRegistrationPeriod_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Configured);
            timeline.SetPrivateField("_registrationPeriod", null);

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.ExtensionPeriod), ChallengeTimeline.MinExtensionPeriod));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void ExtensionPeriod_LessThanThreeTimesRegistrationPeriod_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var extensionPeriod = ChallengeTimeline.MinExtensionPeriod;
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Configured);

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.ExtensionPeriod), extensionPeriod));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void ClosedAt_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline();

            // Act
            var action = new Action(() => timeline.SetProperty(nameof(ChallengeTimeline.ClosedAt), null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void State_IsDraft_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline();

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.Draft);
        }

        [TestMethod]
        public void State_IsConfigured_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Configured);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.Configured);
        }

        [TestMethod]
        public void State_IsOpened_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Opened);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.Opened);
        }

        [TestMethod]
        public void State_IsStarted_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.InProgress);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.InProgress);
        }

        [TestMethod]
        public void State_IsEnded_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Ended);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.Ended);
        }

        [TestMethod]
        public void State_IsClosed_ShouldBeTrue()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.Closed);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().HaveFlag(ChallengeState.Closed);
        }

        [TestMethod]
        public void Close_IsNotEnded_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var timeline = _factory.CreateChallengeTimeline(ChallengeState.InProgress);

            // Act
            var action = new Action(() => timeline.Close());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}