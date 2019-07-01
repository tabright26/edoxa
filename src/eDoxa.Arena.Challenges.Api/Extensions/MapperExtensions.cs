// Filename: MapperExtensions.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class MapperExtensions
    {
        public static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                config =>
                {
                    config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                    config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                }
            )
        );

        public static IReadOnlyCollection<ChallengeModel> ToModels(this IEnumerable<Challenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static ChallengeModel ToModel(this Challenge challenge)
        {
            return Mapper.Map<ChallengeModel>(challenge);
        }

        public static IReadOnlyCollection<ChallengeViewModel> ToViewModels(this IEnumerable<Challenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges.ToModels());
        }

        public static ChallengeViewModel ToViewModel(this Challenge challenge)
        {
            return Mapper.Map<ChallengeViewModel>(challenge.ToModel());
        }
    }
}
