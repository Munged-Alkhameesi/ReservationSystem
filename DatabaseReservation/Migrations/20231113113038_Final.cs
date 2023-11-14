using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseReservation.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    areaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.areaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    guestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guestFirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    guestLastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    guestEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    guestPhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.guestId);
                });

            migrationBuilder.CreateTable(
                name: "Sittings",
                columns: table => new
                {
                    sittingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sittingType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    startDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    endDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sittings", x => x.sittingId);
                });

            migrationBuilder.CreateTable(
                name: "AllTables",
                columns: table => new
                {
                    tableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tableName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    areaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllTables", x => x.tableId);
                    table.ForeignKey(
                        name: "FK_AllTables_Areas_areaId",
                        column: x => x.areaId,
                        principalTable: "Areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guestCount = table.Column<int>(type: "int", nullable: false),
                    reservationSource = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    startDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    sittingId = table.Column<int>(type: "int", nullable: false),
                    guestId = table.Column<int>(type: "int", nullable: false),
                    ResStatus = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Guest_guestId",
                        column: x => x.guestId,
                        principalTable: "Guest",
                        principalColumn: "guestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Sittings_sittingId",
                        column: x => x.sittingId,
                        principalTable: "Sittings",
                        principalColumn: "sittingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservedTables",
                columns: table => new
                {
                    ReservedTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    tableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedTables", x => x.ReservedTableId);
                    table.ForeignKey(
                        name: "FK_ReservedTables_AllTables_tableId",
                        column: x => x.tableId,
                        principalTable: "AllTables",
                        principalColumn: "tableId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservedTables_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "areaId", "AreaType" },
                values: new object[,]
                {
                    { 1, "Main" },
                    { 2, "Outside" },
                    { 3, "Balcony" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "manager", "MANAGER" },
                    { "2", null, "staff", "STAFF" },
                    { "3", null, "member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "181413fd-7aa8-4ed4-98f4-a121162b3019", 0, "b90ed227-7d71-44f7-872e-ffe99420512f", "staff@beanscene.com", true, "Adam", "Smith", false, null, null, "STAFF", "AQAAAAIAAYagAAAAEFrH0BD0IhBRUvhSInqlZ4/uqRzn8Iy9YytyEVy2CG7mTEBDDT2TJPrICJvDK/ax1Q==", null, true, "9ea22007-6574-43cd-9373-6803358c3854", false, "Staff" },
                    { "7f284a9c-5141-4f86-ae1c-0be5d6bd226b", 0, "27234bd2-1721-47e4-95f7-e4a23153f486", "member@beanscene.com", true, "Jason", "Smith", false, null, null, "MEMBER", "AQAAAAIAAYagAAAAEDc1HEdmjy+orojJbYeDoCAVjrTPm6wk/oostWrcAAdtc5b2G9izz3AvrZyi4KMTaQ==", null, true, "4c23f2cd-3188-46a0-b3f8-39dd10f81f59", false, "Member" },
                    { "8b11af29-826a-47d2-b0ae-706499efdc9a", 0, "f2e72c7e-5838-4972-a630-bcf0ef2bad7b", "manager@beanscene.com", true, "John", "Smith", false, null, null, "MANAGER", "AQAAAAIAAYagAAAAEO86FH+zBo9CprdQMtIGQnV6jja4CQ5rZeugi3tZY7PE50Jnd/B5YnZCc/vPO+13Ew==", null, true, "ebd19bec-3309-4b41-b49e-2ce7abfc55f6", false, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Sittings",
                columns: new[] { "sittingId", "capacity", "endDateTime", "sittingType", "startDateTime" },
                values: new object[,]
                {
                    { 1, 40, new DateTime(2023, 11, 13, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 13, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 40, new DateTime(2023, 11, 13, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 13, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 40, new DateTime(2023, 11, 13, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 13, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 40, new DateTime(2023, 11, 14, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 14, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 40, new DateTime(2023, 11, 14, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 14, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 6, 40, new DateTime(2023, 11, 14, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 14, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 7, 40, new DateTime(2023, 11, 15, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 15, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 8, 40, new DateTime(2023, 11, 15, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 15, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 9, 40, new DateTime(2023, 11, 15, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 15, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 10, 40, new DateTime(2023, 11, 16, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 16, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 11, 40, new DateTime(2023, 11, 16, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 16, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 12, 40, new DateTime(2023, 11, 16, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 16, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 13, 40, new DateTime(2023, 11, 17, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 17, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 14, 40, new DateTime(2023, 11, 17, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 17, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 15, 40, new DateTime(2023, 11, 17, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 17, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 16, 40, new DateTime(2023, 11, 18, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 18, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 17, 40, new DateTime(2023, 11, 18, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 18, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 18, 40, new DateTime(2023, 11, 18, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 18, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 19, 40, new DateTime(2023, 11, 19, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 19, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 20, 40, new DateTime(2023, 11, 19, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 19, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 21, 40, new DateTime(2023, 11, 19, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 19, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 22, 40, new DateTime(2023, 11, 20, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 20, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 23, 40, new DateTime(2023, 11, 20, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 20, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 24, 40, new DateTime(2023, 11, 20, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 20, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 25, 40, new DateTime(2023, 11, 21, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 21, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 26, 40, new DateTime(2023, 11, 21, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 21, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 27, 40, new DateTime(2023, 11, 21, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 21, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 28, 40, new DateTime(2023, 11, 22, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 22, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 29, 40, new DateTime(2023, 11, 22, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 22, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 30, 40, new DateTime(2023, 11, 22, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 22, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 31, 40, new DateTime(2023, 11, 23, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 23, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 32, 40, new DateTime(2023, 11, 23, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 23, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 33, 40, new DateTime(2023, 11, 23, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 23, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 34, 40, new DateTime(2023, 11, 24, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 24, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 35, 40, new DateTime(2023, 11, 24, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 24, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 36, 40, new DateTime(2023, 11, 24, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 24, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 37, 40, new DateTime(2023, 11, 25, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 25, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 38, 40, new DateTime(2023, 11, 25, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 25, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 39, 40, new DateTime(2023, 11, 25, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 25, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 40, 40, new DateTime(2023, 11, 26, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 26, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 41, 40, new DateTime(2023, 11, 26, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 26, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 42, 40, new DateTime(2023, 11, 26, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 26, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 43, 40, new DateTime(2023, 11, 27, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 27, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 44, 40, new DateTime(2023, 11, 27, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 27, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 45, 40, new DateTime(2023, 11, 27, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 27, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 46, 40, new DateTime(2023, 11, 28, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 28, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 47, 40, new DateTime(2023, 11, 28, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 28, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 48, 40, new DateTime(2023, 11, 28, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 28, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 49, 40, new DateTime(2023, 11, 29, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 29, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 50, 40, new DateTime(2023, 11, 29, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 29, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 51, 40, new DateTime(2023, 11, 29, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 29, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 52, 40, new DateTime(2023, 11, 30, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 11, 30, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 53, 40, new DateTime(2023, 11, 30, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 11, 30, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 54, 40, new DateTime(2023, 11, 30, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 11, 30, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 55, 40, new DateTime(2023, 12, 1, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 1, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 56, 40, new DateTime(2023, 12, 1, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 1, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 57, 40, new DateTime(2023, 12, 1, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 1, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 58, 40, new DateTime(2023, 12, 2, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 2, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 59, 40, new DateTime(2023, 12, 2, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 2, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 60, 40, new DateTime(2023, 12, 2, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 2, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 61, 40, new DateTime(2023, 12, 3, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 3, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 62, 40, new DateTime(2023, 12, 3, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 3, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 63, 40, new DateTime(2023, 12, 3, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 3, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 64, 40, new DateTime(2023, 12, 4, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 4, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 65, 40, new DateTime(2023, 12, 4, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 4, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 66, 40, new DateTime(2023, 12, 4, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 4, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 67, 40, new DateTime(2023, 12, 5, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 5, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 68, 40, new DateTime(2023, 12, 5, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 5, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 69, 40, new DateTime(2023, 12, 5, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 5, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 70, 40, new DateTime(2023, 12, 6, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 6, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 71, 40, new DateTime(2023, 12, 6, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 6, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 72, 40, new DateTime(2023, 12, 6, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 6, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 73, 40, new DateTime(2023, 12, 7, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 7, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 74, 40, new DateTime(2023, 12, 7, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 7, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 75, 40, new DateTime(2023, 12, 7, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 7, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 76, 40, new DateTime(2023, 12, 8, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 8, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 77, 40, new DateTime(2023, 12, 8, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 8, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 78, 40, new DateTime(2023, 12, 8, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 8, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 79, 40, new DateTime(2023, 12, 9, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 9, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 80, 40, new DateTime(2023, 12, 9, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 9, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 81, 40, new DateTime(2023, 12, 9, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 9, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 82, 40, new DateTime(2023, 12, 10, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 10, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 83, 40, new DateTime(2023, 12, 10, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 10, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 84, 40, new DateTime(2023, 12, 10, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 10, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 85, 40, new DateTime(2023, 12, 11, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 11, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 86, 40, new DateTime(2023, 12, 11, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 11, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 87, 40, new DateTime(2023, 12, 11, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 11, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 88, 40, new DateTime(2023, 12, 12, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 12, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 89, 40, new DateTime(2023, 12, 12, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 12, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 90, 40, new DateTime(2023, 12, 12, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 12, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 91, 40, new DateTime(2023, 12, 13, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 13, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 92, 40, new DateTime(2023, 12, 13, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 13, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 93, 40, new DateTime(2023, 12, 13, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 13, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 94, 40, new DateTime(2023, 12, 14, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 14, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 95, 40, new DateTime(2023, 12, 14, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 14, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 96, 40, new DateTime(2023, 12, 14, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 14, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 97, 40, new DateTime(2023, 12, 15, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 15, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 98, 40, new DateTime(2023, 12, 15, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 15, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 99, 40, new DateTime(2023, 12, 15, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 15, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 100, 40, new DateTime(2023, 12, 16, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 16, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 101, 40, new DateTime(2023, 12, 16, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 16, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 102, 40, new DateTime(2023, 12, 16, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 16, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 103, 40, new DateTime(2023, 12, 17, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 17, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 104, 40, new DateTime(2023, 12, 17, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 17, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 105, 40, new DateTime(2023, 12, 17, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 17, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 106, 40, new DateTime(2023, 12, 18, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 18, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 107, 40, new DateTime(2023, 12, 18, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 18, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 108, 40, new DateTime(2023, 12, 18, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 18, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 109, 40, new DateTime(2023, 12, 19, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 19, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 110, 40, new DateTime(2023, 12, 19, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 19, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 111, 40, new DateTime(2023, 12, 19, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 19, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 112, 40, new DateTime(2023, 12, 20, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 20, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 113, 40, new DateTime(2023, 12, 20, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 20, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 114, 40, new DateTime(2023, 12, 20, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 20, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 115, 40, new DateTime(2023, 12, 21, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 21, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 116, 40, new DateTime(2023, 12, 21, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 21, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 117, 40, new DateTime(2023, 12, 21, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 21, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 118, 40, new DateTime(2023, 12, 22, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 22, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 119, 40, new DateTime(2023, 12, 22, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 22, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 120, 40, new DateTime(2023, 12, 22, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 22, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 121, 40, new DateTime(2023, 12, 23, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 23, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 122, 40, new DateTime(2023, 12, 23, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 23, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 123, 40, new DateTime(2023, 12, 23, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 23, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 124, 40, new DateTime(2023, 12, 24, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 24, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 125, 40, new DateTime(2023, 12, 24, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 24, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 126, 40, new DateTime(2023, 12, 24, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 24, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 127, 40, new DateTime(2023, 12, 25, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 25, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 128, 40, new DateTime(2023, 12, 25, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 25, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 129, 40, new DateTime(2023, 12, 25, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 25, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 130, 40, new DateTime(2023, 12, 26, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 26, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 131, 40, new DateTime(2023, 12, 26, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 26, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 132, 40, new DateTime(2023, 12, 26, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 26, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 133, 40, new DateTime(2023, 12, 27, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 27, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 134, 40, new DateTime(2023, 12, 27, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 27, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 135, 40, new DateTime(2023, 12, 27, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 27, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 136, 40, new DateTime(2023, 12, 28, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 28, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 137, 40, new DateTime(2023, 12, 28, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 28, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 138, 40, new DateTime(2023, 12, 28, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 28, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 139, 40, new DateTime(2023, 12, 29, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 29, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 140, 40, new DateTime(2023, 12, 29, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 29, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 141, 40, new DateTime(2023, 12, 29, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 29, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 142, 40, new DateTime(2023, 12, 30, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 30, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 143, 40, new DateTime(2023, 12, 30, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 30, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 144, 40, new DateTime(2023, 12, 30, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 30, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 145, 40, new DateTime(2023, 12, 31, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2023, 12, 31, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 146, 40, new DateTime(2023, 12, 31, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2023, 12, 31, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 147, 40, new DateTime(2023, 12, 31, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2023, 12, 31, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 148, 40, new DateTime(2024, 1, 1, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 1, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 149, 40, new DateTime(2024, 1, 1, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 150, 40, new DateTime(2024, 1, 1, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 1, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 151, 40, new DateTime(2024, 1, 2, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 2, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 152, 40, new DateTime(2024, 1, 2, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 2, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 153, 40, new DateTime(2024, 1, 2, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 2, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 154, 40, new DateTime(2024, 1, 3, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 3, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 155, 40, new DateTime(2024, 1, 3, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 3, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 156, 40, new DateTime(2024, 1, 3, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 3, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 157, 40, new DateTime(2024, 1, 4, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 4, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 158, 40, new DateTime(2024, 1, 4, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 4, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 159, 40, new DateTime(2024, 1, 4, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 4, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 160, 40, new DateTime(2024, 1, 5, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 5, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 161, 40, new DateTime(2024, 1, 5, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 5, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 162, 40, new DateTime(2024, 1, 5, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 5, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 163, 40, new DateTime(2024, 1, 6, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 6, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 164, 40, new DateTime(2024, 1, 6, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 6, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 165, 40, new DateTime(2024, 1, 6, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 6, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 166, 40, new DateTime(2024, 1, 7, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 7, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 167, 40, new DateTime(2024, 1, 7, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 7, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 168, 40, new DateTime(2024, 1, 7, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 7, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 169, 40, new DateTime(2024, 1, 8, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 8, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 170, 40, new DateTime(2024, 1, 8, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 8, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 171, 40, new DateTime(2024, 1, 8, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 8, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 172, 40, new DateTime(2024, 1, 9, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 9, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 173, 40, new DateTime(2024, 1, 9, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 9, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 174, 40, new DateTime(2024, 1, 9, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 9, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 175, 40, new DateTime(2024, 1, 10, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 10, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 176, 40, new DateTime(2024, 1, 10, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 10, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 177, 40, new DateTime(2024, 1, 10, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 10, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 178, 40, new DateTime(2024, 1, 11, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 11, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 179, 40, new DateTime(2024, 1, 11, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 11, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 180, 40, new DateTime(2024, 1, 11, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 11, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 181, 40, new DateTime(2024, 1, 12, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 12, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 182, 40, new DateTime(2024, 1, 12, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 12, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 183, 40, new DateTime(2024, 1, 12, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 12, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 184, 40, new DateTime(2024, 1, 13, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 13, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 185, 40, new DateTime(2024, 1, 13, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 13, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 186, 40, new DateTime(2024, 1, 13, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 13, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 187, 40, new DateTime(2024, 1, 14, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 14, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 188, 40, new DateTime(2024, 1, 14, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 14, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 189, 40, new DateTime(2024, 1, 14, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 14, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 190, 40, new DateTime(2024, 1, 15, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 15, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 191, 40, new DateTime(2024, 1, 15, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 192, 40, new DateTime(2024, 1, 15, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 15, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 193, 40, new DateTime(2024, 1, 16, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 16, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 194, 40, new DateTime(2024, 1, 16, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 16, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 195, 40, new DateTime(2024, 1, 16, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 16, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 196, 40, new DateTime(2024, 1, 17, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 17, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 197, 40, new DateTime(2024, 1, 17, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 17, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 198, 40, new DateTime(2024, 1, 17, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 17, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 199, 40, new DateTime(2024, 1, 18, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 18, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 200, 40, new DateTime(2024, 1, 18, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 18, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 201, 40, new DateTime(2024, 1, 18, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 18, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 202, 40, new DateTime(2024, 1, 19, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 19, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 203, 40, new DateTime(2024, 1, 19, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 19, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 204, 40, new DateTime(2024, 1, 19, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 19, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 205, 40, new DateTime(2024, 1, 20, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 20, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 206, 40, new DateTime(2024, 1, 20, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 20, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 207, 40, new DateTime(2024, 1, 20, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 20, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 208, 40, new DateTime(2024, 1, 21, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 21, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 209, 40, new DateTime(2024, 1, 21, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 21, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 210, 40, new DateTime(2024, 1, 21, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 21, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 211, 40, new DateTime(2024, 1, 22, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 22, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 212, 40, new DateTime(2024, 1, 22, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 22, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 213, 40, new DateTime(2024, 1, 22, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 22, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 214, 40, new DateTime(2024, 1, 23, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 23, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 215, 40, new DateTime(2024, 1, 23, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 23, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 216, 40, new DateTime(2024, 1, 23, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 23, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 217, 40, new DateTime(2024, 1, 24, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 24, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 218, 40, new DateTime(2024, 1, 24, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 24, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 219, 40, new DateTime(2024, 1, 24, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 24, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 220, 40, new DateTime(2024, 1, 25, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 25, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 221, 40, new DateTime(2024, 1, 25, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 25, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 222, 40, new DateTime(2024, 1, 25, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 25, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 223, 40, new DateTime(2024, 1, 26, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 26, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 224, 40, new DateTime(2024, 1, 26, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 26, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 225, 40, new DateTime(2024, 1, 26, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 26, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 226, 40, new DateTime(2024, 1, 27, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 27, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 227, 40, new DateTime(2024, 1, 27, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 27, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 228, 40, new DateTime(2024, 1, 27, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 27, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 229, 40, new DateTime(2024, 1, 28, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 28, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 230, 40, new DateTime(2024, 1, 28, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 28, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 231, 40, new DateTime(2024, 1, 28, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 28, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 232, 40, new DateTime(2024, 1, 29, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 29, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 233, 40, new DateTime(2024, 1, 29, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 29, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 234, 40, new DateTime(2024, 1, 29, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 29, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 235, 40, new DateTime(2024, 1, 30, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 30, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 236, 40, new DateTime(2024, 1, 30, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 30, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 237, 40, new DateTime(2024, 1, 30, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 30, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 238, 40, new DateTime(2024, 1, 31, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 1, 31, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 239, 40, new DateTime(2024, 1, 31, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 1, 31, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 240, 40, new DateTime(2024, 1, 31, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 1, 31, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 241, 40, new DateTime(2024, 2, 1, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 1, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 242, 40, new DateTime(2024, 2, 1, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 1, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 243, 40, new DateTime(2024, 2, 1, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 1, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 244, 40, new DateTime(2024, 2, 2, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 2, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 245, 40, new DateTime(2024, 2, 2, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 2, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 246, 40, new DateTime(2024, 2, 2, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 2, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 247, 40, new DateTime(2024, 2, 3, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 3, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 248, 40, new DateTime(2024, 2, 3, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 3, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 249, 40, new DateTime(2024, 2, 3, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 3, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 250, 40, new DateTime(2024, 2, 4, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 4, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 251, 40, new DateTime(2024, 2, 4, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 4, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 252, 40, new DateTime(2024, 2, 4, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 4, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 253, 40, new DateTime(2024, 2, 5, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 5, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 254, 40, new DateTime(2024, 2, 5, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 5, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 255, 40, new DateTime(2024, 2, 5, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 5, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 256, 40, new DateTime(2024, 2, 6, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 6, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 257, 40, new DateTime(2024, 2, 6, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 6, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 258, 40, new DateTime(2024, 2, 6, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 6, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 259, 40, new DateTime(2024, 2, 7, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 7, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 260, 40, new DateTime(2024, 2, 7, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 7, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 261, 40, new DateTime(2024, 2, 7, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 7, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 262, 40, new DateTime(2024, 2, 8, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 8, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 263, 40, new DateTime(2024, 2, 8, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 8, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 264, 40, new DateTime(2024, 2, 8, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 8, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 265, 40, new DateTime(2024, 2, 9, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 9, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 266, 40, new DateTime(2024, 2, 9, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 9, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 267, 40, new DateTime(2024, 2, 9, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 9, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 268, 40, new DateTime(2024, 2, 10, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 10, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 269, 40, new DateTime(2024, 2, 10, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 10, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 270, 40, new DateTime(2024, 2, 10, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 10, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 271, 40, new DateTime(2024, 2, 11, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 11, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 272, 40, new DateTime(2024, 2, 11, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 11, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 273, 40, new DateTime(2024, 2, 11, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 11, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 274, 40, new DateTime(2024, 2, 12, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 12, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 275, 40, new DateTime(2024, 2, 12, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 12, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 276, 40, new DateTime(2024, 2, 12, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 12, 17, 0, 0, 0, DateTimeKind.Local) },
                    { 277, 40, new DateTime(2024, 2, 13, 11, 59, 0, 0, DateTimeKind.Local), "breakfast", new DateTime(2024, 2, 13, 7, 0, 0, 0, DateTimeKind.Local) },
                    { 278, 40, new DateTime(2024, 2, 13, 16, 59, 0, 0, DateTimeKind.Local), "lunch", new DateTime(2024, 2, 13, 12, 0, 0, 0, DateTimeKind.Local) },
                    { 279, 40, new DateTime(2024, 2, 13, 21, 59, 0, 0, DateTimeKind.Local), "dinner", new DateTime(2024, 2, 13, 17, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "AllTables",
                columns: new[] { "tableId", "areaId", "tableName" },
                values: new object[,]
                {
                    { 1, 1, "M1" },
                    { 2, 1, "M2" },
                    { 3, 1, "M3" },
                    { 4, 1, "M4" },
                    { 5, 1, "M5" },
                    { 6, 1, "M6" },
                    { 7, 1, "M7" },
                    { 8, 1, "M8" },
                    { 9, 1, "M9" },
                    { 10, 1, "M10" },
                    { 11, 2, "O1" },
                    { 12, 2, "O2" },
                    { 13, 2, "O3" },
                    { 14, 2, "O4" },
                    { 15, 2, "O5" },
                    { 16, 2, "O6" },
                    { 17, 2, "O7" },
                    { 18, 2, "O8" },
                    { 19, 2, "O9" },
                    { 20, 2, "O10" },
                    { 21, 2, "B1" },
                    { 22, 2, "B2" },
                    { 23, 2, "B3" },
                    { 24, 2, "B4" },
                    { 25, 2, "B5" },
                    { 26, 2, "B6" },
                    { 27, 2, "B7" },
                    { 28, 2, "B8" },
                    { 29, 2, "B9" },
                    { 30, 2, "B10" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "181413fd-7aa8-4ed4-98f4-a121162b3019" },
                    { "3", "7f284a9c-5141-4f86-ae1c-0be5d6bd226b" },
                    { "1", "8b11af29-826a-47d2-b0ae-706499efdc9a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllTables_areaId",
                table: "AllTables",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_guestId",
                table: "Reservations",
                column: "guestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_sittingId",
                table: "Reservations",
                column: "sittingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedTables_ReservationId",
                table: "ReservedTables",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedTables_tableId",
                table: "ReservedTables",
                column: "tableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ReservedTables");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AllTables");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Sittings");
        }
    }
}
