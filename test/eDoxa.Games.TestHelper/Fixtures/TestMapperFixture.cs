// Filename: TestMapperFixture.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Games.Api;
using eDoxa.Games.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper;

namespace eDoxa.Games.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static LazyMapper LazyMapper = new LazyMapper(typeof(GamesDbContext), typeof(Startup));

        public IMapper Instance => LazyMapper.Value;
    }
}
