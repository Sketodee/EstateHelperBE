using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateHelper.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class createdProductEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pricing_Development",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Pricing_Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Pricing_Survey",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Pricing_Unit",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Survey = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Development = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Pricing_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_Development",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing_Survey",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pricing_Unit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
