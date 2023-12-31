USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_updateProduct]    Script Date: 8/16/2023 8:57:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_updateProduct]
    @ProductId SMALLINT,
    @ProductName VARCHAR(50),
    @Price DECIMAL(10, 2),
    @Quantity INT,
    @ModifiedBy VARCHAR(50),
    @ModifiedDate DATETIME,
    @IsActive BIT
AS
BEGIN
IF EXISTS (
        SELECT 1
        FROM products
        WHERE ProductId <> @ProductId AND ProductName = @ProductName
    )
    BEGIN
        SELECT 1 AS IsDuplicate;
    END
    ELSE
	Begin
		 UPDATE Products
		 SET ProductName = @ProductName,
			Price = @Price,
			Quantity = @Quantity,
			ModifiedBy = @ModifiedBy,
			ModifiedDate = @ModifiedDate,
			IsActive = @IsActive
		WHERE ProductId = @ProductId;
		select 0 as IsDuplicate;
    end		
END;