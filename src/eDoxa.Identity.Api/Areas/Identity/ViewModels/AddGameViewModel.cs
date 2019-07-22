// Filename: AddGameViewModel.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.ViewModels
{
    [DataContract]
    public class AddGameViewModel
    {
        public AddGameViewModel(string playerId)
        {
            PlayerId = playerId;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; private set; }
    }
}
