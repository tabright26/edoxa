﻿// Filename: ScoringTypeConverter.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ScoringTypeConverter : ITypeConverter<ICollection<ScoringItemModel>, IScoring>
    {
        [NotNull]
        public IScoring Convert([NotNull] ICollection<ScoringItemModel> source, [NotNull] IScoring destination, [NotNull] ResolutionContext context)
        {
            var scoring = new Scoring();

            source.ForEach(item => scoring.Add(new StatName(item.Name), new StatWeighting(item.Weighting)));

            return scoring;
        }
    }
}