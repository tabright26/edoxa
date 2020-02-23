// Filename: MvcBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.Json.Extensions;

using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomNewtonsoftJson(this IMvcBuilder builder)
        {
            return builder.AddNewtonsoftJson(options => options.SerializerSettings.IncludeCustomConverters());
        }

        public static IMvcBuilder AddCustomFluentValidation<TStartup>(this IMvcBuilder builder)
        {
            return builder.AddFluentValidation(
                config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<TStartup>();
                    config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
        }
    }
}
