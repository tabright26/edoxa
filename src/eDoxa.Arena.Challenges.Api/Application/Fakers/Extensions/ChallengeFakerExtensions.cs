// Filename: ChallengeFakerExtensions.cs
// Date Created: 2019-06-25
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

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions
{
    public static class ChallengeFakerExtensions
    {
        private static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                config =>
                {
                    config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                    config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                }
            )
        );

        public static IReadOnlyCollection<ChallengeViewModel> GenerateViewModels(this ChallengeFaker challengeFaker, int count)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(Mapper.Map<ICollection<ChallengeModel>>(challengeFaker.Generate(count)));
        }

        public static ChallengeViewModel GenerateViewModel(this ChallengeFaker challengeFaker)
        {
            return Mapper.Map<ChallengeViewModel>(Mapper.Map<ChallengeModel>(challengeFaker.Generate()));
        }

        public static ICollection<ChallengeModel> GenerateModels(this ChallengeFaker challengeFaker, int count)
        {
            return Mapper.Map<ICollection<ChallengeModel>>(challengeFaker.Generate(count));
        }

        public static ChallengeModel GenerateModel(this ChallengeFaker challengeFaker)
        {
            return Mapper.Map<ChallengeModel>(challengeFaker.Generate());
        }
    }
}
