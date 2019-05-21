// Filename: IMatchV4Service.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Abstractions
{
    public interface IMatchV4Service
    {
        Task<LeagueOfLegendsMatchReferenceDTO[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime);

        Task<LeagueOfLegendsMatchDTO> GetMatchAsync(string gameId);
    }
}
