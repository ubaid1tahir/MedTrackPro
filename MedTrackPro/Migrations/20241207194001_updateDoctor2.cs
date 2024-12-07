using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedTrackPro.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_CategoryId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CategoryId",
                table: "Doctors",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_CategoryId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CategoryId",
                table: "Doctors",
                column: "CategoryId",
                unique: true);
        }
    }
}
