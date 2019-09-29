// Filename: TestMapperFixture.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Infrastructure;

namespace eDoxa.Arena.Challenges.TestHelpers
{
    public sealed class TestMapperFixture
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(
            () =>
            {
                var configuration = new MapperConfiguration(
                    config => config.AddMaps(Assembly.GetAssembly(typeof(ArenaChallengesDbContext)), Assembly.GetAssembly(typeof(Startup))));

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            });

        public IMapper Instance => Lazy.Value;
    }
}
