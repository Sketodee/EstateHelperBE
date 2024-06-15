using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateHelper.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class createdProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pricing_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pricing_Unit = table.Column<int>(type: "int", nullable: false),
                    Pricing_Survey = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pricing_Development = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ImageLinks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
