SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DATA_LogOfDBOperation](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL DEFAULT (newid()),
	[Logdate] [datetime] NOT NULL DEFAULT (getdate()),
	[Operator] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Note] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_DATA_LogOfDBOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[DATA_LogDetailOfDBOperation](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [DF__DATA_LogDeta__ID__164452B1]  DEFAULT (newid()),
	[DATA_LogOfDBOperationID] [uniqueidentifier] NOT NULL,
	[TableName] [sysname] COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Description] [xml] NULL,
	[OperationType] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Flag] [bit] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_DATA_LogDetailOfDBOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DATA_LogDetailOfDBOperation]  WITH CHECK ADD  CONSTRAINT [FK_DATA_LogDetailOfDBOperation_DATA_LogOfDBOperationID] FOREIGN KEY([DATA_LogOfDBOperationID])
REFERENCES [dbo].[DATA_LogOfDBOperation] ([ID])
GO
ALTER TABLE [dbo].[DATA_LogDetailOfDBOperation] CHECK CONSTRAINT [FK_DATA_LogDetailOfDBOperation_DATA_LogOfDBOperationID]