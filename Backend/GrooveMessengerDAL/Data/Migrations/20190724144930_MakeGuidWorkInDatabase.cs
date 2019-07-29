using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class MakeGuidWorkInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "UserInfoContact",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "UserInfo",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Participant",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Message",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Conversation",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "UserInfoContact",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "UserInfo",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Participant",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Message",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                "Id",
                "Conversation",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");
        }
    }
}