using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorToInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Inventories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CreatorId",
                table: "Inventories",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_CreatorId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Inventories");
        }
    }
}
