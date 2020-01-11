// Filename: MetadataExtensions.cs
// Date Created: 2020-01-09
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;

using Grpc.Core;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Grpc.Extensions
{
    public static class MetadataExtensions
    {
        public static IDictionary<string, string[]> GetInvalidArgumentErrors(this Metadata metadata)
        {
            return metadata.GetErrors()
                .Where(error => error.Key != DomainValidationError.FailedPreconditionPropertyName)
                .ToDictionary(error => error.Key, error => error.Value);
        }

        public static IDictionary<string, string[]> GetFailedPreconditionErrors(this Metadata metadata)
        {
            return metadata.GetErrors()
                .Where(error => error.Key == DomainValidationError.FailedPreconditionPropertyName)
                .ToDictionary(error => error.Key, error => error.Value);
        }

        private static IDictionary<string, string[]> GetErrors(this Metadata metadata)
        {
            var trailers = metadata.ToDictionary(entry => entry.Key, entry => entry.Value);

            var errors = trailers[ServerCallContextExtensions.Errors];

            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(errors);
        }
    }
}
