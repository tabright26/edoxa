// Filename: TestMapperFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper;

namespace eDoxa.Cashier.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static LazyMapper LazyMapper = new LazyMapper(typeof(CashierDbContext), typeof(Startup));

        public IMapper Instance => LazyMapper.Value;
    }
}
