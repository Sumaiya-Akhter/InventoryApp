using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bool1Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bool2Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bool3Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Int1Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Int2Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Int3Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "String1Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "String2Name",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "String3Name",
                table: "Inventories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bool1Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Bool2Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Bool3Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Int1Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Int2Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Int3Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "String1Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "String2Name",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "String3Name",
                table: "Inventories");
        }
    }
}
