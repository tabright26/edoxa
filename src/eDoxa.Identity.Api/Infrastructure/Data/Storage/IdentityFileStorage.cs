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

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class IdentityFileStorage : IIdentityFileStorage
    {
        private readonly CloudFileShare _share;

        public IdentityFileStorage()
        {
            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA=="
            );

            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = cloudStorageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("identity");
        }

        public async Task<IImmutableSet<Role>> GetRolesAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var file = rootDirectory.GetFileReference("Roles.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

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
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var file = rootDirectory.GetFileReference("RoleClaims.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

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
