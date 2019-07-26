using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class MakeGuidWorkInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfoContact",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Participant",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Message",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Conversation",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfoContact",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Participant",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Message",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Conversation",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");
        }
    }
}
