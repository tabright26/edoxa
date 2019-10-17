// Filename: FileStorage.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure.Extensions;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static readonly Random Random = new Random();

        private static Lazy<IImmutableSet<Role>> LazyRoles =>
            new Lazy<IImmutableSet<Role>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/roles.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid),
                                Name = default(string),
                                NormalizedName = default(string)
                            })
                        .Select(
                            record => new Role
                            {
                                Id = record.Id,
                                Name = record.Name,
                                NormalizedName = record.NormalizedName
                            })
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<RoleClaim>> LazyRoleClaims =>
            new Lazy<IImmutableSet<RoleClaim>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/roles.claims.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(int),
                                ClaimType = default(string),
                                ClaimValue = default(string),
                                RoleId = default(Guid)
                            })
                        .Select(
                            record => new RoleClaim
                            {
                                Id = record.Id,
                                ClaimType = record.ClaimType,
                                ClaimValue = record.ClaimValue,
                                RoleId = record.RoleId
                            })
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<UserClaim>> LazyUserClaims =>
            new Lazy<IImmutableSet<UserClaim>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.claims.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(int),
                                ClaimType = default(string),
                                ClaimValue = default(string),
                                UserId = default(Guid)
                            })
                        .Select(
                            record => new UserClaim
                            {
                                Id = record.Id,
                                ClaimType = record.ClaimType,
                                ClaimValue = record.ClaimValue,
                                UserId = record.UserId
                            })
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<User>> LazyUsers =>
            new Lazy<IImmutableSet<User>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.csv"));

                    using var csvReader = file.OpenCsvReader();

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
                            })
                        .Select(
                            record => new User
                            {
                                Id = record.Id,
                                UserName = record.Email,
                                Country = Country.Canada, // FRANCIS: Should be inside users.csv
                                Email = record.Email,
                                PhoneNumber = record.Phone,
                                DoxatagHistory = new Collection<UserDoxatag>
                                {
                                    new UserDoxatag
                                    {
                                        Id = Guid.NewGuid(),
                                        UserId = record.Id,
                                        Name = record.Doxatag,
                                        Code = Random.Next(100, 10000),
                                        Timestamp = DateTime.UtcNow
                                    }
                                },
                                Informations = new UserInformations
                                {
                                    FirstName = record.FirstName,
                                    LastName = record.LastName,
                                    BirthDate = DateTimeOffset.FromUnixTimeSeconds(record.BirthDate).Date,
                                    Gender = Gender.FromValue(record.Gender)
                                }
                            })
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<UserRole>> LazyUserRoles =>
            new Lazy<IImmutableSet<UserRole>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.roles.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                RoleId = default(Guid),
                                UserId = default(Guid)
                            })
                        .Select(
                            record => new UserRole
                            {
                                RoleId = record.RoleId,
                                UserId = record.UserId
                            })
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<User> Users => LazyUsers.Value;

        public static IImmutableSet<UserClaim> UserClaims => LazyUserClaims.Value;

        public static IImmutableSet<UserRole> UserRoles => LazyUserRoles.Value;

        public static IImmutableSet<Role> Roles => LazyRoles.Value;

        public static IImmutableSet<RoleClaim> RoleClaims => LazyRoleClaims.Value;

        public IImmutableSet<RoleClaim> GetRoleClaims()
        {
            return RoleClaims;
        }

        public IImmutableSet<Role> GetRoles()
        {
            return Roles;
        }

        public IImmutableSet<UserClaim> GetUserClaims()
        {
            return UserClaims;
        }

        public IImmutableSet<User> GetUsers()
        {
            return Users;
        }

        public IImmutableSet<UserRole> GetUserRoles()
        {
            return UserRoles;
        }
    }
}
