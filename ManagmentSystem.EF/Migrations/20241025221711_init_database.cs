using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagmentSystem.EF.Migrations
{
    public partial class init_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departmentes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentParentId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepCode = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<ulong>(type: "bit", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmentes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCostAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCostWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<float>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EDescription = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ADescription = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Productes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<float>(type: "float", nullable: false, defaultValue: 0f),
                    Status = table.Column<ulong>(type: "bit", nullable: false),
                    IsDiscount = table.Column<ulong>(type: "bit", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SessionId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAdmin = table.Column<ulong>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AFirstName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EFirstName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ALastName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ELastName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBlocked = table.Column<ulong>(type: "bit", nullable: false),
                    IsAdmin = table.Column<ulong>(type: "bit", nullable: false, defaultValue: 0ul),
                    BlockedType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Positiones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLeader = table.Column<ulong>(type: "bit", nullable: false),
                    IsActive = table.Column<ulong>(type: "bit", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DepartmentId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positiones_Departmentes_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departmentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryProductes",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductes", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryProductes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProductes_Productes_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Productes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageFolderes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagePath = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    CategoryId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFolderes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageFolderes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageFolderes_Productes_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Productes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolePrivileges",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrivilegeId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivileges", x => new { x.RoleId, x.PrivilegeId });
                    table.ForeignKey(
                        name: "FK_RolePrivileges_Privileges_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privileges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePrivileges_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customeres",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customeres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customeres_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produceres",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produceres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produceres_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagePath = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Language = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Theme = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersPositions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<ulong>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "NOW()"),
                    StartDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PositionId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersPositions_Positiones_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPositions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<float>(type: "float", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true, defaultValue: "Request")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessType = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    InvoiceId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProducerId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Customeres_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customeres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processes_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processes_Produceres_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Produceres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processes_Productes_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Productes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccessListPrivileges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPositionId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<float>(type: "float", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessListPrivileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessListPrivileges_UsersPositions_UserPositionId",
                        column: x => x.UserPositionId,
                        principalTable: "UsersPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleUserPosition",
                columns: table => new
                {
                    RolesId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPositionesId = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUserPosition", x => new { x.RolesId, x.UserPositionesId });
                    table.ForeignKey(
                        name: "FK_RoleUserPosition_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUserPosition_UsersPositions_UserPositionesId",
                        column: x => x.UserPositionesId,
                        principalTable: "UsersPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserPositionRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastAccessed = table.Column<DateTime>(type: "DateTime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    AddedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RoleId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPositionId = table.Column<string>(type: "VARCHAR(36)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPositionRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPositionRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPositionRoles_UsersPositions_UserPositionId",
                        column: x => x.UserPositionId,
                        principalTable: "UsersPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Departmentes",
                columns: new[] { "Id", "AName", "Code", "DepCode", "DepartmentParentId", "DepartmentType", "EName", "IsActive" },
                values: new object[] { "b5079173-69ba-40b1-b375-9d7958bea99c", "الإدارة العامة", "GD", "0001", "", "GeneralDepartment", "General department", 1ul });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "ADescription", "AName", "Code", "EDescription", "EName", "Type" },
                values: new object[] { "15408bef-2786-47bd-b958-900d73492ed3", "مدير النظام", "مدير النظام", 1f, "System manager", "SystemManager", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AName", "EName" },
                values: new object[] { "c0de3799-b907-4951-8541-f7da578f5cd7", "دور مدير النظام", "Role system manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AFirstName", "ALastName", "BlockedType", "EFirstName", "ELastName", "Email", "IsAdmin", "IsBlocked", "Password", "PhoneNumber", "UserName", "UserType" },
                values: new object[] { "c6d10ce6-94ca-40a7-a19a-38485a79c0cb", "مدير", "النظام", "None", "System", "Manager", "nawras4mstarehe@gmail.com", 1ul, 0ul, "AQAAAAEAACcQAAAAENRBDSaUZGPpRa2bFRTX5gB6oif6S4N+uoUZJgkR5orpYrlyEaVriHcDzQXXLMm/Hg==", "0953244518", "admin", "Employee" });

            migrationBuilder.InsertData(
                table: "Positiones",
                columns: new[] { "Id", "AName", "DepartmentId", "EName", "IsActive", "IsLeader" },
                values: new object[] { "05fb1167-86ce-4a88-91b5-0a065ae66e6a", "المدير العام", "b5079173-69ba-40b1-b375-9d7958bea99c", "Manager General", 1ul, 1ul });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegeId", "RoleId", "Id" },
                values: new object[] { "15408bef-2786-47bd-b958-900d73492ed3", "c0de3799-b907-4951-8541-f7da578f5cd7", "4cd39865-785a-4e6e-bdf0-3c257827e6a9" });

            migrationBuilder.InsertData(
                table: "UsersPositions",
                columns: new[] { "Id", "EndDate", "IsActive", "PositionId", "StartDate", "Type", "UserId", "UserName" },
                values: new object[] { "b0e4c635-0c47-4f8c-b97c-99b055bd267a", new DateTime(2025, 10, 26, 1, 17, 10, 760, DateTimeKind.Local).AddTicks(6664), 1ul, "05fb1167-86ce-4a88-91b5-0a065ae66e6a", new DateTime(2024, 10, 26, 1, 17, 10, 760, DateTimeKind.Local).AddTicks(6219), "HR", "c6d10ce6-94ca-40a7-a19a-38485a79c0cb", "admin" });

            migrationBuilder.InsertData(
                table: "AccessListPrivileges",
                columns: new[] { "Id", "Code", "UserPositionId" },
                values: new object[] { "cabcf242-3e4e-4ad9-a82f-1b357efffb49", 1f, "b0e4c635-0c47-4f8c-b97c-99b055bd267a" });

            migrationBuilder.InsertData(
                table: "UserPositionRoles",
                columns: new[] { "Id", "AddedOn", "RoleId", "UserPositionId" },
                values: new object[] { "1eedc01c-a94a-44f3-85e8-a7c732a57037", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c0de3799-b907-4951-8541-f7da578f5cd7", "b0e4c635-0c47-4f8c-b97c-99b055bd267a" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessListPrivileges_Id",
                table: "AccessListPrivileges",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessListPrivileges_UserPositionId",
                table: "AccessListPrivileges",
                column: "UserPositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id",
                table: "Categories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductes_CategoryId",
                table: "CategoryProductes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductes_Id",
                table: "CategoryProductes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customeres_Id",
                table: "Customeres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customeres_UserId",
                table: "Customeres",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departmentes_Id_Code",
                table: "Departmentes",
                columns: new[] { "Id", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageFolderes_CategoryId",
                table: "ImageFolderes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageFolderes_Id",
                table: "ImageFolderes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageFolderes_ProductId",
                table: "ImageFolderes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Id",
                table: "Invoices",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positiones_DepartmentId",
                table: "Positiones",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positiones_Id",
                table: "Positiones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Privileges_Id",
                table: "Privileges",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_CustomerId",
                table: "Processes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Id",
                table: "Processes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_InvoiceId",
                table: "Processes",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ProducerId",
                table: "Processes",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ProductId",
                table: "Processes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Produceres_Id",
                table: "Produceres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produceres_UserId",
                table: "Produceres",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productes_Id",
                table: "Productes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivileges_Id",
                table: "RolePrivileges",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivileges_PrivilegeId",
                table: "RolePrivileges",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserPosition_UserPositionesId",
                table: "RoleUserPosition",
                column: "UserPositionesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositionRoles_Id",
                table: "UserPositionRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPositionRoles_RoleId",
                table: "UserPositionRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositionRoles_UserPositionId",
                table: "UserPositionRoles",
                column: "UserPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Id",
                table: "UserProfiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_UserName",
                table: "Users",
                columns: new[] { "Id", "UserName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPositions_Id",
                table: "UsersPositions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPositions_PositionId",
                table: "UsersPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPositions_UserId",
                table: "UsersPositions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessListPrivileges");

            migrationBuilder.DropTable(
                name: "CategoryProductes");

            migrationBuilder.DropTable(
                name: "ImageFolderes");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "RolePrivileges");

            migrationBuilder.DropTable(
                name: "RoleUserPosition");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "UserPositionRoles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customeres");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Produceres");

            migrationBuilder.DropTable(
                name: "Productes");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UsersPositions");

            migrationBuilder.DropTable(
                name: "Positiones");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departmentes");
        }
    }
}
