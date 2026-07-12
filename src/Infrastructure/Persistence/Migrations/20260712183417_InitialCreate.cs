using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kondongpu.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaneTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CarNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LanguageCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    VatId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Moo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Village = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubDistrict = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Telephone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Fax = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LogoPath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AuthCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prefixs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(510)", maxLength: 510, nullable: false),
                    NameEnglish = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "ชื่อกลุ่มรายงาน"),
                    Sort = table.Column<int>(type: "integer", nullable: false, comment: "ลำดับการแสดงผล"),
                    CreateUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่สร้างรายงาน"),
                    ModifiedUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่แก้ไขรายงาน"),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "สถานะการเปิดใช้งาน"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleReportTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false, comment: "ชื่อกลุ่มรายงาน"),
                    ReportTemplateList = table.Column<List<int>>(type: "json", nullable: false, comment: "กลุ่มรหัสรายงาน Ref. ReportTemplate.Id"),
                    CreateUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่สร้างรายการ"),
                    ModifiedUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่แก้ไขล่าสุด"),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "สถานะการเปิดใช้งาน"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "วันที่สร้างรายการ"),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "วันที่แก้ไขรายการ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleReportTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunningConfigs",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsCode = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false),
                    IsYear = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false),
                    DigitCount = table.Column<int>(type: "integer", nullable: false),
                    TemplateCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningConfigs", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Runnings",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    YearCode = table.Column<int>(type: "integer", nullable: false),
                    LastRunning = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runnings", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Passwords = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PositionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastLog = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BillReference = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CarNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CaneTypeId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: true),
                    NetPrice = table.Column<double>(type: "double precision", nullable: true),
                    GasPrice = table.Column<double>(type: "double precision", nullable: true),
                    NetBalance = table.Column<double>(type: "double precision", nullable: true),
                    BillFlag = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Remark = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    MUser = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AccountName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Branch = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    AliasName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OwnerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VatId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressNo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Moo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Village = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubDistrict = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProvinceId = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Telephone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Fax = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    MUser = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    PrefixId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NickName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Sex = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    CardId = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    FamerId = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Tel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AddressNo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Moo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Village = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SubDistrict = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProvinceId = table.Column<string>(type: "text", nullable: true),
                    Zipcode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    MUser = table.Column<int>(type: "integer", nullable: true),
                    AccountNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AccountName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameEnglish = table.Column<string>(type: "text", nullable: false),
                    ProvinceId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "ชื่อรายงาน"),
                    SqlText = table.Column<string>(type: "text", nullable: true, comment: "คำสั่ง sql"),
                    ReportGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsConfidential = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "รายงานปกปิด"),
                    CreateUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, comment: "รหัสผู้ใช้งานที่สร้างรายงาน"),
                    ModifiedUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, comment: "รหัสผู้ใช้งานที่แก้ไขรายงาน"),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "สถานะการเปิดใช้งาน"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "วันที่สร้างรายงาน"),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "วันที่แก้ไขรายงาน")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTemplates_ReportGroups_ReportGroupId",
                        column: x => x.ReportGroupId,
                        principalTable: "ReportGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceYear = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CaneTypeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    MUser = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_CaneTypes_CaneTypeId",
                        column: x => x.CaneTypeId,
                        principalTable: "CaneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BillDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CompanyCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: true),
                    TotalNetPrice = table.Column<double>(type: "double precision", nullable: true),
                    TotalDeduct = table.Column<double>(type: "double precision", nullable: true),
                    Balance = table.Column<double>(type: "double precision", nullable: true),
                    CUser = table.Column<int>(type: "integer", nullable: true),
                    MUser = table.Column<int>(type: "integer", nullable: true),
                    Remark = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubDistricts",
                columns: table => new
                {
                    SubDistrictId = table.Column<string>(type: "text", nullable: false),
                    ProvinceId = table.Column<string>(type: "text", nullable: false),
                    DistrictId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameEnglish = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDistricts", x => x.SubDistrictId);
                    table.ForeignKey(
                        name: "FK_SubDistricts_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplateDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportTemplateId = table.Column<int>(type: "integer", nullable: false, comment: "รหัสอ้างอิงเลขลำดับของรายงาน"),
                    ParameterName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "ชื่อพารามีเตอร์"),
                    ControlType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, comment: "ประเภทของ control"),
                    Description = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "รายละเอียด"),
                    ValueMember = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, comment: "ชื่อค่าที่ต้องการอ้างอิง"),
                    DisplayMember = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "ชื่อค่าที่ต้องการอ้างอิง"),
                    SqlText = table.Column<string>(type: "text", nullable: true, comment: "คำสั่ง sql"),
                    CreateUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่สร้างรายงาน"),
                    ModifiedUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "รหัสผู้ใช้งานที่แก้ไขรายงาน"),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "สถานะการเปิดใช้งาน"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTemplateDetails_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalTable: "ReportTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_AccountId",
                table: "BillDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CompanyId",
                table: "Bills",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ProvinceId",
                table: "Company",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankId",
                table: "Customers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ProvinceId",
                table: "Customers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CaneTypeId",
                table: "Prices",
                column: "CaneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CompanyId",
                table: "Prices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportGroups_IsActive",
                table: "ReportGroups",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateDetails_IsActive",
                table: "ReportTemplateDetails",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateDetails_ReportTemplateId",
                table: "ReportTemplateDetails",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_Id",
                table: "ReportTemplates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_IsActive",
                table: "ReportTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_IsConfidential",
                table: "ReportTemplates",
                column: "IsConfidential");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_ReportGroupId",
                table: "ReportTemplates",
                column: "ReportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleReportTemplates_Id",
                table: "RoleReportTemplates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleReportTemplates_IsActive",
                table: "RoleReportTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SubDistricts_DistrictId",
                table: "SubDistricts",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "CustomerCars");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Prefixs");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "ReportTemplateDetails");

            migrationBuilder.DropTable(
                name: "RoleReportTemplates");

            migrationBuilder.DropTable(
                name: "RunningConfigs");

            migrationBuilder.DropTable(
                name: "Runnings");

            migrationBuilder.DropTable(
                name: "SubDistricts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CaneTypes");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ReportTemplates");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "ReportGroups");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
