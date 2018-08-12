USE [Payroll]
GO

/****** Object:  Table [dbo].[UserPersonalInformation]    Script Date: 13/08/2018 10:51:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserPersonalInformation](
	[UserPersonalInformationId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [varchar](250) NULL,
	[FirstName] [varchar](250) NULL,
	[LastName] [varchar](250) NULL,
	[Address] [varchar](max) NULL,
	[Position] [varchar](250) NULL,
	[BasicPay] [decimal](18, 2) NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_UserPersonalInformation] PRIMARY KEY CLUSTERED 
(
	[UserPersonalInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


