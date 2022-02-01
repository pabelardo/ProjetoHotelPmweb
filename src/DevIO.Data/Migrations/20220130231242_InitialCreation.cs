using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIO.Data.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HOTELS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    CNPJ = table.Column<string>(type: "char(14)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Fotos = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOTELS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QUARTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHotel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    NumeroOcupantes = table.Column<int>(type: "int", nullable: false),
                    NumeroDeAdultos = table.Column<int>(type: "int", nullable: false),
                    NumeroDeCriancas = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fotos = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUARTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QUARTOS_HOTELS_IdHotel",
                        column: x => x.IdHotel,
                        principalTable: "HOTELS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QUARTOS_IdHotel",
                table: "QUARTOS",
                column: "IdHotel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QUARTOS");

            migrationBuilder.DropTable(
                name: "HOTELS");
        }
    }
}
