﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AdoptPets.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdoptPets.Infrastructure.Migrations
{
    [DbContext(typeof(AdoptPetsContext))]
    partial class AdoptPetsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AdoptPets.Domain.Entities.Animal", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AnimalAge")
                        .HasColumnType("integer");

                    b.Property<string>("AnimalBreed")
                        .HasColumnType("text");

                    b.Property<string>("AnimalDescription")
                        .HasColumnType("text");

                    b.Property<string>("AnimalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AnimalSex")
                        .HasColumnType("text");

                    b.Property<string>("AnimalType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("PersonalityTraits")
                        .HasColumnType("text[]");

                    b.HasKey("AnimalId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Announcement", b =>
                {
                    b.Property<Guid>("AnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AnnouncementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AnnouncementDescription")
                        .HasColumnType("text");

                    b.Property<string>("AnnouncementTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AnnouncementId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Deworming", b =>
                {
                    b.Property<Guid>("DewormingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DewormingType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("DewormingId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Dewormings");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.MedicalHistory", b =>
                {
                    b.Property<Guid>("MedicalHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("MedicalHistoryId");

                    b.HasIndex("AnimalId")
                        .IsUnique();

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Observation", b =>
                {
                    b.Property<Guid>("ObservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ObservationDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ObservationId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Observations");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Vaccination", b =>
                {
                    b.Property<Guid>("VaccinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VaccineName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VaccinationId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Deworming", b =>
                {
                    b.HasOne("AdoptPets.Domain.Entities.Animal", "Animal")
                        .WithMany("Dewormings")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.MedicalHistory", b =>
                {
                    b.HasOne("AdoptPets.Domain.Entities.Animal", "Animal")
                        .WithOne("MedicalHistory")
                        .HasForeignKey("AdoptPets.Domain.Entities.MedicalHistory", "AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Observation", b =>
                {
                    b.HasOne("AdoptPets.Domain.Entities.Animal", "Animal")
                        .WithMany("Observations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Vaccination", b =>
                {
                    b.HasOne("AdoptPets.Domain.Entities.Animal", "Animal")
                        .WithMany("Vaccinations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("AdoptPets.Domain.Entities.Animal", b =>
                {
                    b.Navigation("Dewormings");

                    b.Navigation("MedicalHistory")
                        .IsRequired();

                    b.Navigation("Observations");

                    b.Navigation("Vaccinations");
                });
#pragma warning restore 612, 618
        }
    }
}
