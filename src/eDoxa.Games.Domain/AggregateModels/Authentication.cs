// Filename: Authentication.cs
// Date Created: 2019-11-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;

using Newtonsoft.Json;

namespace eDoxa.Games.Domain.AggregateModels
{
    [JsonObject]
    public class Authentication<TFactor> : Authentication
    where TFactor : class, IAuthenticationFactor
    {
        [JsonConstructor]
        public Authentication(PlayerId playerId, TFactor factor) : base(playerId, factor)
        {
            Factor = factor;
        }

        [JsonProperty("factor")]
        public new TFactor Factor { get; private set; }
    }

    [JsonObject]
    public class Authentication
    {
        [JsonConstructor]
        public Authentication(PlayerId playerId, object factor)
        {
            PlayerId = playerId;
            Factor = factor;
        }

        [JsonProperty("playerId")]
        public PlayerId PlayerId { get; private set; }

        [JsonProperty("factor")]
        public object Factor { get; private set; }
    }
}
