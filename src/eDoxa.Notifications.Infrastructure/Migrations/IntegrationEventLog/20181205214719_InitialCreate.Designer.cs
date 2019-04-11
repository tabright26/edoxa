﻿// <auto-generated />

using System;

using eDoxa.ServiceBus;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eDoxa.Notifications.Infrastructure.Migrations.IntegrationEventLog
{
    [DbContext(typeof(IntegrationEventLogDbContext))]
    [Migration("20181205214719_InitialCreate")]
    internal partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.EventBus.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("JsonObject")
                        .IsRequired();

                    b.Property<int>("PublishAttempted");

                    b.Property<int>("State");

                    b.Property<string>("TypeFullName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("IntegrationEventLogs","dbo");
                });
#pragma warning restore 612, 618
        }
    }
}