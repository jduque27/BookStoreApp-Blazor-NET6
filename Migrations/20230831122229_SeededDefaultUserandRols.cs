using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8343074e-8623-4e1a-b0c1-84fb8678c8f3", null, "User", "USER" },
                    { "c7ac6cfe-1f10-4baf-b604-cde350db9554", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "30a24107-d279-4e37-96fd-01af5b38cb27", 0, "8deb9ecd-036a-4070-8003-538c6bec39f8", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEGg/fQ0VZnJAdKo/b9uZYSrYJ0T9NKT+RL1NKaZcH+qkRZBf6+n60h2/RwvzV0EpZQ==", null, false, "6ab698ba-d5d5-4e02-b751-0c85ce2509bd", false, "user@bookstore.com" },
                    { "8e448afa-f008-446e-a52f-13c449803c2e", 0, "42ce7c23-9468-4c41-81cc-048cea9e9c35", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEHro0tuV2dZclTM1ewGKEmkSf/VDkNLqx0eDkjU162jZXtDx+UHM/fRS1RG6b9tKcA==", null, false, "bad001c0-5bbd-44ed-af79-fdb34399a086", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8343074e-8623-4e1a-b0c1-84fb8678c8f3", "30a24107-d279-4e37-96fd-01af5b38cb27" },
                    { "c7ac6cfe-1f10-4baf-b604-cde350db9554", "8e448afa-f008-446e-a52f-13c449803c2e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8343074e-8623-4e1a-b0c1-84fb8678c8f3", "30a24107-d279-4e37-96fd-01af5b38cb27" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7ac6cfe-1f10-4baf-b604-cde350db9554", "8e448afa-f008-446e-a52f-13c449803c2e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8343074e-8623-4e1a-b0c1-84fb8678c8f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7ac6cfe-1f10-4baf-b604-cde350db9554");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e");
        }
    }
}
