USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductDetailsById]    Script Date: 8/16/2023 8:57:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[sp_getProductDetailsById]
 @ProductId SmaLLINT
 As
 Begin
 select * from Products where ProductId = @ProductId;
 end