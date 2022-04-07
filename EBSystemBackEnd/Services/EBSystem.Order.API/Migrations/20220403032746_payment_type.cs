using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBSystem.Order.API.Migrations
{
    public partial class payment_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "paymentTypes",
                columns: table => new
                {
                    paymentTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentTypes", x => x.paymentTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentTypes");
        }
    }
}
