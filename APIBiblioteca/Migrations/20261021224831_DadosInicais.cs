using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIBiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class DadosInicais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Autor", "Emprestado", "Titulo" },
                values: new object[,]
                {
                    { 1, "Machado de Assis", false, "Dom Casmurro" },
                    { 2, "Antoine de Saint-Exupery", false, "O Pequeno Principe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
