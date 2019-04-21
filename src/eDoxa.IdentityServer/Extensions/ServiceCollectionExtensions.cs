﻿// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, string audience)
        {
            var authority = configuration.GetValue<string>("IdentityServer:Url");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = audience;
                options.Authority = authority;
                options.RequireHttpsMetadata = false;
            });
        }
    }
}