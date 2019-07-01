// Filename: MatchScoreResolver.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles.Resolvers
{
    internal sealed class MatchScoreResolver : IValueResolver<MatchModel, MatchViewModel, decimal>
    {
        public decimal Resolve(
            [NotNull] MatchModel matchModel,
            [NotNull] MatchViewModel matchViewModel,
            decimal totalScore,
            [NotNull] ResolutionContext context
        )
        {
            return matchModel.Stats.Sum(stat => context.Mapper.Map<StatViewModel>(stat).Score);
        }
    }
}
