// Filename: IdentityStorage.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public static class IdentityStorage
    {
        private const string RolesFilePath = "Infrastructure/Data/Storage/SourceFiles/Roles.csv";
        private const string RoleClaimsFilePath = "Infrastructure/Data/Storage/SourceFiles/RoleClaims.csv";
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";
        private const string TestUserClaimsFilePath = "Infrastructure/Data/Storage/TestFiles/TestUserClaims.csv";
        private const string TestUserRolesFilePath = "Infrastructure/Data/Storage/TestFiles/TestUserRoles.csv";

        public static IReadOnlyCollection<Role> Roles => GetRoles().ToList();

        public static IReadOnlyCollection<RoleClaim> RoleClaims => GetRoleClaims().ToList();

        public static User TestAdmin => GetTestUsers().First();

        public static IReadOnlyCollection<User> TestUsers => GetTestUsers().ToList();

        public static IReadOnlyCollection<UserClaim> TestUserClaims => GetTestUserClaims().ToList();

        public static IReadOnlyCollection<UserRole> TestUserRoles => GetTestUserRoles().ToList();

        private static IEnumerable<UserClaim> GetTestUserClaims()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUserClaimsFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                new
                {
                    Id = default(int),
                    ClaimType = default(string),
                    ClaimValue = default(string),
                    UserId = default(Guid)
                }
            );

            foreach (var record in records)
            {
                yield return new UserClaim
                {
                    Id = record.Id,
                    ClaimType = record.ClaimType,
                    ClaimValue = record.ClaimValue,
                    UserId = record.UserId
                };
            }
        }

        private static IEnumerable<Role> GetRoles()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), RolesFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                new
                {
                    Id = default(Guid),
                    Name = default(string),
                    NormalizedName = default(string)
                }
            );

            foreach (var record in records)
            {
                yield return new Role
                {
                    Id = record.Id,
                    Name = record.Name,
                    NormalizedName = record.NormalizedName
                };
            }
        }

        private static IEnumerable<RoleClaim> GetRoleClaims()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), RoleClaimsFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                new
                {
                    Id = default(int),
                    ClaimType = default(string),
                    ClaimValue = default(string),
                    RoleId = default(Guid)
                }
            );

            foreach (var record in records)
            {
                yield return new RoleClaim
                {
                    Id = record.Id,
                    ClaimType = record.ClaimType,
                    ClaimValue = record.ClaimValue,
                    RoleId = record.RoleId
                };
            }
        }

        private static IEnumerable<User> GetTestUsers()
        {
            var random = new Random();

            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
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
            );

            foreach (var record in records)
            {
                yield return new User
                {
                    Id = record.Id,
                    UserName = record.Email,
                    Email = record.Email,
                    PhoneNumber = record.Phone,
                    Doxatag = new Doxatag
                    {
                        Name = record.Doxatag,
                        UniqueTag = random.Next(100, 10000)
                    },
                    Profile = new Profile
                    {
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        BirthDate = DateTimeOffset.FromUnixTimeSeconds(record.BirthDate).Date,
                        Gender = Gender.FromValue(record.Gender)
                    }
                };
            }
        }

        private static IEnumerable<UserRole> GetTestUserRoles()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUserRolesFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                new
                {
                    RoleId = default(Guid),
                    UserId = default(Guid)
                }
            );

            foreach (var record in records)
            {
                yield return new UserRole
                {
                    RoleId = record.RoleId,
                    UserId = record.UserId
                };
            }
        }
    }
}
