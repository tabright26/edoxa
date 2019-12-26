// Filename: FileStorage.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions;

namespace eDoxa.Notifications.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static Lazy<IImmutableSet<User>> LazyUsers =>
            new Lazy<IImmutableSet<User>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(string),
                                Email = default(string)
                            })
                        .Select(record => new User(record.Id!.ParseEntityId<UserId>(), record.Email!))
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<User> Users => LazyUsers.Value;

        public IImmutableSet<User> GetUsers()
        {
            return Users;
        }
    }
}
