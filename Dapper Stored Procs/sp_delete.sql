USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete]    Script Date: 8/16/2023 8:56:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_delete]
 @ProductId SMALLINT
AS
BEGIN
	Delete from Products 
	where ProductId= @ProductId;

END