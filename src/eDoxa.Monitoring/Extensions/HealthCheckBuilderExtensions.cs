// Filename: HealthCheckBuilderExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;

namespace eDoxa.Monitoring.Extensions
{
    public static class HealthCheckBuilderExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration, string databaseName)
        {
            services.AddHealthChecks(
                builder =>
                {
                    var timeSpan = TimeSpan.FromMinutes(1);

                    if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutes))
                    {
                        timeSpan = TimeSpan.FromMinutes(minutes);
                    }

                    builder.AddSqlCheck(databaseName, configuration.GetConnectionString("SqlServer"), timeSpan);
                }
            );
        }

        public static HealthCheckBuilder AddSqlCheck(this HealthCheckBuilder builder, string name, string connectionString)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return AddSqlCheck(builder, name, connectionString, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddSqlCheck(this HealthCheckBuilder builder, string name, string connectionString, TimeSpan cacheDuration)
        {
            builder.AddCheck(
                $"SqlCheck({name})",
                async () =>
                {
                    try
                    {
                        //TODO: There is probably a much better way to do this.
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.Text;
                                command.CommandText = "SELECT 1";
                                var result = (int) await command.ExecuteScalarAsync().ConfigureAwait(false);

                                if (result == 1)
                                {
                                    return HealthCheckResult.Healthy($"SqlCheck({name}): Healthy");
                                }

                                return HealthCheckResult.Unhealthy($"SqlCheck({name}): Unhealthy");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return HealthCheckResult.Unhealthy($"SqlCheck({name}): Exception during check: {ex.GetType().FullName}");
                    }
                },
                cacheDuration
            );

            return builder;
        }
    }
}