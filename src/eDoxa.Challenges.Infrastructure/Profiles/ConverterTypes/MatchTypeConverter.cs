// Filename: MatchTypeConverter.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class MatchTypeConverter : ITypeConverter<MatchModel, IMatch>
    {
        public IMatch Convert(MatchModel matchModel, IMatch destination, ResolutionContext context)
        {
            var stats = context.Mapper.Map<ICollection<Stat>>(matchModel.Stats);

            var match = new Match(
                matchModel.GameUuid,
                new DateTimeProvider(matchModel.GameStartedAt),
                TimeSpan.FromSeconds(matchModel.GameDuration),
                stats,
                new DateTimeProvider(matchModel.SynchronizedAt));

            match.SetEntityId(MatchId.FromGuid(matchModel.Id));

            match.ClearDomainEvents();

            return match;
        }
    }
}
