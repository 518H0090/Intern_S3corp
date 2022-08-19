﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWebAPiWithXUnitTest.Models.DatabaseContext;

#nullable disable

namespace TestWebAPiWithXUnitTest.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220819090528_InitialValue")]
    partial class InitialValue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestWebAPiWithXUnitTest.Models.DatabaseModels.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CharacterJob")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CharacterJob");

                    b.Property<int>("CharacterLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("CharacterLevel");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CharacterName");

                    b.HasKey("Id");

                    b.HasIndex("CharacterName")
                        .IsUnique();

                    b.ToTable("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
