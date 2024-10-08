USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertCurrencyExchangeRate]
    @Date				datetime2(7),
	@OriginalCurrency	nchar(3),
	@TargetCurrency		nchar(3),
	@ExchangeRate		float
AS
BEGIN
    INSERT INTO [dbo].[CurrencyExchangeRate]
           ([Date]
           ,[OriginalCurrency]
           ,[TargetCurrency]
           ,[ExchangeRate])
     VALUES
           (@Date
           ,@OriginalCurrency
           ,@TargetCurrency
           ,@ExchangeRate)
END
