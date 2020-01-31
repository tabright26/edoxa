// Filename: IdentityDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextCleaner : DbContextCleaner
    {
        public IdentityDbContextCleaner(IdentityDbContext context, IWebHostEnvironment environment, ILogger<IdentityDbContextCleaner> logger) : base(
            context,
            environment,
            logger)
        {
            Users = context.Set<User>();
            Doxatags = context.Set<Doxatag>();
            Addresses = context.Set<Address>();
            Roles = context.Set<Role>();
        }

        private DbSet<User> Users { get; }

        private DbSet<Doxatag> Doxatags { get; }

        private DbSet<Address> Addresses { get; }

        private DbSet<Role> Roles { get; }

        protected override void Cleanup()
        {
            Users.RemoveRange(Users);

            Roles.RemoveRange(Roles);

            Addresses.RemoveRange(Addresses);

            Doxatags.RemoveRange(Doxatags);
        }
    }
}
