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
            Services = new Dictionary<string, bool>
            {
                ["challenge"] = false,
                ["manager"] = false,
                ["tournament"] = false
            };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayed")]
        public bool Displayed { get; set; }

        [JsonProperty("services")]
        public IDictionary<string, bool> Services { get; set; }

        [JsonIgnore]
        public string ApiKey { get; set; }
    }
}
