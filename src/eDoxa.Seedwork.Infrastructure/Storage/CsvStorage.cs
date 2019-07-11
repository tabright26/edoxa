// Filename: CsvStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.IO;

using CsvHelper;

using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Seedwork.Infrastructure.Storage
{
    public static class CsvStorage
    {
        public static readonly TestUserDataSets TestUsers = new TestUserDataSets();

        public sealed class TestUserDataSets : Collection<Guid>
        {
            private const string FileName = "TestUsers.csv";

            internal TestUserDataSets()
            {
                using (var streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), $"Storage/Csv/{FileName}")))
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.GetRecords<TestUser>().ForEach(testUser => this.Add(testUser.Id));
                }
            }

            private sealed class TestUser
            {
                public Guid Id { get; set; }
            }
        }
    }
}
