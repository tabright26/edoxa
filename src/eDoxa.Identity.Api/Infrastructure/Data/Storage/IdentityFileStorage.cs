// Filename: IdentityFileStorage.cs
// Date Created: 2019-08-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Storage.Azure.File.Abstractions;
using eDoxa.Storage.Azure.File.Extensions;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class IdentityFileStorage : IIdentityFileStorage
    {
        private readonly IAzureFileStorage _fileStorage;

        public IdentityFileStorage(IAzureFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<IImmutableSet<Role>> GetRolesAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var file = await root.GetFileAsync("Roles.csv");
            
            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(Guid),
                        Name = default(string),
                        NormalizedName = default(string)
                    }
                )
                .Select(
                    record => new Role
                    {
                        Id = record.Id,
                        Name = record.Name,
                        NormalizedName = record.NormalizedName
                    }
                )
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<RoleClaim>> GetRoleClaimsAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var file = await root.GetFileAsync("RoleClaims.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(int),
                        ClaimType = default(string),
                        ClaimValue = default(string),
                        RoleId = default(Guid)
                    }
                )
                .Select(
                    record => new RoleClaim
                    {
                        Id = record.Id,
                        ClaimType = record.ClaimType,
                        ClaimValue = record.ClaimValue,
                        RoleId = record.RoleId
                    }
                )
                .ToImmutableHashSet();
        }
    }
}
