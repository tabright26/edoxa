// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using AutoMapper;

using eDoxa.Seedwork.Application.AutoMapper.TypeConverters;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.AutoMapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services, params Type[] types)
        {
            types.Append(typeof(EnumerableToRepeatedFieldTypeConverter<,>));

            return services.AddAutoMapper(types);
        }
    }
}
