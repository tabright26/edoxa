// Filename: MatchFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class MatchFaker : CustomFaker<Match>
    {
        private readonly ScoringFaker _scoringFaker = new ScoringFaker();
        private readonly MatchStatsFaker _matchStatsFaker = new MatchStatsFaker();

        public MatchFaker(Participant participant)
        {
            var game = participant.Challenge.Game;
            
            this.CustomInstantiator(
                faker =>
                {
                    var matchReference = new MatchReference(faker.Random.Guid().ToString().Replace("-", string.Empty));

                    return new Match(participant, matchReference);
                }
            );

            this.RuleFor(match => match.Timestamp, DateTime.UtcNow);

            this.FinishWith(
                (faker, match) =>
                {
                    match.SnapshotStats(_matchStatsFaker.FakeMatchStats(game), _scoringFaker.FakeScoring(game));
                }
            );
        }

        public IEnumerable<Match> FakeMatches(int count)
        {
            var matches = this.Generate(count);

            Console.WriteLine(matches.DumbAsJson());

            return matches;
        }

        public Match FakeMatch()
        {
            var match = this.Generate();

            Console.WriteLine(match.DumbAsJson());

            return match;
        }
    }
}
