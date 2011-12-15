USE [moresoftseo]
GO
alter	table   InfoType_ift   add   Keywords_ift     nvarchar(50);
alter	table   Info_inf   add   Author_inf     nvarchar(50);
alter	table   Info_inf   add   Keywords_inf     nvarchar(50);
GO
/****** 对象:  Table [dbo].[Page_pag]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page_pag](
	[ID_pag] [int] IDENTITY(1,1) NOT NULL,
	[Name_pag] [nvarchar](50) NULL,
	[Code_pag] [nvarchar](50) NULL,
	[Content_pag] [nvarchar](max) NULL,
	[Sort_pag] [int] NULL,
 CONSTRAINT [PK_Page_pag] PRIMARY KEY CLUSTERED 
(
	[ID_pag] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Setting_set]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting_set](
	[ID_set] [int] IDENTITY(1,1) NOT NULL,
	[WebName_set] [nvarchar](50) NULL,
	[WebUrl_set] [nvarchar](50) NULL,
	[Title_set] [nvarchar](50) NULL,
	[Keywords_set] [nvarchar](50) NULL,
	[Author_set] [nvarchar](50) NULL,
	[ICP_set] [nvarchar](50) NULL,
 CONSTRAINT [PK_Setting_set] PRIMARY KEY CLUSTERED 
(
	[ID_set] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
insert into Setting_set(WebName_set,WebUrl_set,Title_set,Keywords_set,Author_set)
values('摩尔社区-国内最专业的MES服务商','http://mes.moresoft.com.cn','摩尔社区-国内最专业的MES服务商','摩尔软件|摩尔社区|mes','摩尔软件');