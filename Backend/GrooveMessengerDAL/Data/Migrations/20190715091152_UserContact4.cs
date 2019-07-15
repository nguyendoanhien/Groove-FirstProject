using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class UserContact4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfoContact_UserInfo_ContactId",
                table: "UserInfoContact");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfoContact_UserInfo_UserId",
                table: "UserInfoContact");

            migrationBuilder.DropIndex(
                name: "IX_UserInfoContact_ContactId",
                table: "UserInfoContact");

            migrationBuilder.DropIndex(
                name: "IX_UserInfoContact_UserId",
                table: "UserInfoContact");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInfoContact",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "UserInfoContact",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoId",
                table: "UserInfoContact",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoContact_UserInfoId",
                table: "UserInfoContact",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfoContact_UserInfo_Id",
                table: "UserInfoContact",
                column: "Id",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfoContact_UserInfo_UserInfoId",
                table: "UserInfoContact",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfoContact_UserInfo_Id",
                table: "UserInfoContact");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfoContact_UserInfo_UserInfoId",
                table: "UserInfoContact");

            migrationBuilder.DropIndex(
                name: "IX_UserInfoContact_UserInfoId",
                table: "UserInfoContact");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "UserInfoContact");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInfoContact",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "UserInfoContact",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoContact_ContactId",
                table: "UserInfoContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoContact_UserId",
                table: "UserInfoContact",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfoContact_UserInfo_ContactId",
                table: "UserInfoContact",
                column: "ContactId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfoContact_UserInfo_UserId",
                table: "UserInfoContact",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
