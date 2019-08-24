// Filename: AddGameRequest.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class GamePostRequest
    {
        public GamePostRequest(string playerId)
        {
            PlayerId = playerId;
        }

#nullable disable
        public GamePostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "playerId")]
        public string PlayerId { get; private set; }
    }
}
