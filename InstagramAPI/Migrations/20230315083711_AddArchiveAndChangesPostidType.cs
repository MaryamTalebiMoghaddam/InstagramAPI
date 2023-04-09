using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstagramAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddArchiveAndChangesPostidType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Comments_T_Post_PostId1",
                table: "T_Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Like_T_Post_PostId1",
                table: "T_Like");

            migrationBuilder.DropIndex(
                name: "IX_T_Like_PostId1",
                table: "T_Like");

            migrationBuilder.DropIndex(
                name: "IX_T_Comments_PostId1",
                table: "T_Comments");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "T_Like");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "T_Comments");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AuthorizedPhoneNumber",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TTL",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ArchivedPost",
                table: "T_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "T_Like",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "T_Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_T_Like_PostId",
                table: "T_Like",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Comments_PostId",
                table: "T_Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Comments_T_Post_PostId",
                table: "T_Comments",
                column: "PostId",
                principalTable: "T_Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Like_T_Post_PostId",
                table: "T_Like",
                column: "PostId",
                principalTable: "T_Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Comments_T_Post_PostId",
                table: "T_Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Like_T_Post_PostId",
                table: "T_Like");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_T_Like_PostId",
                table: "T_Like");

            migrationBuilder.DropIndex(
                name: "IX_T_Comments_PostId",
                table: "T_Comments");

            migrationBuilder.DropColumn(
                name: "AuthorizedPhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TTL",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ArchivedPost",
                table: "T_Post");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "T_Like",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "T_Like",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "T_Comments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "T_Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Like_PostId1",
                table: "T_Like",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_T_Comments_PostId1",
                table: "T_Comments",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Comments_T_Post_PostId1",
                table: "T_Comments",
                column: "PostId1",
                principalTable: "T_Post",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Like_T_Post_PostId1",
                table: "T_Like",
                column: "PostId1",
                principalTable: "T_Post",
                principalColumn: "PostId");
        }
    }
}
