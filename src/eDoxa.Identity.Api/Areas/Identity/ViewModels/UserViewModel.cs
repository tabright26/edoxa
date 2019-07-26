// Filename: UserViewModel.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.ViewModels
{
    [JsonObject]
    public class UserViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("gamertag")]
        public string Gamertag { get; set; }
    }
}
