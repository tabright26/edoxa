// Filename: DbContextCleaner.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application.SqlServer.Abstractions
{
    public abstract class DbContextCleaner : IDbContextCleaner
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        protected DbContextCleaner(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            Logger = logger;
        }

        protected ILogger Logger { get; }

        async Task IDbContextCleaner.CleanupAsync()
        {
            if (!_environment.IsProduction() && !_environment.IsStaging())
            {
                this.Cleanup();

                await _unitOfWork.CommitAsync(false);
            }
        }

        protected virtual void Cleanup()
        {
        }
    }
}
