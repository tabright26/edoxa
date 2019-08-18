// Filename: ChallengeViewModelAssertions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions
{
    public class ChallengeViewModelAssertions : ObjectAssertions
    {
        public ChallengeViewModelAssertions(ChallengeViewModel challengeViewModel) : base(challengeViewModel)
        {
        }

        protected override string Identifier => nameof(ChallengeViewModelAssertions);

        private ChallengeViewModel ChallengeViewModel => Subject as ChallengeViewModel;

        public AndConstraint<ChallengeViewModelAssertions> BeValid(string because = "", params object[] becauseArgs)
        {
            ChallengeViewModel.Should().NotBeNull(because, becauseArgs);

            ChallengeViewModel.Id.Should().NotBeEmpty(because, becauseArgs);

            ChallengeViewModel.Name.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeViewModel.Game.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeViewModel.Game.Should().NotBe(new ChallengeGame().Name, because, becauseArgs);

            ChallengeViewModel.Game.Should().NotBe(ChallengeGame.All.Name, because, becauseArgs);

            ChallengeViewModel.State.Should().NotBeNullOrWhiteSpace(because, becauseArgs);

            ChallengeViewModel.State.Should().NotBe(new ChallengeState().Name, because, becauseArgs);

            ChallengeViewModel.State.Should().NotBe(ChallengeState.All.Name, because, becauseArgs);

            ChallengeViewModel.Should().NotBeNull(because, becauseArgs);

            ChallengeViewModel.Timeline.Should().NotBeNull(because, becauseArgs);

            ChallengeViewModel.Timeline.CreatedAt.Should().BeBefore(DateTime.UtcNow, because, becauseArgs);

            ChallengeViewModel.Scoring.Should().NotBeNull(because, becauseArgs);

            ChallengeViewModel.Scoring.Should().NotBeEmpty(because, becauseArgs);

            foreach (var participantViewModel in ChallengeViewModel.Participants)
            {
                participantViewModel.Id.Should().NotBeEmpty(because, becauseArgs);

                participantViewModel.UserId.Should().NotBeEmpty(because, becauseArgs);

                foreach (var matchViewModel in participantViewModel.Matches)
                {
                    matchViewModel.Id.Should().NotBeEmpty(because, becauseArgs);

                    matchViewModel.Score.Should().BeGreaterOrEqualTo(decimal.Zero, because, becauseArgs);

                    foreach (var statViewModel in matchViewModel.Stats)
                    {
                        statViewModel.Name.Should().NotBeNullOrWhiteSpace(because, becauseArgs);
                    }
                }
            }

            return new AndConstraint<ChallengeViewModelAssertions>(this);
        }
    }
}
