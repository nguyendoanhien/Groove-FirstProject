using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Contact_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"-- =============================================
				-- Author:		Doan Hien
				-- Create date: 23-7-2019
				-- Description:	Add Contact
				-- =============================================
				CREATE PROCEDURE [dbo].[usp_Contact_Add] 
					-- Add the parameters for the stored procedure here
					@UserId UNIQUEIDENTIFIER, 
					@ContactId UNIQUEIDENTIFIER,
					@CreatedBy NVARCHAR(MAX),
					@NickName NVARCHAR(12)
				AS
				BEGIN
					-- SET NOCOUNT ON added to prevent extra result sets from
					-- interfering with SELECT statements.
					SET NOCOUNT ON;

					-- Insert statements for procedure here
					INSERT INTO [dbo].[UserInfoContact]
		   
	
						   ([CreatedBy]
						   ,[UserId]
						   ,[ContactId]
						   ,[NickName]
						   ,[CreatedOn])
					 VALUES
						   (@CreatedBy
						   ,@UserId
						   ,@ContactId
						   ,@NickName
						   ,GETDATE())
					SELECT @@ROWCOUNT
	
				END
				GO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
				DROP PROCEDURE [dbo].[usp_Contact_Add]
				GO
				"
            );
        }
    }
}