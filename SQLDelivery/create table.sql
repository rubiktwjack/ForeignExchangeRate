USE [ForeignExchangeRate]
GO

/****** Object:  Table [dbo].[CurrencyNameMapping]    Script Date: 2024/8/17 ¤U¤È 02:40:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrencyNameMapping](
	[Currency] [nchar](3) NOT NULL,
	[Name] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO


