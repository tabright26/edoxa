// Filename: IdentityStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Identity.Api.Models;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public static class IdentityStorage
    {
        private const string RolesFilePath = "Infrastructure/Data/Storage/SourceFiles/Roles.csv";
        private const string RoleClaimsFilePath = "Infrastructure/Data/Storage/SourceFiles/RoleClaims.csv";
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";
        private const string TestUserClaimsFilePath = "Infrastructure/Data/Storage/TestFiles/TestUserClaims.csv";
        private const string TestUserRolesFilePath = "Infrastructure/Data/Storage/TestFiles/TestUserRoles.csv";

        public static IReadOnlyCollection<RoleModel> Roles => GetRoles().ToList();

        public static IReadOnlyCollection<RoleClaimModel> RoleClaims => GetRoleClaims().ToList();

        public static UserModel TestAdmin => GetTestUsers().First();

        public static IReadOnlyCollection<UserModel> TestUsers => GetTestUsers().ToList();

        public static IReadOnlyCollection<UserClaimModel> TestUserClaims => GetTestUserClaims().ToList();

        public static IReadOnlyCollection<UserRoleModel> TestUserRoles => GetTestUserRoles().ToList();

        private static IEnumerable<UserClaimModel> GetTestUserClaims()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUserClaimsFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
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
                    yield return new UserClaimModel
                    {
                        Id = record.Id,
                        ClaimType = record.ClaimType,
                        ClaimValue = record.ClaimValue,
                        UserId = record.UserId
                    };
                }
            }
        }

        private static IEnumerable<RoleModel> GetRoles()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), RolesFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
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
                    yield return new RoleModel
                    {
                        Id = record.Id,
                        Name = record.Name,
                        NormalizedName = record.NormalizedName
                    };
                }
            }
        }

        private static IEnumerable<RoleClaimModel> GetRoleClaims()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), RoleClaimsFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
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
                    yield return new RoleClaimModel
                    {
                        Id = record.Id,
                        ClaimType = record.ClaimType,
                        ClaimValue = record.ClaimValue,
                        RoleId = record.RoleId
                    };
                }
            }
        }

        private static IEnumerable<UserModel> GetTestUsers()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords(
                    new
                    {
                        Id = default(Guid),
                        Gamertag = default(string),
                        FirstName = default(string),
                        LastName = default(string),
                        Email = default(string),
                        Phone = default(string),
                        BirthDate = default(long)
                    }
                );

                foreach (var record in records)
                {
                    yield return new UserModel
                    {
                        Id = record.Id,
                        UserName = record.Gamertag,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        Email = record.Email,
                        PhoneNumber = record.Phone,
                        BirthDate = DateTimeOffset.FromUnixTimeSeconds(record.BirthDate).Date
                    };
                }
            }
        }

        private static IEnumerable<UserRoleModel> GetTestUserRoles()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUserRolesFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords(
                    new
                    {
                        RoleId = default(Guid),
                        UserId = default(Guid)
                    }
                );

                foreach (var record in records)
                {
                    yield return new UserRoleModel
                    {
                        RoleId = record.RoleId,
                        UserId = record.UserId
                    };
                }
            }
        }
    }
}
