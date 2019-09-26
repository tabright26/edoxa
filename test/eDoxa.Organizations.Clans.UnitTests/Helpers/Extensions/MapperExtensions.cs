// Filename: MapperExtensions.cs
// Date Created: 2019-07-05
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Reflection;

using AutoMapper;

using eDoxa.Organizations.Clans.Api;

namespace eDoxa.Organizations.Clans.UnitTests.Helpers.Extensions
{
    public static class MapperExtensions
    {
        public static IMapper Mapper
        {
            get
            {
                var configuration = new MapperConfiguration(config => config.AddMaps(Assembly.GetAssembly(typeof(Startup))));

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            }
        }
    }
}
