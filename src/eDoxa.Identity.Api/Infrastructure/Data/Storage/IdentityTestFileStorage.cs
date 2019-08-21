// Filename: IdentityTestFileStorage.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;

using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class IdentityTestFileStorage : IIdentityTestFileStorage
    {
        private static readonly Random Random = new Random();

        private readonly CloudFileShare _share;

        public IdentityTestFileStorage()
        {
            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA=="
            );

            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = cloudStorageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("identity");
        }

        public async Task<IImmutableSet<UserClaim>> GetUserClaimsAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("UserClaims.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var stream = await file.OpenReadAsync();

            using var streamReader = new StreamReader(stream);

            using var csvStream = new CsvReader(streamReader);

            return csvStream.GetRecords(
                    new
                    {
                        Id = default(int),
                        ClaimType = default(string),
                        ClaimValue = default(string),
                        UserId = default(Guid)
                    }
                )
                .Select(
                    record => new UserClaim
                    {
                        Id = record.Id,
                        ClaimType = record.ClaimType,
                        ClaimValue = record.ClaimValue,
                        UserId = record.UserId
                    }
                )
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<User>> GetUsersAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("Users.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var stream = await file.OpenReadAsync();

            using var streamReader = new StreamReader(stream);

            using var csvStream = new CsvReader(streamReader);

            return csvStream.GetRecords(
                    new
                    {
                        Id = default(Guid),
                        Doxatag = default(string),
                        FirstName = default(string),
                        LastName = default(string),
                        Email = default(string),
                        Phone = default(string),
                        BirthDate = default(long),
                        Gender = default(int)
                    }
                )
                .Select(
                    record => new User
                    {
                        Id = record.Id,
                        UserName = record.Email,
                        Email = record.Email,
                        PhoneNumber = record.Phone,
                        DoxaTagHistory = new Collection<UserDoxaTag>
                        {
                            new UserDoxaTag
                            {
                                Id = Guid.NewGuid(),
                                UserId = record.Id,
                                Name = record.Doxatag,
                                Code = Random.Next(100, 10000),
                                Timestamp = DateTime.UtcNow
                            }
                        },
                        PersonalInfo = new UserPersonalInfo
                        {
                            FirstName = record.FirstName,
                            LastName = record.LastName,
                            BirthDate = DateTimeOffset.FromUnixTimeSeconds(record.BirthDate).Date,
                            Gender = Gender.FromValue(record.Gender)
                        }
                    }
                )
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<UserRole>> GetUserRolesAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("UserRoles.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var stream = await file.OpenReadAsync();

            using var streamReader = new StreamReader(stream);

            using var csvStream = new CsvReader(streamReader);

            return csvStream.GetRecords(
                    new
                    {
                        RoleId = default(Guid),
                        UserId = default(Guid)
                    }
                )
                .Select(
                    record => new UserRole
                    {
                        RoleId = record.RoleId,
                        UserId = record.UserId
                    }
                )
                .ToImmutableHashSet();
        }
    }
}
