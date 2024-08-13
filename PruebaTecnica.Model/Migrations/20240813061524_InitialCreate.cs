using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PruebaTecnica.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParametrosSensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoParametro = table.Column<long>(type: "bigint", nullable: false),
                    DescripcionLarga = table.Column<string>(type: "text", nullable: false),
                    DescripcionMedia = table.Column<string>(type: "text", nullable: false),
                    DescripcionCorta = table.Column<string>(type: "text", nullable: false),
                    Abreviatura = table.Column<string>(type: "text", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Unidad = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosSensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatosSensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoParametro = table.Column<long>(type: "bigint", nullable: false),
                    ParametroSensoresId = table.Column<int>(type: "integer", nullable: false),
                    NombreParametro = table.Column<string>(type: "text", nullable: false),
                    FechaDato = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValorNumero = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosSensores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosSensores_ParametrosSensores_ParametroSensoresId",
                        column: x => x.ParametroSensoresId,
                        principalTable: "ParametrosSensores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatosSensores_ParametroSensoresId",
                table: "DatosSensores",
                column: "ParametroSensoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosSensores");

            migrationBuilder.DropTable(
                name: "ParametrosSensores");
        }
    }
}
