USE [Payroll]
GO

/****** Object:  Table [dbo].[Income]    Script Date: 13/08/2018 10:51:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Income](
	[IncomeId] [int] IDENTITY(1,1) NOT NULL,
	[IncomeValue] [decimal](18, 2) NULL,
	[IncomeName] [varchar](250) NULL,
	[IncomeDescription] [varchar](250) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Income] PRIMARY KEY CLUSTERED 
(
	[IncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


