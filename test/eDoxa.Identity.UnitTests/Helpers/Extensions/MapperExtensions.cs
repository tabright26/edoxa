// Filename: MapperExtensions.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Reflection;

using AutoMapper;

using eDoxa.Identity.Api;

namespace eDoxa.Identity.UnitTests.Helpers.Extensions
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
