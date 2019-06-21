// Filename: ChallengeFakerExtensions.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions
{
    public static class ChallengeFakerExtensions
    {
        public static IEnumerable<IChallenge> GenerateEntities(this ChallengeFaker challengeFaker, IMapper mapper, int count)
        {
            return mapper.Map<IEnumerable<IChallenge>>(challengeFaker.Generate(count));
        }

        public static IChallenge GenerateEntity(this ChallengeFaker challengeFaker, IMapper mapper)
        {
            return mapper.Map<IChallenge>(challengeFaker.Generate());
        }
    }
}
