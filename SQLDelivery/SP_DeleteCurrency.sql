USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCurrency]    
	@Currency	nchar(3)
AS
BEGIN
	
    DELETE FROM [dbo].[CurrencyNameMapping]
      WHERE [Currency]=@Currency

END
