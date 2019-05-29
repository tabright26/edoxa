﻿// Filename: DesignTimeDbContextFactory.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Security;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.Factories
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
    {
        protected abstract string BasePath { get; }

        protected abstract Assembly MigrationsAssembly { get; }

        protected DbContextOptions<TContext> Options
        {
            get
            {
                var builder = new DbContextOptionsBuilder<TContext>();

                builder.UseSqlServer(
                    Configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                    options => options.MigrationsAssembly(MigrationsAssembly.GetName().Name)
                );

                return builder.Options;
            }
        }

        private IConfiguration Configuration
        {
            get
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var builder = new ConfigurationBuilder();

                builder.SetBasePath(BasePath);

                builder.AddJsonFile("appsettings.json", false, true);

                builder.AddJsonFile($"appsettings.{environment}.json", true);

                builder.AddEnvironmentVariables();

                return builder.Build();
            }
        }

        [CanBeNull]
        public abstract TContext CreateDbContext(string[] args);

        protected class NoMediator : IMediator
        {
            [NotNull]
            public Task Publish([NotNull] object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            [NotNull]
            public Task Publish<TNotification>([NotNull] TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            [ItemNotNull]
            public async Task<TResponse> Send<TResponse>([NotNull] IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return await Task.FromResult(default(TResponse));
            }
        }
    }
}
