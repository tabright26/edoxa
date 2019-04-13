﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDoxa.Notifications.Infrastructure;

namespace eDoxa.Notifications.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationsDbContext))]
    internal class NotificationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate.Notification", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsRead");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("RedirectUrl");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("eDoxa.Notifications.Domain.AggregateModels.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eDoxa.Seedwork.Infrastructure.Repositories.RequestLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdempotencyKey");

                    b.Property<string>("LocalIpAddress");

                    b.Property<string>("Method");

                    b.Property<string>("Origin");

                    b.Property<string>("RemoteIpAddress");

                    b.Property<DateTime>("Time");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.HasIndex("IdempotencyKey")
                        .IsUnique()
                        .HasFilter("[IdempotencyKey] IS NOT NULL");

                    b.ToTable("RequestLogs","dbo");
                });

            modelBuilder.Entity("eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate.Notification", b =>
                {
                    b.HasOne("eDoxa.Notifications.Domain.AggregateModels.UserAggregate.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
