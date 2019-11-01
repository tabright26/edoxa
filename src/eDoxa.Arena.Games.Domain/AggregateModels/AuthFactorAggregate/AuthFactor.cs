// Filename: AuthFactor.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate
{
    [JsonObject]
    public class AuthFactor
    {
        [JsonConstructor]
        protected AuthFactor(PlayerId playerId, object key)
        {
            PlayerId = playerId;
            Key = key;
        }

        [JsonProperty]
        public PlayerId PlayerId { get; }

        [JsonProperty]
        public object Key { get; }
    }
}
