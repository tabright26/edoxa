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

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions;

namespace eDoxa.Clans.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static Lazy<IImmutableSet<Clan>> LazyClans =>
            new Lazy<IImmutableSet<Clan>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Clans.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid),
                                Name = default(string),
                                OwnerId = default(Guid)
                            })
                        .Select(
                            record =>
                            {
                                var clan = new Clan(record.Name!, UserId.FromGuid(record.OwnerId!));

                                clan.SetEntityId(record.Id!);

                                return clan;
                            })
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<Clan> Clans => LazyClans.Value;

        public IImmutableSet<Clan> GetClans()
        {
            return Clans;
        }
    }
}
