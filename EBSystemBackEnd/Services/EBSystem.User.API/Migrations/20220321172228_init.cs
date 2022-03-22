using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBSystem.Authentication.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "logins",
                columns: new[] { "Id", "Password", "RefreshToken", "RefreshTokenExpiryTime", "UserName" },
                values: new object[] { 1L, "Gowtham@123", null, null, "Gowtham" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logins");
        }
    }
}
