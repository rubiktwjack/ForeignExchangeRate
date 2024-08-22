USE [ForeignExchangeRate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrencyNameMapping](
	[Currency] [nchar](3) NOT NULL,
	[Name] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CurrencyExchangeRate](
	[Date] [datetime2](7) NOT NULL,
	[OriginalCurrency] [nchar](3) NOT NULL,
	[TargetCurrency] [nchar](3) NOT NULL,
	[ExchangeRate] [float] NOT NULL
) ON [PRIMARY]
GO



