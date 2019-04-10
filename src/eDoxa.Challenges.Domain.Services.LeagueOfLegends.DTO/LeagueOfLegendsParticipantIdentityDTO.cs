﻿// Filename: LeagueOfLegendsParticipantIdentityDTO.cs
// Date Created: 2019-03-25
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO
{
    public class LeagueOfLegendsParticipantIdentityDTO
    {
        [JsonProperty("player")]
        public LeagueOfLegendsPlayerDTO Player { get; set; }

        [JsonProperty("participantId")]
        public long ParticipantId { get; set; }
    }
}