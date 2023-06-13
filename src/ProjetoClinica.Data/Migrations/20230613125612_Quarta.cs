using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoClinica.Data.Migrations
{
    /// <inheritdoc />
    public partial class Quarta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "Apagado",
                table: "Usuario",
                type: "BIT",
                maxLength: 1,
                nullable: false,
                defaultValue: 0ul);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apagado",
                table: "Usuario");
        }
    }
}
