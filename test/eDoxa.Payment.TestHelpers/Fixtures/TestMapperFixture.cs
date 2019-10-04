// Filename: TestMapperFixture.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using AutoMapper;

using eDoxa.Payment.Api;

namespace eDoxa.Payment.TestHelpers.Fixtures
{
    public sealed class TestMapperFixture
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(
            () =>
            {
                var configuration = new MapperConfiguration(config => config.AddMaps(Assembly.GetAssembly(typeof(Startup))));

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            });

        public IMapper Instance => Lazy.Value;
    }
}
