// Filename: ChallengesDbContextData.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        private readonly ChallengesDbContext _context;

        private readonly IHostingEnvironment _environment;

        private static readonly Random Random = new Random();

        public ChallengesDbContextData(IHostingEnvironment environment, ChallengesDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        public IMatchStats CreateMatchStats(MatchExternalId matchExternalId = null)
        {
            matchExternalId = matchExternalId ?? new MatchExternalId(2233345251);

            return new MatchStats(
                matchExternalId,
                new
                {
                    Kills = Random.Next(0, 40 + 1),
                    Deaths = Random.Next(0, 15 + 1),
                    Assists = Random.Next(0, 50 + 1),
                    TotalDamageDealtToChampions = Random.Next(10000, 500000 + 1),
                    TotalHeal = Random.Next(10000, 350000 + 1)
                }
            );
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    var challenges = new List<Challenge>
                    {
                        CreateChallengeType1(),
                        CreateChallengeType2(),
                        CreateChallengeType3(),
                        CreateChallengeType4(),
                        CreateChallengeType5(),
                        CreateChallengeType6()
                    };

                    foreach (var challenge in challenges)
                    {
                        for (var index = 0; index < challenge.Setup.Entries; index++)
                        {
                            var userId = new UserId();

                            challenge.RegisterParticipant(userId, new ParticipantExternalAccount(Guid.NewGuid()));

                            var participantId = challenge.Participants.Single(participant => participant.UserId == userId).Id;

                            for (var i = 0; i < Random.Next(1, challenge.Setup.BestOf + Random.Next(0, challenge.Setup.BestOf + 1) + 1); i++)
                            {
                                challenge.SnapshotParticipantMatch(participantId, this.CreateMatchStats());
                            }
                        }
                    }

                    _context.Challenges.AddRange(challenges);

                    await _context.CommitAsync();
                }
            }
        }

        public static Challenge CreateChallengeType1()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(10M), 1);

            payout.AddBucket(new Prize(7.5M), 1);

            payout.AddBucket(new Prize(2.5M), 1);

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type1"),
                new ChallengeSetup(
                    new BestOf(1),
                    new Entries(10),
                    new EntryFee(2.5M),
                    new PayoutRatio(0.3F),
                    new ServiceChargeRatio(0.2F)
                ),
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        public static Challenge CreateChallengeType2()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(14M), 1);

            payout.AddBucket(new Prize(8M), 2);

            payout.AddBucket(new Prize(5M), 3);

            payout.AddBucket(new Prize(2.5M), 6);

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type2"),
                new ChallengeSetup(
                    new BestOf(3),
                    new Entries(30),
                    new EntryFee(2.5M),
                    new PayoutRatio(0.4F),
                    new ServiceChargeRatio(0.2F)
                ),
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        public static Challenge CreateChallengeType3()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(20M), 1);

            payout.AddBucket(new Prize(15M), 1);

            payout.AddBucket(new Prize(10M), 2);

            payout.AddBucket(new Prize(7M), 4);

            payout.AddBucket(new Prize(5M), 7);

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type3"),
                new ChallengeSetup(
                    new BestOf(3),
                    new Entries(30),
                    new EntryFee(5M),
                    new PayoutRatio(0.5F),
                    new ServiceChargeRatio(0.2F)
                ),
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        public static Challenge CreateChallengeType4()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(40M), 1);

            payout.AddBucket(new Prize(30M), 1);

            payout.AddBucket(new Prize(20M), 2);

            payout.AddBucket(new Prize(14M), 4);

            payout.AddBucket(new Prize(10M), 7);

            var setup = new ChallengeSetup(
                new BestOf(3),
                new Entries(30),
                new EntryFee(10M),
                new PayoutRatio(0.5F),
                new ServiceChargeRatio(0.2F)
            );

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type4"),
                setup,
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        public static Challenge CreateChallengeType5()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(25M), 1);

            payout.AddBucket(new Prize(20M), 1);

            payout.AddBucket(new Prize(12.5M), 2);

            payout.AddBucket(new Prize(10M), 3);

            payout.AddBucket(new Prize(7M), 5);

            payout.AddBucket(new Prize(5M), 13);

            var setup = new ChallengeSetup(
                new BestOf(3),
                new Entries(50),
                new EntryFee(5M),
                new PayoutRatio(0.5F),
                new ServiceChargeRatio(0.2F)
            );

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type5"),
                setup,
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        public static Challenge CreateChallengeType6()
        {
            var payout = new Payout();

            payout.AddBucket(new Prize(50M), 1);

            payout.AddBucket(new Prize(40M), 1);

            payout.AddBucket(new Prize(25M), 2);

            payout.AddBucket(new Prize(20M), 3);

            payout.AddBucket(new Prize(14M), 5);

            payout.AddBucket(new Prize(10M), 13);

            var setup = new ChallengeSetup(
                new BestOf(3),
                new Entries(50),
                new EntryFee(10M),
                new PayoutRatio(0.5F),
                new ServiceChargeRatio(0.2F)
            );

            return new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type6"),
                setup,
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );
        }

        //public IReadOnlyCollection<Challenge> CreateRandomChallenges(ChallengeState state = null)
        //{
        //    state = state ?? ChallengeState.Opened;

        //    var challenges = new Collection<Challenge>();

        //    for (var row = 0; row < DefaultRandomChallengeCount; row++)
        //    {
        //        challenges.Add(this.CreateRandomChallengeFromState(state));
        //    }

        //    return challenges;
        //}

        //public Challenge CreateRandomChallenge(ChallengeState state = null)
        //{
        //    state = state ?? ChallengeState.Opened;

        //    return this.CreateRandomChallenges(state).First();
        //}

        //public IReadOnlyCollection<Challenge> CreateRandomChallengesWithOtherStates(ChallengeState state)
        //{
        //    return this.CreateRandomChallenges(state).Union(this.CreateOtherRandomChallenges(state)).ToList();
        //}

        //private Challenge CreateRandomChallengeFromState(ChallengeState state)
        //{
        //    var challenge = FakeChallengeFactory.CreateChallenge(state);

        //    if (state.Value >= ChallengeState.Opened.Value)
        //    {
        //        var timeline = challenge.Timeline;

        //        challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
        //            ?.SetValue(challenge, FakeChallengeFactory.CreateChallengeTimeline(ChallengeState.Opened));

        //        for (var row = 0; row < Random.Next(1, challenge.Setup.Entries + 1); row++)
        //        {
        //            var userId = new UserId();

        //            challenge.RegisterParticipant(userId, new ParticipantExternalAccount(Guid.NewGuid()));
        //        }

        //        challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
        //            ?.SetValue(challenge, FakeChallengeFactory.CreateChallengeTimeline(ChallengeState.InProgress));

        //        foreach (var participant in challenge.Participants)
        //        {
        //            for (var index = 0; index < Random.Next(1, challenge.Setup.BestOf + Random.Next(0, challenge.Setup.BestOf + 1) + 1); index++)
        //            {
        //                var stats = this.CreateMatchStats();

        //                challenge.SnapshotParticipantMatch(participant.Id, stats);
        //            }
        //        }

        //        challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);
        //    }

        //    return challenge;
        //}

        //private IReadOnlyCollection<Challenge> CreateOtherRandomChallenges(ChallengeState state)
        //{
        //    var challenges = new Collection<Challenge>();

        //    foreach (var otherState in OtherStates(state))
        //    {
        //        for (var row = 0; row < DefaultRandomChallengeCount; row++)
        //        {
        //            challenges.Add(this.CreateRandomChallengeFromState(otherState));
        //        }
        //    }

        //    return challenges;
        //}

        //private static IEnumerable<ChallengeState> OtherStates(ChallengeState state)
        //{
        //    var states = ChallengeState.GetAll().ToList();

        //    states.Remove(state);

        //    return states;
        //}

        //public IMatchStats CreateMatchStats(MatchExternalId matchExternalId = null)
        //{
        //    matchExternalId = matchExternalId ?? new MatchExternalId(2233345251);

        //    return new MatchStats(
        //        matchExternalId,
        //        new
        //        {
        //            Kills = Random.Next(0, 40 + 1),
        //            Deaths = Random.Next(0, 15 + 1),
        //            Assists = Random.Next(0, 50 + 1),
        //            TotalDamageDealtToChampions = Random.Next(10000, 500000 + 1),
        //            TotalHeal = Random.Next(10000, 350000 + 1)
        //        }
        //    );
        //}
    }
}
