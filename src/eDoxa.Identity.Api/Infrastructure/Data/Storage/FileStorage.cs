﻿// Filename: FileStorage.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public sealed class FileStorage
    {
        private static readonly Random Random = new Random();

        private static Lazy<IImmutableSet<Role>> LazyRoles =>
            new Lazy<IImmutableSet<Role>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Roles.csv"));

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
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/RoleClaims.csv"));

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
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/UserClaims.csv"));

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

        private static Lazy<IImmutableSet<Doxatag>> LazyDoxatags =>
            new Lazy<IImmutableSet<Doxatag>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Users.csv"));

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
                            record => new Doxatag(
                                UserId.FromGuid(record.Id),
                                record.Doxatag!,
                                Random.Next(100, 10000),
                                new UtcNowDateTimeProvider()))
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<User>> LazyUsers =>
            new Lazy<IImmutableSet<User>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Users.csv"));

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
                                Gender = default(int),
                                Country = default(string)
                            })
                        .Select(
                            record => new User
                            {
                                Id = record.Id,
                                Country = Country.FromName(record.Country),
                                Dob = new UserDob(DateTimeOffset.FromUnixTimeSeconds(record.BirthDate).Date),
                                Email = record.Email,
                                PhoneNumber = record.Phone,
                                SecurityStamp = Guid.NewGuid().ToString("N"),
                                Profile = new UserProfile(record.FirstName, record.LastName, Gender.FromValue(record.Gender))
                            })
                        .ToImmutableHashSet();
                });

        private static Lazy<IImmutableSet<UserRole>> LazyUserRoles =>
            new Lazy<IImmutableSet<UserRole>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/UserRoles.csv"));

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

        public static IImmutableSet<Doxatag> Doxatags => LazyDoxatags.Value;

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

        public IImmutableSet<Doxatag> GetDoxatags()
        {
            return Doxatags;
        }
    }
}
