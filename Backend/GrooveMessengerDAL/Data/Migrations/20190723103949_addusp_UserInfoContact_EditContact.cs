﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_UserInfoContact_EditContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		Linh Tran
				-- Create date: 2019-07-23
				-- Description:	Edit contact
                -- =============================================
                CREATE PROCEDURE [dbo].[usp_UserInfoContact_EditContact]
	                -- Add the parameters for the stored procedure here
	                @Nickname nvarchar(120) ,-- This is an string
                	@Id uniqueidentifier --this is a number
                AS
                BEGIN
	                SET NOCOUNT ON;
		
	                UPDATE UserInfoContact
					SET UserInfoContact.NickName = @Nickname
					WHERE UserInfoContact.Id = @Id

                END
                GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_UserInfoContact_EditContact]
                GO
                "
                );
        }
    }
}
