// Filename: DataResources.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using CsvHelper;

using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Seedwork.Common
{
    public static class DataResources
    {
        public static readonly IEnumerable<Guid> TestUserIds = new DataTestUserIds();

        public sealed class DataTestUserIds : Collection<Guid>
        {
            private const string FileName = "TestUserIds.csv";

            internal DataTestUserIds()
            {
                using (var streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), $"Resources/{FileName}")))
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
