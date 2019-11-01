// Filename: GamesDbContextModelSnapshot.cs
// Date Created: 2019-10-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Games.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Arena.Games.Api.Infrastructure.Data.Migrations
{
    [DbContext(typeof(GamesDbContext))]
    internal class GamesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate.Credential",
                b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<int>("Game");

                    b.Property<string>("PlayerId").IsRequired();

                    b.Property<long>("Timestamp");

                    b.HasKey("UserId", "Game");

                    b.HasIndex("Game", "PlayerId").IsUnique();

                    b.ToTable("Credential");
                });
#pragma warning restore 612, 618
        }
    }
}
