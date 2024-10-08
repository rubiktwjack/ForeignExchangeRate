USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCurrency]    
	@Currency	nchar(3),
	@Name		nvarchar(10)
AS
BEGIN
    INSERT INTO [dbo].[CurrencyNameMapping]
           ([Currency]
           ,[Name])
     VALUES
           (@Currency
           ,@Name)
END
