﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Retro.Module.Team.Infrastructure.Persistence;

#nullable disable

namespace Retro.Module.Team.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(TeamDbContext))]
    [Migration("20240306070709_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Retro.Module.Team.Infrastructure.Persistence.Entities.TeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Retro.Module.Team.Infrastructure.Persistence.Entities.TeamMemberEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("ValidFrom")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ValidTo")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
