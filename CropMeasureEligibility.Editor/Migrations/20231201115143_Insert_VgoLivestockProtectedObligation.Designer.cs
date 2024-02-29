﻿// <auto-generated />
using System;
using CropMeasureEligibility.Editor.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    [DbContext(typeof(EditorDbContext))]
    [Migration("20231201115143_Insert_VgoLivestockProtectedObligation")]
    partial class Insert_VgoLivestockProtectedObligation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.ActionContextIdIdentifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActionContextId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ActionContextIdIdentifiers");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.FarmDestinationCropMeasuresEligibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActionContextId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CropMeasureEligibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FarmDestinationCropMeasuresEligibilityes");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.FarmIdBarcodeId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FarmId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FarmIdBarcodeIds");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.FarmSourceCropMeasuresEligibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActionContextId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CropMeasureEligibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FarmSourceCropMeasuresEligibilityes");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.ListD.ActionContextLivestockRequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ActionContextId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActionContextYear")
                        .HasColumnType("int");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<string>("LivestockRequestItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LivestockRequestItemsAfterUpdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestDocumentTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ActionContextLivestockRequestItems");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.ListD.DcanimalBreed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AnimalTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsProtected")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DcanimalBreeds");
                });

            modelBuilder.Entity("CropMeasureEligibility.Editor.Models.ListD.VgoLivestockProtectedObligation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AnimalBreedId")
                        .HasColumnType("int");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeactivated")
                        .HasColumnType("datetime2");

                    b.Property<int>("EndYear")
                        .HasColumnType("int");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<int>("FirstYear")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UgQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("YearsInCommitment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("VgoLivestockProtectedObligations");
                });
#pragma warning restore 612, 618
        }
    }
}
