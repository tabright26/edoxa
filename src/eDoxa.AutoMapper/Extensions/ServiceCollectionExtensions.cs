// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.AutoMapper.Factories;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.AutoMapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProfiles(this IServiceCollection services, IMapperFactory factory)
        {
            services.AddAutoMapper(config => factory.CreateConfiguration());
        }
    }
}