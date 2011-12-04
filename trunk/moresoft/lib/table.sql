USE [moresoftseo]
GO
/****** 对象:  Table [dbo].[InfoType_ift]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoType_ift](
	[ID_ift] [int] IDENTITY(1,1) NOT NULL,
	[Name_ift] [nvarchar](50) NULL,
	[Code_ift] [nvarchar](50) NULL,
	[Url_ift] [nvarchar](50) NULL,
	[Sort_ift] [int] NULL,
	[Parent_ift] [int] NULL,
	[Path_ift] [int] NULL,
	[IsHide_ift] [bit] NULL,
	[Keywords_ift] [nvarchar](50) NULL,
 CONSTRAINT [PK_InfoType_ift] PRIMARY KEY CLUSTERED 
(
	[ID_ift] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Info_inf]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Info_inf](
	[ID_inf] [int] IDENTITY(1,1) NOT NULL,
	[InfoTypeID_inf] [int] NULL,
	[Title_inf] [nvarchar](100) NULL,
	[Content_inf] [nvarchar](max) NULL,
	[Hits_inf] [int] NULL,
	[TopType_inf] [nvarchar](50) NULL,
	[IndexTagID_inf] [int] NULL,
	[CreateTime_inf] [datetime] NULL,
	[Author_inf] [nvarchar](50) NULL,
	[Keywords_inf] [nvarchar](50) NULL,
 CONSTRAINT [PK_Info_inf] PRIMARY KEY CLUSTERED 
(
	[ID_inf] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[IndexTag_itg]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IndexTag_itg](
	[ID_itg] [int] IDENTITY(1,1) NOT NULL,
	[Name_itg] [nvarchar](50) NULL,
	[Sort_itg] [int] NULL,
 CONSTRAINT [PK_IndexTag_itg] PRIMARY KEY CLUSTERED 
(
	[ID_itg] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** 对象:  Table [dbo].[mc_User]    脚本日期: 12/04/2011 21:00:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mc_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[DeptID] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_mc_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
