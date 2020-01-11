// Filename: DomainValidationError.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Domain
{
    [JsonObject]
    public sealed class DomainValidationError
    {
        public const string DebugPropertyName = "_debug";
        public const string FailedPreconditionPropertyName = "_error";

        [JsonConstructor]
        public DomainValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        [JsonProperty]
        public string PropertyName { get; }

        [JsonProperty]
        public string ErrorMessage { get; }
    }
}
