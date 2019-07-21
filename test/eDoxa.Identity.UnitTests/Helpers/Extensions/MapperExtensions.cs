// Filename: MapperExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Reflection;

using AutoMapper;

using eDoxa.Identity.Api;
using eDoxa.Identity.Api.Infrastructure;

namespace eDoxa.Identity.UnitTests.Helpers.Extensions
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
                        config.AddProfiles(Assembly.GetAssembly(typeof(IdentityDbContext)));
                        config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    }
                );

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            }
        }
    }
}
