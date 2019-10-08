﻿// Filename: FileStorage.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure.Extensions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public class FileStorage
    {
        public static IImmutableSet<User> Users => UsersLazy.Value;

        public static IImmutableSet<IChallenge> Challenges => ChallengesLazy.Value;

        private static Lazy<IImmutableSet<User>> UsersLazy =>
            new Lazy<IImmutableSet<User>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid)
                            })
                        .Select(record => new User(UserId.FromGuid(record.Id)))
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<IChallenge>> ChallengesLazy =>
            new Lazy<IImmutableSet<IChallenge>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/challenges.csv"));

                    using var csvReader = file.OpenCsvReader();

                    var challengeFaker = new ChallengeFaker();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid),
                                Name = default(string),
                                Game = default(int),
                                Entries = default(int),
                                BestOf = default(int),
                                Duration = default(long),
                                State = default(int)
                            })
                        .Select(
                            record => challengeFaker.FakeChallenge(
                                new ChallengeModel
                                {
                                    Name = record.Name,
                                    Game = record.Game,
                                    Entries = record.Entries,
                                    BestOf = record.BestOf,
                                    Timeline = new ChallengeTimelineModel
                                    {
                                        Duration = record.Duration
                                    },
                                    State = record.State,
                                    Id = record.Id
                                }))
                        .ToImmutableHashSet();
                });

        public IImmutableSet<User> GetUsers()
        {
            return Users;
        }

        public IImmutableSet<IChallenge> GetChallenges()
        {
            return Challenges;
        }
    }
}