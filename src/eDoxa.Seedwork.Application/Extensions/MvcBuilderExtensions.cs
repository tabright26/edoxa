// Filename: MvcBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.Json.Extensions;

using FluentValidation.AspNetCore;

using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;
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

        public static TAppSettings GetAppSettings<TAppSettings>(this IConfiguration configuration, ApiResource? apiResource = null)
        where TAppSettings : class
        {
            var appSettings = configuration.Get<TAppSettings>();

            if (appSettings is IHasApiResourceAppSettings settings)
            {
                settings.ApiResource = apiResource ?? throw new ArgumentNullException(nameof(apiResource));
            }

            if (!appSettings.IsValid())
            {
                throw new Exception("The appsettings.json file configuration failed!");
            }

            return appSettings;
        }

        public static bool IsValid<T>(this T obj)
        where T : class
        {
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                obj,
                new ValidationContext(obj),
                validationResults,
                true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    foreach (var name in validationResult.MemberNames)
                    {
                        Console.WriteLine($"{name}:{validationResult.ErrorMessage}");
                    }
                }
            }

            return isValid;
        }

        public static void AddAppSettings<TAppSettings>(this IServiceCollection services, IConfiguration configuration)
        where TAppSettings : class
        {
            services.Configure<TAppSettings>(configuration);
        }
    }
}
