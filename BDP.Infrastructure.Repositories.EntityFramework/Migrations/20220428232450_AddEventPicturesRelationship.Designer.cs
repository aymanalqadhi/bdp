// <auto-generated />
using System;
using BDP.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    [DbContext(typeof(BdpDbContext))]
    [Migration("20220428232450_AddEventPicturesRelationship")]
    partial class AddEventPicturesRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BDP.Domain.Entities.Attachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("EventId")
                        .HasColumnType("bigint");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("SellableId")
                        .HasColumnType("bigint");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SellableId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Confirmation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("ForUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("OneTimePassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("ValidFor")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ForUserId");

                    b.ToTable("Confirmations");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TakesPlaceAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("BDP.Domain.Entities.EventType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("BDP.Domain.Entities.FinancialRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<long>("MadeById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("MadeById");

                    b.ToTable("FinancialRecords");
                });

            modelBuilder.Entity("BDP.Domain.Entities.FinancialRecordVerification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("FinancialRecordId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Outcome")
                        .HasColumnType("int");

                    b.Property<long>("VerifiedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FinancialRecordId");

                    b.HasIndex("VerifiedById");

                    b.ToTable("FinancialRecordVerifications");
                });

            modelBuilder.Entity("BDP.Domain.Entities.PhoneNumber", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("BDP.Domain.Entities.ProductOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionId");

                    b.ToTable("ProductOrders");
                });

            modelBuilder.Entity("BDP.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Sellable", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EventId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("OfferedById")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("OfferedById");

                    b.ToTable("Sellable");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Sellable");
                });

            modelBuilder.Entity("BDP.Domain.Entities.ServiceReservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TransactionId");

                    b.ToTable("ServiceReservations");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("ConfirmationToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("FromId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("ToId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConfirmationToken")
                        .IsUnique();

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BDP.Domain.Entities.TransactionConfirmation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Outcome")
                        .HasColumnType("int");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionConfirmations");
                });

            modelBuilder.Entity("BDP.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("CoverPictureId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProfilePictureId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CoverPictureId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfilePictureId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BDP.Domain.Entities.UserGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("UserUserGroup", b =>
                {
                    b.Property<long>("GroupsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserUserGroup");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Product", b =>
                {
                    b.HasBaseType("BDP.Domain.Entities.Sellable");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("Product");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Service", b =>
                {
                    b.HasBaseType("BDP.Domain.Entities.Sellable");

                    b.Property<DateTime>("AvailableBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AvailableEnd")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Service");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Event", null)
                        .WithMany("Pictures")
                        .HasForeignKey("EventId");

                    b.HasOne("BDP.Domain.Entities.Sellable", null)
                        .WithMany("Attachments")
                        .HasForeignKey("SellableId");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Confirmation", b =>
                {
                    b.HasOne("BDP.Domain.Entities.User", "ForUser")
                        .WithMany()
                        .HasForeignKey("ForUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForUser");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Event", b =>
                {
                    b.HasOne("BDP.Domain.Entities.EventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("BDP.Domain.Entities.FinancialRecord", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Attachment", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.User", "MadeBy")
                        .WithMany()
                        .HasForeignKey("MadeById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("MadeBy");
                });

            modelBuilder.Entity("BDP.Domain.Entities.FinancialRecordVerification", b =>
                {
                    b.HasOne("BDP.Domain.Entities.FinancialRecord", "FinancialRecord")
                        .WithMany()
                        .HasForeignKey("FinancialRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.User", "VerifiedBy")
                        .WithMany()
                        .HasForeignKey("VerifiedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FinancialRecord");

                    b.Navigation("VerifiedBy");
                });

            modelBuilder.Entity("BDP.Domain.Entities.PhoneNumber", b =>
                {
                    b.HasOne("BDP.Domain.Entities.User", null)
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BDP.Domain.Entities.ProductOrder", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BDP.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("BDP.Domain.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Sellable", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Event", null)
                        .WithMany("Sellables")
                        .HasForeignKey("EventId");

                    b.HasOne("BDP.Domain.Entities.User", "OfferedBy")
                        .WithMany()
                        .HasForeignKey("OfferedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfferedBy");
                });

            modelBuilder.Entity("BDP.Domain.Entities.ServiceReservation", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("BDP.Domain.Entities.User", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.User", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("BDP.Domain.Entities.TransactionConfirmation", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BDP.Domain.Entities.User", b =>
                {
                    b.HasOne("BDP.Domain.Entities.Attachment", "CoverPicture")
                        .WithMany()
                        .HasForeignKey("CoverPictureId");

                    b.HasOne("BDP.Domain.Entities.Attachment", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.Navigation("CoverPicture");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("UserUserGroup", b =>
                {
                    b.HasOne("BDP.Domain.Entities.UserGroup", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BDP.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BDP.Domain.Entities.Event", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("Sellables");
                });

            modelBuilder.Entity("BDP.Domain.Entities.Sellable", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("BDP.Domain.Entities.User", b =>
                {
                    b.Navigation("PhoneNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
