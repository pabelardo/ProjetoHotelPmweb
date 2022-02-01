using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIO.Data.Migrations
{
    public partial class AlteradoNomenclaturaDaForeignKeyDaClasseQuarto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QUARTOS_HOTELS_IdHotel",
                table: "QUARTOS");

            migrationBuilder.RenameColumn(
                name: "IdHotel",
                table: "QUARTOS",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_QUARTOS_IdHotel",
                table: "QUARTOS",
                newName: "IX_QUARTOS_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_QUARTOS_HOTELS_HotelId",
                table: "QUARTOS",
                column: "HotelId",
                principalTable: "HOTELS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QUARTOS_HOTELS_HotelId",
                table: "QUARTOS");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "QUARTOS",
                newName: "IdHotel");

            migrationBuilder.RenameIndex(
                name: "IX_QUARTOS_HotelId",
                table: "QUARTOS",
                newName: "IX_QUARTOS_IdHotel");

            migrationBuilder.AddForeignKey(
                name: "FK_QUARTOS_HOTELS_IdHotel",
                table: "QUARTOS",
                column: "IdHotel",
                principalTable: "HOTELS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
