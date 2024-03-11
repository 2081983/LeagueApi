﻿// <auto-generated />
using System;
using LeagueApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeagueApi.Migrations
{
    [DbContext(typeof(AppDb))]
    [Migration("20240310131907_add-points")]
    partial class addpoints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LeagueApi.Model.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LgId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LgId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("LeagueApi.Model.League", b =>
                {
                    b.Property<int>("Lid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Lid"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("state")
                        .HasColumnType("tinyint");

                    b.HasKey("Lid");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Lid = 1,
                            Name = "First League",
                            state = (byte)0
                        });
                });

            modelBuilder.Entity("LeagueApi.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId1")
                        .HasColumnType("int");

                    b.Property<int>("TeamId2")
                        .HasColumnType("int");

                    b.Property<byte>("state")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeamId1");

                    b.HasIndex("TeamId2");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("LeagueApi.Model.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<byte>("ResultType")
                        .HasColumnType("tinyint");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("LeagueApi.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LgId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LgId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LeagueApi.Model.Group", b =>
                {
                    b.HasOne("LeagueApi.Model.League", "League")
                        .WithMany()
                        .HasForeignKey("LgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("LeagueApi.Model.Match", b =>
                {
                    b.HasOne("LeagueApi.Model.Group", "group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeagueApi.Model.Team", "fteam")
                        .WithMany()
                        .HasForeignKey("TeamId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeagueApi.Model.Team", "steam")
                        .WithMany()
                        .HasForeignKey("TeamId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("fteam");

                    b.Navigation("group");

                    b.Navigation("steam");
                });

            modelBuilder.Entity("LeagueApi.Model.Result", b =>
                {
                    b.HasOne("LeagueApi.Model.Match", "Matche")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeagueApi.Model.Team", "team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matche");

                    b.Navigation("team");
                });

            modelBuilder.Entity("LeagueApi.Model.Team", b =>
                {
                    b.HasOne("LeagueApi.Model.League", "League")
                        .WithMany()
                        .HasForeignKey("LgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });
#pragma warning restore 612, 618
        }
    }
}
