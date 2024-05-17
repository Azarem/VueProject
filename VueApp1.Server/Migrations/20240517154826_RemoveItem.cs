using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VueApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Items_ItemId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_ItemId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "MenuItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "MenuItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ItemId",
                table: "MenuItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Items_ItemId",
                table: "MenuItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
