// Filename: TimelineTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class TimelineTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Act
            var timeline = new Timeline();

            // Assert
            timeline.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 200);
            timeline.PublishedAt.Should().BeNull();
            timeline.RegistrationPeriod.Should().BeNull();
            timeline.ExtensionPeriod.Should().BeNull();
            timeline.StartedAt.Should().BeNull();
            timeline.EndedAt.Should().BeNull();
            timeline.ClosedAt.Should().BeNull();
            timeline.State.Should().Be(ChallengeState.Draft);
        }

        [TestMethod]
        public void PublishedAt_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                TimelinePublishedAt.Min.AddTicks(-1), TimelinePublishedAt.Max.AddTicks(1)
            };

            foreach (var publishedAt in rowData)
            {
                // Arrange
                var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline();

                // Act
                var action = new Action(() => timeline.SetProperty(nameof(Timeline.PublishedAt), publishedAt));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void RegistrationPeriod_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                TimelineRegistrationPeriod.Min - TimeSpan.FromTicks(1),
                TimelineRegistrationPeriod.Max + TimeSpan.FromTicks(1)
            };

            foreach (var registrationPeriod in rowData)
            {
                // Arrange
                var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline();

                // Act
                var action = new Action(() =>
                    timeline.SetProperty(nameof(Timeline.RegistrationPeriod), registrationPeriod));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void ExtensionPeriod_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException()
        {
            var rowData = new[]
            {
                TimelineExtensionPeriod.Min - TimeSpan.FromTicks(1),
                TimelineExtensionPeriod.Max + TimeSpan.FromTicks(1)
            };

            foreach (var extensionPeriod in rowData)
            {
                // Arrange
                var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline();

                // Act
                var action = new Action(() =>
                    timeline.SetProperty(nameof(Timeline.ExtensionPeriod), extensionPeriod));

                // Assert
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TestMethod]
        public void ExtensionPeriod_LessThanThreeTimesRegistrationPeriod_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var extensionPeriod = TimelineExtensionPeriod.Min;
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.Configured);

            // Act
            var action = new Action(() =>
                timeline.SetProperty(nameof(Timeline.ExtensionPeriod), extensionPeriod));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void State_IsDraft_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline();

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.Draft);
        }

        [TestMethod]
        public void State_IsConfigured_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.Configured);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.Configured);
        }

        [TestMethod]
        public void State_IsOpened_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.Opened);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.Opened);
        }

        [TestMethod]
        public void State_IsStarted_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.InProgress);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.InProgress);
        }

        [TestMethod]
        public void State_IsEnded_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.Ended);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.Ended);
        }

        [TestMethod]
        public void State_IsClosed_ShouldBeTrue()
        {
            // Arrange
            var timeline = FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState.Closed);

            // Act
            var state = timeline.State;

            // Assert
            state.Should().Be(ChallengeState.Closed);
        }
    }
}