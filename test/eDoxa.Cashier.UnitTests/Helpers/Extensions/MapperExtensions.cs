// Filename: MapperExtensions.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using AutoMapper;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;

namespace eDoxa.Cashier.UnitTests.Helpers.Extensions
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
                        config.AddProfiles(Assembly.GetAssembly(typeof(CashierDbContext)));
                        config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                    }
                );

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            }
        }
    }
}
