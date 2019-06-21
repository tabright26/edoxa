// Filename: MapperBuilder.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Infrastructure;

namespace eDoxa.Arena.Challenges.UnitTests.Utilities
{
    // TODO: To refactor and update to version 8.
    public static class MapperBuilder
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(
                config =>
                {
                    config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                }
            );

            configuration.AssertConfigurationIsValid();

            return new Mapper(configuration);
        }
    }
}
