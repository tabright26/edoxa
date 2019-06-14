// Filename: IdentityDbContextData.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Data.Fakers;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Application.Data
{
    public sealed class IdentityDbContextData : IDbContextData
    {
        private readonly ILogger<IdentityDbContextData> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IdentityDbContext _context;

        public IdentityDbContextData(ILogger<IdentityDbContextData> logger, IHostingEnvironment environment, IdentityDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Roles.Any())
                {
                    var roleFaker = new RoleFaker();

                    var roles = roleFaker.FakeRoles();

                    roles.ForEach(role => _context.Roles.Add(role));

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("The roles being populated:");

                    _logger.LogInformation(roles.DumbAsJson());
                }
                else
                {
                    _logger.LogInformation("The roles already populated.");
                }

                if (!_context.Users.Any())
                {
                    var userFaker = new UserFaker();

                    var adminUser = userFaker.FakeAdminUser();

                    _context.Users.Add(adminUser);

                    var newUsers = userFaker.FakeNewUsers(99);

                    _context.AddRange(newUsers);

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("The users being populated:");

                    _logger.LogInformation(adminUser.DumbAsJson());
                }
                else
                {
                    _logger.LogInformation("The users already populated.");
                }
            }
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Users.RemoveRange(_context.Users);

                await _context.SaveChangesAsync();
            }
        }
    }
}
