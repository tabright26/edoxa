// Filename: IdentityDbContextData.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Infrastructure.Abstractions;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Infrastructure
{
    public sealed class IdentityDbContextData : IDbContextData
    {
        private readonly ILogger<IdentityDbContextData> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IdentityDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public IdentityDbContextData(
            ILogger<IdentityDbContextData> logger,
            IHostingEnvironment environment,
            IdentityDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
        )
        {
            _logger = logger;
            _environment = environment;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                var adminRole = new Role(CustomRoles.Administrator);

                if (!await _roleManager.RoleExistsAsync(adminRole.Name))
                {
                    await _roleManager.CreateAsync(adminRole);

                    await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "*"));
                }

                var challengerRole = new Role("challenger");

                if (!await _roleManager.RoleExistsAsync(challengerRole.Name))
                {
                    await _roleManager.CreateAsync(challengerRole);

                    await _roleManager.AddClaimAsync(challengerRole, new Claim(CustomClaimTypes.Permission, "challenge.read"));

                    await _roleManager.AddClaimAsync(challengerRole, new Claim(CustomClaimTypes.Permission, "challenge.register"));
                }

                if (!_context.Users.Any())
                {
                    var admin = new User("Administrator", "admin@edoxa.gg", new PersonalName("eDoxa", "Admin"), new BirthDate(1970, 1, 1))
                    {
                        Id = Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                        EmailConfirmed = true,
                        PhoneNumber = "0000000000",
                        PhoneNumberConfirmed = true,
                        LockoutEnabled = false
                    };

                    await _userManager.CreateAsync(admin, "Pass@word1");

                    await _userManager.AddToRolesAsync(
                        admin,
                        new List<string>
                        {
                            adminRole.Name,
                            challengerRole.Name
                        }
                    );

                    _logger.LogInformation("The users being populated.");
                }
                else
                {
                    _logger.LogInformation("The users already populated.");
                }
            }
        }
    }
}
