// Filename: ChallengesDbContextData.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        //private const int DefaultRandomChallengeCount = 5;

        //private static readonly Random Random = new Random();

        private readonly IHostingEnvironment _environment;

        public ChallengesDbContextData(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public Task SeedAsync()
        {
            return Task.CompletedTask;
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
