// Filename: IdentityTestFileStorage.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Storage.Azure.File.Abstractions;
using eDoxa.Storage.Azure.File.Extensions;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class IdentityTestFileStorage : IIdentityTestFileStorage
    {
        private static readonly Random Random = new Random();

        private readonly IAzureFileStorage _fileStorage;

        public IdentityTestFileStorage(IAzureFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<IImmutableSet<UserClaim>> GetUserClaimsAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("UserClaims.csv");

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
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("Users.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
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
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("UserRoles.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
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
