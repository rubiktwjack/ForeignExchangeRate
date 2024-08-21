USE [ForeignExchangeRate]
GO

/****** Object:  Table [dbo].[CurrencyExchangeRate]    Script Date: 2024/8/17 ¤U¤È 03:02:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrencyExchangeRate](
	[Date] [datetime2](7) NOT NULL,
	[OriginalCurrency] [nchar](3) NOT NULL,
	[TargetCurrency] [nchar](3) NOT NULL,
	[ExchangeRate] [float] NOT NULL
) ON [PRIMARY]
GO


