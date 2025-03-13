using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreEntity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_folders_parent_folder_id",
                table: "folders",
                column: "parent_folder_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_versions_file_id",
                table: "file_versions",
                column: "file_id");

            migrationBuilder.AddForeignKey(
                name: "fk_file_versions_files_file_id",
                table: "file_versions",
                column: "file_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_folders_folders_parent_folder_id",
                table: "folders",
                column: "parent_folder_id",
                principalTable: "folders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_file_versions_files_file_id",
                table: "file_versions");

            migrationBuilder.DropForeignKey(
                name: "fk_folders_folders_parent_folder_id",
                table: "folders");

            migrationBuilder.DropIndex(
                name: "ix_folders_parent_folder_id",
                table: "folders");

            migrationBuilder.DropIndex(
                name: "ix_file_versions_file_id",
                table: "file_versions");
        }
    }
}
