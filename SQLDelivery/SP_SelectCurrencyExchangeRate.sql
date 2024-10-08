USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCurrencyExchangeRate]
    @QueryStartTime		datetime2(7),
	@QueryEndTime		datetime2(7),
	@TargetCurrency		nchar(3)
AS
BEGIN
	SELECT [Date]
      ,[OriginalCurrency]
      ,[TargetCurrency]
      ,[ExchangeRate]	  
	FROM [ForeignExchangeRate].[dbo].[CurrencyExchangeRate] as rate	
	WHERE [Date]>=@QueryStartTime and [Date]<=@QueryEndTime and TargetCurrency=@TargetCurrency

END
