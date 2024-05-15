using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_API__Avancerad.NET.Migrations
{
    /// <inheritdoc />
    public partial class InitalizeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
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
                name: "ChangeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
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
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1, "admin@example.com", "Admin123" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "kontakt@gekas.se", "Gekås", "Test123" },
                    { 2, "kontakt@ica.se", "ICA", "Test123" },
                    { 3, "kontakt@coop.se", "Coop", "Test123" },
                    { 4, "kontakt@campus.se", "Campus", "Test123" },
                    { 5, "kontakt@elgiganten.se", "Elgiganten", "Test123" },
                    { 6, "kontakt@netonnet.se", "Net On Net", "Test123" },
                    { 7, "kontakt@campino.se", "Campino", "Test123" },
                    { 8, "kontakt@dressman.se", "Dressman", "Test123" },
                    { 9, "kontakt@lindex.se", "Lindex", "Test123" },
                    { 10, "kontakt@hm.se", "H&M", "Test123" },
                    { 11, "kontakt@volvo.se", "Volvo", "Test123" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, "olof@test.com", "Olof Sandberg", "Test123", "+46701234567" },
                    { 2, "jonatan@test.com", "Jonatan Nordin", "Test123", "+46706543210" },
                    { 3, "christian@test.com", "Christian Rapp", "Test123", "+46709876543" },
                    { 4, "anna@test.com", "Anna Söderberg", "Test123", "+46702345678" },
                    { 5, "nina@test.com", "Nina Lindberg Nilsson", "Test123", "+46707654321" },
                    { 6, "par@test.com", "Pär Sandberg", "Test123", "+46701123456" },
                    { 7, "tobias@test.com", "Tobias Qlok", "Test123", "+46702234567" },
                    { 8, "reidar@test.com", "Reidar Qlok", "Test123", "+46703345678" },
                    { 9, "pepsi@test.com", "Pepsi Sandberg", "Test123", "+46704456789" },
                    { 10, "miki@test.com", "Miki Vidacic", "Test123", "+46705567890" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CompanyId", "CustomerId", "Description", "DurationInMinutes", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 2, 1, "Technical support session", 90, new DateTime(2024, 5, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Support Session" },
                    { 2, 2, 2, "Scheduled data backup", 120, new DateTime(2024, 5, 7, 14, 30, 0, 0, DateTimeKind.Unspecified), "Data Backup" },
                    { 3, 6, 3, "Consultation for website development", 60, new DateTime(2024, 5, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), "Website Consultation" },
                    { 4, 1, 4, "Upgrade network infrastructure", 90, new DateTime(2024, 5, 9, 15, 30, 0, 0, DateTimeKind.Unspecified), "Network Upgrade" },
                    { 5, 5, 5, "Training session for new software", 120, new DateTime(2024, 5, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Software Training" },
                    { 6, 9, 6, "Consultation for IT infrastructure", 90, new DateTime(2024, 5, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), "IT Consultation" },
                    { 7, 7, 7, "Upgrading system components", 120, new DateTime(2024, 5, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), "System Upgrade" },
                    { 8, 8, 8, "Optimizing database performance", 60, new DateTime(2024, 5, 13, 10, 30, 0, 0, DateTimeKind.Unspecified), "Database Optimization" },
                    { 9, 9, 9, "Comprehensive security audit", 90, new DateTime(2024, 5, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), "Security Audit" },
                    { 10, 10, 10, "Installing new software", 120, new DateTime(2024, 5, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), "Software Installation" },
                    { 11, 3, 2, "Technical meeting for project planning", 90, new DateTime(2024, 5, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), "Technical Meeting" },
                    { 12, 4, 4, "Demonstration of new products", 120, new DateTime(2024, 5, 17, 14, 30, 0, 0, DateTimeKind.Unspecified), "Product Demo" },
                    { 13, 6, 6, "Routine maintenance check", 60, new DateTime(2024, 5, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), "Maintenance Check" },
                    { 14, 7, 7, "Kickoff meeting for new project", 90, new DateTime(2024, 5, 19, 15, 30, 0, 0, DateTimeKind.Unspecified), "Project Kickoff" },
                    { 15, 8, 9, "Training session for staff", 120, new DateTime(2024, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "Training Session" },
                    { 16, 9, 10, "Meeting with potential client", 90, new DateTime(2024, 5, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), "Client Meeting" },
                    { 17, 10, 1, "Review of current systems", 120, new DateTime(2024, 5, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), "System Review" },
                    { 18, 1, 3, "Consultation for future projects", 60, new DateTime(2024, 5, 23, 10, 30, 0, 0, DateTimeKind.Unspecified), "Consultation" },
                    { 19, 2, 5, "Session to gather client feedback", 90, new DateTime(2024, 5, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), "Feedback Session" },
                    { 20, 3, 8, "Meeting to discuss company strategy", 120, new DateTime(2024, 5, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), "Strategy Meeting" },
                    { 21, 4, 7, "Planning session for upcoming projects", 90, new DateTime(2024, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Planning Session" },
                    { 22, 5, 6, "Review of company budget", 120, new DateTime(2024, 6, 3, 14, 30, 0, 0, DateTimeKind.Unspecified), "Budget Review" },
                    { 23, 6, 4, "Review of company performance", 60, new DateTime(2024, 6, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "Performance Review" },
                    { 24, 7, 3, "Meeting to discuss project details", 90, new DateTime(2024, 6, 7, 15, 30, 0, 0, DateTimeKind.Unspecified), "Project Meeting" },
                    { 25, 8, 2, "Presentation to potential clients", 120, new DateTime(2024, 6, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "Client Presentation" },
                    { 26, 9, 1, "Meeting with project team", 90, new DateTime(2024, 6, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), "Team Meeting" },
                    { 27, 10, 10, "Review of technical aspects", 120, new DateTime(2024, 6, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), "Technical Review" },
                    { 28, 1, 9, "Meeting to discuss project progress", 60, new DateTime(2024, 6, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "Progress Meeting" },
                    { 29, 2, 8, "Update meeting with client", 90, new DateTime(2024, 6, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), "Client Update" },
                    { 30, 3, 7, "Training session for staff", 120, new DateTime(2024, 6, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), "Staff Training" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CompanyId",
                table: "Appointments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

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
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
