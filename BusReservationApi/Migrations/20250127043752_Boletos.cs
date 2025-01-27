using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservationApi.Migrations
{
    /// <inheritdoc />
    public partial class Boletos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    NombrePasajero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaViaje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    EstadoReserva = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_ReservaId",
                table: "Boletos",
                column: "ReservaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");
        }
    }
}
