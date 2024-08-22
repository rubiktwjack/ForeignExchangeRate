USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCurrencyNameMapping]
 AS
BEGIN
	SELECT [Currency],[Name]
  FROM [ForeignExchangeRate].[dbo].[CurrencyNameMapping]

END
