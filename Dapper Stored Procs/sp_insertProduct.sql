USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_insertProduct]    Script Date: 8/16/2023 8:57:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_insertProduct]
    @ProductName VARCHAR(50),
    @Price DECIMAL(10, 2),
    @Quantity INT,
    @CreatedBy VARCHAR(50),
    @CreatedDate DATETIME,
    @IsActive BIT
AS
BEGIN
	IF EXISTS(	SELECT 1 FROM Products WHERE ProductName = @ProductName)
	BEGIN
		SELECT 1 as isduplicate
	end
	else
	begin
		INSERT INTO Products (ProductName, Price, Quantity, CreatedBy, CreatedDate, IsActive)
		VALUES (@ProductName, @Price, @Quantity, @CreatedBy, @CreatedDate, @IsActive);
		
		select 0 as isduplicate
	end
END;