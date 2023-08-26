using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore6.Migrations
{
    /// <inheritdoc />
    public partial class AddTbLoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaLoai",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_MaLoai",
                table: "Book",
                column: "MaLoai");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Loai_MaLoai",
                table: "Book",
                column: "MaLoai",
                principalTable: "Loai",
                principalColumn: "MaLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Loai_MaLoai",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropIndex(
                name: "IX_Book_MaLoai",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "MaLoai",
                table: "Book");
        }
    }
}
