// Filename: ChallengeFakerExtensions.cs
// Date Created: 2019-06-21
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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.Extensions
{
    public static class ChallengeFakerExtensions
    {
        private static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                config =>
                {
                    config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                }
            )
        );

        public static IEnumerable<IChallenge> GenerateEntities(this ChallengeFaker challengeFaker, int count)
        {
            return Mapper.Map<IEnumerable<IChallenge>>(challengeFaker.Generate(count));
        }

        public static IChallenge GenerateEntity(this ChallengeFaker challengeFaker)
        {
            return Mapper.Map<IChallenge>(challengeFaker.Generate());
        }

        public static IReadOnlyCollection<ChallengeViewModel> GenerateViewModels(this ChallengeFaker challengeFaker, int count)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challengeFaker.Generate(count));
        }

        public static ChallengeViewModel GenerateViewModel(this ChallengeFaker challengeFaker)
        {
            return Mapper.Map<ChallengeViewModel>(challengeFaker.Generate());
        }
    }
}
