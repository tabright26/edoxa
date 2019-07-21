// Filename: AddGameProviderViewModel.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.GameProviders.ViewModels
{
    [DataContract]
    public class AddGameProviderViewModel
    {
        public AddGameProviderViewModel(string playerId)
        {
            PlayerId = playerId;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; private set; }
    }
}
