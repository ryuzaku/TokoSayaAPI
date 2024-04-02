using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokoSayaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddItemId_ChangeHargaDataTypeToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Harga",
                table: "Produk",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Produk",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Produk");

            migrationBuilder.AlterColumn<float>(
                name: "Harga",
                table: "Produk",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
