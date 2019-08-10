// Filename: AddGameRequest.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class AddGameRequest
    {
        public AddGameRequest(string playerId)
        {
            PlayerId = playerId;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; private set; }
    }
}
