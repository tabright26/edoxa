// Filename: MvcBuilderExtensions.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomNewtonsoftJson(this IMvcBuilder builder)
        {
            return builder.AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.Converters.Add(new DecimalValueConverter());
                });
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
