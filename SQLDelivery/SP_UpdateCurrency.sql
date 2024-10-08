USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCurrency]    
	@Currency	nchar(3),
	@Name		nvarchar(10)
AS
BEGIN
    UPDATE [dbo].[CurrencyNameMapping]
	SET [Currency] = @Currency
		,[Name] = @Name
	WHERE [Currency]=@Currency or [Name]=@Name
END
