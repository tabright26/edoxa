﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDoxa.Clans.Infrastructure;

namespace eDoxa.Clans.Api.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ClansDbContext))]
    [Migration("20191103211251_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Clans.Domain.Models.Candidature", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ClanId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Candidature");
                });

            modelBuilder.Entity("eDoxa.Clans.Domain.Models.Clan", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.ToTable("Clan");
                });

            modelBuilder.Entity("eDoxa.Clans.Domain.Models.Invitation", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ClanId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("eDoxa.Clans.Domain.Models.Member", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ClanId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ClanId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("eDoxa.Clans.Domain.Models.Member", b =>
                {
                    b.HasOne("eDoxa.Clans.Domain.Models.Clan")
                        .WithMany("Members")
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
