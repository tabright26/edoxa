// Filename: Claim.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [JsonObject]
    public sealed class Claim : ValueObject
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

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Value;
        }

        public override string ToString()
        {
            return $"{Type}={Value}";
        }
    }
}
