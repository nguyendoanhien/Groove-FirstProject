﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Message_SetValueSeenBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		TrucPhan
                -- Create date: 24-07-2019
                -- Description:	
                -- =============================================
                CREATE PROCEDURE [dbo].[usp_Message_SetValueSeenBy] 
	                -- Add the parameters for the stored procedure here
	                @userId uniqueIdentifier, 
	                @conversationId uniqueIdentifier 
                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;

                    -- Insert statements for procedure here
	                DECLARE @SeenBy TABLE (Id uniqueIdentifier)
	                INSERT INTO @SeenBy SELECT Id
	                FROM Message 
	                WHERE ConversationId = @conversationId AND SenderId !=@userId
	                AND isnull(SeenBy,'') NOT LIKE '%'+cast(@userId as nvarchar(255))+'%'
	                DECLARE @result int
	                SET @result = 0
	                WHILE(1=1)
	                BEGIN
	                 DECLARE @i uniqueIdentifier 
	                 SET @i = (SELECT TOP(1) Id FROM @SeenBy)
		                UPDATE Message
		                SET SeenBy = cast(@userId as nvarchar(255))
		                WHERE @i = Message.Id

		                IF @i IS NULL
			                BREAK

		                DELETE TOP(1) FROM @SeenBy
		                SET @result = @result + 1
	                --WHERE ConversationId = @ConversationId AND SenderId !=@UserId
	                --AND isnull(SeenBy,'') NOT LIKE '%'+cast(@UserId as nvarchar(255))+'%'
	                END
	                select @result
                END
                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                 @"
                 DROP PROCEDURE [dbo].[usp_Message_SetValueSeenBy]
                 GO
                 "
                );
        }
    }
}
