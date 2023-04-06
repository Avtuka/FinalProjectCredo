﻿// <auto-generated />
using System;
using BankingManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingManagement.Persistence.Migrations
{
    [DbContext(typeof(BankingManagementDbContext))]
    [Migration("20230404135505_TransactionTypesAdd")]
    partial class TransactionTypesAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankingManagement.Domain.Account.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("IBAN")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BankingManagement.Domain.Card.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<short>("CVC")
                        .HasColumnType("smallint");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(900)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("DateTime");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(90)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(90)");

                    b.Property<short>("PIN")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber")
                        .IsUnique();

                    b.ToTable("Card");
                });

            modelBuilder.Entity("BankingManagement.Domain.Operator.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .IsUnicode(false)
                        .HasColumnType("varchar(70)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(false)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("PrivateNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Operator");
                });

            modelBuilder.Entity("BankingManagement.Domain.Transactions.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<double>("Comission")
                        .HasColumnType("float");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromIBAN")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("ToIBAN")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromIBAN");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("ToIBAN");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BankingManagement.Domain.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .IsUnicode(false)
                        .HasColumnType("varchar(70)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(false)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("PrivateNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrivateNumber")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("BankingManagement.Domain.Account.Account", b =>
                {
                    b.HasOne("BankingManagement.Domain.Card.Card", "Card")
                        .WithMany("Accounts")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingManagement.Domain.User.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BankingManagement.Domain.Transactions.Transaction", b =>
                {
                    b.HasOne("BankingManagement.Domain.User.User", "Reciever")
                        .WithMany("TransactionsRecieved")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BankingManagement.Domain.User.User", "Sender")
                        .WithMany("TransactionsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Reciever");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("BankingManagement.Domain.Card.Card", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BankingManagement.Domain.User.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("TransactionsRecieved");

                    b.Navigation("TransactionsSent");
                });
#pragma warning restore 612, 618
        }
    }
}
