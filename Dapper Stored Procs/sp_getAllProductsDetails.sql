USE [CRUDwithWebApi1]
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllProductsDetails]    Script Date: 8/16/2023 8:57:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 ALTER Procedure [dbo].[sp_getAllProductsDetails]
 
 As
 Begin
 select * from Products
 end