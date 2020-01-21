// Filename: IdentityDbContext.cs
// Date Created: 2019-11-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure.Configurations;
using eDoxa.Seedwork.Domain;

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using UserClaim = eDoxa.Identity.Domain.AggregateModels.UserAggregate.UserClaim;

namespace eDoxa.Identity.Infrastructure
{
    public sealed class IdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
                                            IPersistedGrantDbContext,
                                            IUnitOfWork
    {
        private readonly IOptions<OperationalStoreOptions> _optionsSnapshot;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IOptionsSnapshot<OperationalStoreOptions> optionsSnapshot) : base(options)
        {
            _optionsSnapshot = optionsSnapshot;
            PersistedGrants = this.Set<PersistedGrant>();
            DeviceFlowCodes = this.Set<DeviceFlowCodes>();
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync()
        {
            return this.SaveChangesAsync();
        }

        public async Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePersistedGrantContext(_optionsSnapshot.Value);

            builder.ApplyConfiguration(new AddressConfiguration());

            builder.ApplyConfiguration(new DoxatagConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new UserClaimConfiguration());

            builder.ApplyConfiguration(new UserLoginConfiguration());

            builder.ApplyConfiguration(new UserTokenConfiguration());

            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new RoleClaimConfiguration());
        }
    }
}
