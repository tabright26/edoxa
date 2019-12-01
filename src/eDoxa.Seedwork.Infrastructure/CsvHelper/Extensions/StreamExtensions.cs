// Filename: CsvReaderExtensions.cs
// Date Created: 2019-08-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using CsvHelper;

namespace eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions
{
    public static class StreamExtensions
    {
        public static CsvReader OpenCsvReader(this Stream stream)
        {
            var streamReader = new StreamReader(stream);

            var csvReader = new CsvReader(streamReader);

            csvReader.Configuration.Delimiter = ",";

            return csvReader;
        }
    }
}
