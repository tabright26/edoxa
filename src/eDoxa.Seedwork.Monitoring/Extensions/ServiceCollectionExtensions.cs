// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;

using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Seedwork.Monitoring.Extensions
{
    public static class ServiceCollectionExtensions
    {
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

            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    foreach (var name in validationResult.MemberNames)
                    {
                        Log.Error($"{name}:{validationResult.ErrorMessage}");
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
