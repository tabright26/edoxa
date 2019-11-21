// Filename: DbContextSeeder.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application
{
    public abstract class DbContextSeeder : IDbContextSeeder
    {
        private readonly IWebHostEnvironment _environment;

        protected DbContextSeeder(IWebHostEnvironment environment, ILogger logger)
        {
            _environment = environment;
            Logger = logger;
        }

        protected ILogger Logger { get; }

        async Task IDbContextSeeder.SeedAsync()
        {
            await this.SeedAsync();

            if (_environment.IsDevelopment())
            {
                await this.SeedDevelopmentAsync();
            }

            if (_environment.IsStaging())
            {
                await this.SeedStagingAsync();
            }

            if (_environment.IsProduction())
            {
                await this.SeedProductionAsync();
            }
        }

        protected virtual async Task SeedAsync()
        {
            await Task.CompletedTask;
        }

        protected virtual async Task SeedDevelopmentAsync()
        {
            await Task.CompletedTask;
        }

        protected virtual async Task SeedStagingAsync()
        {
            await Task.CompletedTask;
        }

        protected virtual async Task SeedProductionAsync()
        {
            await Task.CompletedTask;
        }
    }
}
