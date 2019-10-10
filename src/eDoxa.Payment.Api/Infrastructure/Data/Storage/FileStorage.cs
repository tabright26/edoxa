// Filename: FileStorage.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Payment.Domain.Models;
using eDoxa.Seedwork.Infrastructure.Extensions;

namespace eDoxa.Payment.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static Lazy<IImmutableSet<User>> LazyUsers =>
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

        public static IImmutableSet<User> Users => LazyUsers.Value;

        public IImmutableSet<User> GetUsers()
        {
            return Users;
        }
    }
}
