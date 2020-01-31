// Filename: TestMapperFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Clans.Api;
using eDoxa.Seedwork.Application.AutoMapper;

namespace eDoxa.Clans.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static LazyMapper LazyMapper = new LazyMapper(typeof(Startup));

        public IMapper Instance => LazyMapper.Value;
    }
}
