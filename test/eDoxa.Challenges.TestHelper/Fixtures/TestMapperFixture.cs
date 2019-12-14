﻿// Filename: TestMapperFixture.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Api;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper;

namespace eDoxa.Challenges.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static LazyMapper LazyMapper = new LazyMapper(typeof(ChallengesDbContext), typeof(Startup));

        public IMapper Instance => LazyMapper.Value;
    }
}
