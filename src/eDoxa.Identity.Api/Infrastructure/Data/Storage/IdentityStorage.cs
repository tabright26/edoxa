// Filename: IdentityStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public static class IdentityStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";

        public static IReadOnlyCollection<User> TestUsers => Users.OrderBy(user => user.Id).ToList();

        private static IEnumerable<User> Users
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
                        yield return new User
                        {
                            Id = record.Id
                        };
                    }
                }
            }
        }
    }
}
