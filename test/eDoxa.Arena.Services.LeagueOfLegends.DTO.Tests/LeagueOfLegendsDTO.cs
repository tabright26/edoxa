// Filename: LeagueOfLegendsDTO.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace eDoxa.Arena.Services.LeagueOfLegends.DTO.Tests
{
    public static class LeagueOfLegendsDTO
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static string Matches => File.ReadAllText(BaseDirectory + @"\Matches.json");

        public static string MatchReferences => File.ReadAllText(BaseDirectory + @"\MatchReferences.json");

        public static LeagueOfLegendsMatchDTO DeserializeMatch(long gameId)
        {
            var matches = DeserializeMatches();

            return matches[gameId];
        }

        public static IDictionary<long, LeagueOfLegendsMatchDTO> DeserializeMatches()
        {
            return JsonConvert.DeserializeObject<IDictionary<long, LeagueOfLegendsMatchDTO>>(Matches);
        }

        public static LeagueOfLegendsMatchReferenceDTO[] DeserializeMatchReferences()
        {
            return JsonConvert.DeserializeObject<LeagueOfLegendsMatchReferenceDTO[]>(MatchReferences);
        }
    }
}