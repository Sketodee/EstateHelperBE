using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateHelper.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addedAccountManagerNameToConsultantGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountManagerId",
                table: "ConsultantGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AccountManagerName",
                table: "ConsultantGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantGroups_AccountManagerId",
                table: "ConsultantGroups",
                column: "AccountManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantGroups_AspNetUsers_AccountManagerId",
                table: "ConsultantGroups",
                column: "AccountManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantGroups_AspNetUsers_AccountManagerId",
                table: "ConsultantGroups");

            migrationBuilder.DropIndex(
                name: "IX_ConsultantGroups_AccountManagerId",
                table: "ConsultantGroups");

            migrationBuilder.DropColumn(
                name: "AccountManagerName",
                table: "ConsultantGroups");

            migrationBuilder.AlterColumn<string>(
                name: "AccountManagerId",
                table: "ConsultantGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
