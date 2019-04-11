﻿// Filename: PersistedGrantDbContextModelSnapshot.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using IdentityServer4.EntityFramework.DbContexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Identity.Infrastructure.Migrations.PresistedGrant
{
    [DbContext(typeof(PersistedGrantDbContext))]
    internal class PersistedGrantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasDefaultSchema("idsrv")
                        .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                        .HasAnnotation("Relational:MaxIdentifierLength", 128)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "IdentityServer4.EntityFramework.Entities.DeviceFlowCodes",
                b =>
                {
                    b.Property<string>("UserCode").ValueGeneratedOnAdd().HasMaxLength(200);

                    b.Property<string>("ClientId").IsRequired().HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data").IsRequired().HasMaxLength(50000);

                    b.Property<string>("DeviceCode").IsRequired().HasMaxLength(200);

                    b.Property<DateTime?>("Expiration").IsRequired();

                    b.Property<string>("SubjectId").HasMaxLength(200);

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode").IsUnique();

                    b.ToTable("DeviceCodes");
                }
            );

            modelBuilder.Entity(
                "IdentityServer4.EntityFramework.Entities.PersistedGrant",
                b =>
                {
                    b.Property<string>("Key").HasMaxLength(200);

                    b.Property<string>("ClientId").IsRequired().HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data").IsRequired().HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<string>("SubjectId").HasMaxLength(200);

                    b.Property<string>("Type").IsRequired().HasMaxLength(50);

                    b.HasKey("Key");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("PersistedGrants");
                }
            );
#pragma warning restore 612, 618
        }
    }
}