using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstagramAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditingMyModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikeNumber",
                table: "T_Like");

            migrationBuilder.DropColumn(
                name: "LikeNumber",
                table: "T_Like");

            migrationBuilder.AddColumn<bool>(
                name: "CommentFlag",
                table: "T_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentFlag",
                table: "T_Post");

            migrationBuilder.AddColumn<int>(
                name: "DislikeNumber",
                table: "T_Like",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikeNumber",
                table: "T_Like",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
