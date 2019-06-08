// Filename: IntegrationEventLogDbContextModelSnapshot.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Identity.Infrastructure.Migrations.IntegrationEventLog
{
    [DbContext(typeof(IntegrationEventLogDbContext))]
    internal class IntegrationEventLogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.EventBus.IntegrationEventLogEntry",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("JsonObject").IsRequired();

                    b.Property<int>("PublishAttempted");

                    b.Property<int>("State");

                    b.Property<string>("TypeFullName").IsRequired();

                    b.HasKey("Id");

                    b.ToTable("IntegrationEventLogs", "dbo");
                }
            );
#pragma warning restore 612, 618
        }
    }
}
