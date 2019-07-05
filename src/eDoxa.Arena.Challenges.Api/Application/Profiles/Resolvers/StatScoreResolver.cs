﻿// Filename: StatScoreResolver.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles.Resolvers
{
    internal sealed class StatScoreResolver : IValueResolver<StatModel, StatViewModel, decimal>
    {
        public decimal Resolve(
            [NotNull] StatModel statModel,
            [NotNull] StatViewModel statViewModel,
            decimal score,
            [NotNull] ResolutionContext context
        )
        {
            return Convert.ToDecimal(statModel.Value * statModel.Weighting);
        }
    }
}