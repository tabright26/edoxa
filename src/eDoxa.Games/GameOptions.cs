// Filename: GameOptions.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Games
{
    [JsonObject]
    public abstract class GameOptions
    {
        protected GameOptions()
        {
            Displayed = false;
            Verified = false;
            Services = new Dictionary<string, bool>
            {
                ["challenge"] = false,
                ["manager"] = false,
                ["tournament"] = false
            };
            Scoring = new Dictionary<string, float>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayed")]
        public bool Displayed { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("services")]
        public IDictionary<string, bool> Services { get; set; }

        [JsonProperty("scoring")]
        public IDictionary<string, float> Scoring { get; set; }

        [JsonIgnore]
        public string ApiKey { get; set; }
    }
}
