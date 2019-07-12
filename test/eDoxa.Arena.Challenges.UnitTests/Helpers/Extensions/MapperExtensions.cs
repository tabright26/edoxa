// Filename: MapperExtensions.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Infrastructure;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions
{
    public static class MapperExtensions
    {
        public static IMapper Mapper
        {
            get
            {
                var configuration = new MapperConfiguration(
                    config =>
                    {
                        config.AddProfiles(Assembly.GetAssembly(typeof(ArenaChallengesDbContext)));
                        config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    }
                );

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            }
        }
    }
}
