using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateHelper.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addedEmailToConsultantGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ConsultantGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ConsultantGroups");
        }
    }
}
