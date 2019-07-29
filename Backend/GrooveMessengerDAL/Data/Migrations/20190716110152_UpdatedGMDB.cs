using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class UpdatedGMDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "DisplayName",
                "AspNetUsers",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                "Conversation",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50),
                    Avatar = table.Column<string>(maxLength: 50)
                },
                constraints: table => { table.PrimaryKey("PK_Conversation", x => x.Id); });

            migrationBuilder.CreateTable(
                "UserInfo",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Mood = table.Column<string>(maxLength: 150, nullable: true),
                    Status = table.Column<int>(),
                    Avatar = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        "FK_UserInfo_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Message",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    ConversationId = table.Column<Guid>(),
                    SenderId = table.Column<string>(),
                    SeenBy = table.Column<string>(nullable: true),
                    Content = table.Column<string>(maxLength: 1000),
                    Type = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        "FK_Message_Conversation_ConversationId",
                        x => x.ConversationId,
                        "Conversation",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Message_AspNetUsers_SenderId",
                        x => x.SenderId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Participant",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    ConversationId = table.Column<Guid>(),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                    table.ForeignKey(
                        "FK_Participant_Conversation_ConversationId",
                        x => x.ConversationId,
                        "Conversation",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Participant_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserInfoContact",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Deleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(),
                    ContactId = table.Column<Guid>(),
                    NickName = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoContact", x => x.Id);
                    table.ForeignKey(
                        "FK_UserInfoContact_UserInfo_ContactId",
                        x => x.ContactId,
                        "UserInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_UserInfoContact_UserInfo_UserId",
                        x => x.UserId,
                        "UserInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Message_ConversationId",
                "Message",
                "ConversationId");

            migrationBuilder.CreateIndex(
                "IX_Message_SenderId",
                "Message",
                "SenderId");

            migrationBuilder.CreateIndex(
                "IX_Participant_ConversationId",
                "Participant",
                "ConversationId");

            migrationBuilder.CreateIndex(
                "IX_Participant_UserId",
                "Participant",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserInfo_UserId",
                "UserInfo",
                "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_UserInfoContact_ContactId",
                "UserInfoContact",
                "ContactId");

            migrationBuilder.CreateIndex(
                "IX_UserInfoContact_UserId",
                "UserInfoContact",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Message");

            migrationBuilder.DropTable(
                "Participant");

            migrationBuilder.DropTable(
                "UserInfoContact");

            migrationBuilder.DropTable(
                "Conversation");

            migrationBuilder.DropTable(
                "UserInfo");

            migrationBuilder.AlterColumn<string>(
                "DisplayName",
                "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 120);
        }
    }
}