// Filename: Claim.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Security
{
    [JsonObject]
    public sealed class Claim
    {
        [JsonConstructor]
        public Claim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        [JsonProperty]
        public string Type { get; }

        [JsonProperty]
        public string Value { get; }
    }
}
