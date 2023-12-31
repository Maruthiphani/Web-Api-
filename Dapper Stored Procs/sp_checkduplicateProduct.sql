USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_checkDuplicateProduct]    Script Date: 8/16/2023 8:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_checkDuplicateProduct]
    @ProductId INT,
    @ProductName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM products
        WHERE ProductId <> @ProductId AND ProductName = @ProductName
    )
    BEGIN
        SELECT 1 AS IsDuplicate;
    END
    ELSE
    BEGIN
        SELECT 0 AS IsDuplicate;
    END
END