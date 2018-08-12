USE [Payroll]
GO

/****** Object:  Table [dbo].[Deduction]    Script Date: 13/08/2018 10:50:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Deduction](
	[DeductionId] [int] IDENTITY(1,1) NOT NULL,
	[DeductionValue] [decimal](18, 2) NULL,
	[DeductionName] [varchar](250) NULL,
	[DeductionDescription] [varchar](250) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_Deduction] PRIMARY KEY CLUSTERED 
(
	[DeductionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


