using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class FixUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "UserInfoContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    ContactId = table.Column<string>(nullable: false),
                    NickName = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfoContact_UserInfo_ContactId",
                        column: x => x.ContactId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfoContact_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoContact_ContactId",
                table: "UserInfoContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoContact_UserId",
                table: "UserInfoContact",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_Id",
                table: "UserInfo",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_Id",
                table: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserInfoContact");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
