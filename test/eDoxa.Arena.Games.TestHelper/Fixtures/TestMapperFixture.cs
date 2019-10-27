// Filename: TestMapperFixture.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Games.Api;
using eDoxa.Arena.Games.Infrastructure;

namespace eDoxa.Arena.Games.TestHelper.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(
            () =>
            {
                var configuration = new MapperConfiguration(
                    config => config.AddMaps(Assembly.GetAssembly(typeof(GamesDbContext)), Assembly.GetAssembly(typeof(Startup))));

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            });

        public IMapper Instance => Lazy.Value;
    }
}
