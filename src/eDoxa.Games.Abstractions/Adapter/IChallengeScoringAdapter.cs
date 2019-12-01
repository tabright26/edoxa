// Filename: IChallengeScoringAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Abstractions.Adapter
{
    public interface IChallengeScoringAdapter
    {
        Game Game { get; }

        Task<ScoringDto> GetScoringAsync();
    }
}
