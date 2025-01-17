﻿// <auto-generated />
using System;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230121041945_initital_migration")]
    partial class inititalmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nchar(5)")
                        .HasColumnName("nik");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("NIK");

                    b.HasAlternateKey("Email");

                    b.ToTable("tb_m_accounts");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNIK")
                        .IsRequired()
                        .HasColumnType("nchar(5)")
                        .HasColumnName("account_nik");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("AccountNIK");

                    b.HasIndex("RoleId");

                    b.ToTable("tb_r_accounts_roles");
                });

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tb_m_departments");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nchar(5)")
                        .HasColumnName("nik");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birth_date");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int")
                        .HasColumnName("department_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("last_name");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nchar(5)")
                        .HasColumnName("manager_id");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("phone");

                    b.Property<int>("Salary")
                        .HasColumnType("int")
                        .HasColumnName("salary");

                    b.HasKey("NIK");

                    b.HasAlternateKey("Phone");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ManagerId");

                    b.ToTable("tb_m_employees");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tb_m_roles");
                });

            modelBuilder.Entity("API.Models.SPLK", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<int>("JmlJam")
                        .HasColumnType("int")
                        .HasColumnName("hours_of_ot");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("nchar(5)")
                        .HasColumnName("nik");

                    b.Property<int>("OvertimeType")
                        .HasColumnType("int")
                        .HasColumnName("lembur_id");

                    b.Property<byte[]>("ProofOvertime")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("proof_overtime");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("TglSelesai")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date_approval");

                    b.Property<double>("UpahLembur")
                        .HasColumnType("float")
                        .HasColumnName("salary_ot");

                    b.HasKey("Id");

                    b.HasIndex("NIK");

                    b.ToTable("tb_m_splk");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Models.Account", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.HasOne("API.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountNIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Employee", "Manager")
                        .WithMany("Employees")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Department");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("API.Models.SPLK", b =>
                {
                    b.HasOne("API.Models.Employee", "Employees")
                        .WithMany("Splks")
                        .HasForeignKey("NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Employees");

                    b.Navigation("Splks");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
