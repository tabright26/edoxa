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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class MapperExtensions
    {
        // TODO: Must be refactor.
        public static IMapper Mapper
        {
            get
            {
                var configuration = new MapperConfiguration(
                    config =>
                    {
                        config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                        config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    }
                );

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            }
        }

        public static IReadOnlyCollection<IChallenge> ToEntities(this IEnumerable<ChallengeModel> challengeModels)
        {
            return Mapper.Map<IReadOnlyCollection<IChallenge>>(challengeModels);
        }

        public static IChallenge ToEntity(this ChallengeModel challengeModel)
        {
            return Mapper.Map<IChallenge>(challengeModel);
        }

        public static IReadOnlyCollection<ChallengeModel> ToModels(this IEnumerable<IChallenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static ChallengeModel ToModel(this IChallenge challenge)
        {
            return Mapper.Map<ChallengeModel>(challenge);
        }

        public static IReadOnlyCollection<ChallengeViewModel> ToViewModels(this IEnumerable<IChallenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges.ToModels());
        }

        public static ChallengeViewModel ToViewModel(this IChallenge challenge)
        {
            return Mapper.Map<ChallengeViewModel>(challenge.ToModel());
        }

        public static IReadOnlyCollection<ChallengeViewModel> ToViewModels(this IEnumerable<ChallengeModel> challengeModels)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challengeModels);
        }

        public static ChallengeViewModel ToViewModel(this ChallengeModel challengeModel)
        {
            return Mapper.Map<ChallengeViewModel>(challengeModel);
        }
    }
}
