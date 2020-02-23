// Filename: StreamExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Globalization;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

namespace eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions
{
    public static class StreamExtensions
    {
        public static CsvReader OpenCsvReader(this Stream stream)
        {
            return new CsvReader(
                new StreamReader(stream),
                new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ","
                });
        }
    }
}
