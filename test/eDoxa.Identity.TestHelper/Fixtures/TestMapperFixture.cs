// Filename: TestMapperFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper;

namespace eDoxa.Identity.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static LazyMapper LazyMapper = new LazyMapper(typeof(Startup), typeof(IdentityDbContext));

        public IMapper Instance => LazyMapper.Value;
    }
}
