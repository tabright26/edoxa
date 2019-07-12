// Filename: StorageAccessor.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public static class ArenaChallengesStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";
        private const string TestChallengesFilePath = "Infrastructure/Data/Storage/TestFiles/TestChallenges.csv";

        public static IEnumerable<UserId> TestUserIds
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords(
                        new
                        {
                            Id = default(Guid)
                        }
                    );

                    foreach (var record in records)
                    {
                        yield return UserId.FromGuid(record.Id);
                    }
                }
            }
        }

        public static IEnumerable<IChallenge> TestChallenges
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), TestChallengesFilePath);

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords(
                            new
                            {
                                Id = default(Guid),
                                Name = default(string),
                                Game = default(int),
                                Entries = default(int),
                                BestOf = default(int),
                                Duration = default(long)
                            }
                        )
                        .ToList();

                    var scoringFactory = new ScoringFactory();
                    
                    foreach (var record in records)
                    {
                        var name = new ChallengeName(record.Name);

                        var game = ChallengeGame.FromValue(record.Game);

                        var entries = new Entries(record.Entries);

                        var bestOf = new BestOf(record.BestOf);

                        var duration = new ChallengeDuration(TimeSpan.FromTicks(record.Duration));

                        var timeline = new ChallengeTimeline(new UtcNowDateTimeProvider(), duration);

                        var scoringStrategy = scoringFactory.CreateInstance(game);

                        var challenge = new Challenge(name, game, bestOf, entries, timeline, scoringStrategy.Scoring);

                        challenge.SetEntityId(ChallengeId.FromGuid(record.Id));

                        yield return challenge;
                    }
                }
            }
        }
    }
}
