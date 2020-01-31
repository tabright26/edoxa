// Filename: DbContextSeeder.cs
// Date Created: 2019-12-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application.SqlServer.Abstractions
{
    public abstract class DbContextSeeder : IDbContextSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        protected DbContextSeeder(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            Logger = logger;
        }

        protected ILogger Logger { get; }

        protected async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _unitOfWork.CommitAsync(false, cancellationToken);
        }

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

            if (_environment.IsEnvironment("Test"))
            {
                await this.SeedTestAsync();
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

        protected virtual async Task SeedTestAsync()
        {
            await Task.CompletedTask;
        }
    }
}
