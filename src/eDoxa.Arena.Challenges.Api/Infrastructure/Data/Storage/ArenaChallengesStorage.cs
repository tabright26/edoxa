// Filename: StorageAccessor.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;

using CsvHelper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public static class ArenaChallengesStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";

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
    }
}
