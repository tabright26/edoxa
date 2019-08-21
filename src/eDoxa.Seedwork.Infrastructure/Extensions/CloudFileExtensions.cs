// Filename: CsvReaderExtensions.cs
// Date Created: 2019-08-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Threading.Tasks;

using CsvHelper;

using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class CloudFileExtensions
    {
        public static async Task<CsvReader> OpenCsvReaderAsync(this CloudFile cloudFile)
        {
            var stream = await cloudFile.OpenReadAsync();

            var streamReader = new StreamReader(stream);

            var csvReader = new CsvReader(streamReader);

            csvReader.Configuration.Delimiter = ",";

            return csvReader;
        }
    }
}
