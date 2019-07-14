// Filename: IdentityDbContextData.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Data.Fakers;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    public sealed class IdentityDbContextData : IDbContextData
    {
        private readonly ILogger<IdentityDbContextData> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IdentityDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public IdentityDbContextData(
            ILogger<IdentityDbContextData> logger,
            IHostingEnvironment environment,
            IdentityDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository
        )
        {
            _logger = logger;
            _environment = environment;
            _context = context;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Roles.Any())
                {
                    var roleFaker = new RoleFaker();

                    var roles = roleFaker.FakeRoles();

                    _roleRepository.Create(roles);

                    await _roleRepository.CommitAsync();

                    _logger.LogInformation("The roles being populated:");
                }
                else
                {
                    _logger.LogInformation("The roles already populated.");
                }

                if (!_context.Users.Any())
                {
                    var userFaker = new UserFaker();

                    userFaker.UseSeed(85963658);

                    var adminUser = userFaker.FakeAdminUser();

                    _userRepository.Create(adminUser);

                    var testUsers = userFaker.FakeTestUsers();

                    _userRepository.Create(testUsers);

                    await _userRepository.CommitAsync();

                    _logger.LogInformation("The users being populated...");
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

                _context.Roles.RemoveRange(_context.Roles);

                await _context.SaveChangesAsync();
            }
        }
    }
}
