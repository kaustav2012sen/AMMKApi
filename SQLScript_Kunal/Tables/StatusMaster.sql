/****** Object:  Table [dbo].[StatusMaster]    Script Date: 04/23/2019 01:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StatusMaster](
	[StatusID] [int] NOT NULL,
	[StatusName] [nvarchar](30) NOT NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_StatusMaster] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[StatusMaster] ([StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (1,N'Barcode Generated', 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[StatusMaster] ( [StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (2,N'Stock Out', 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[StatusMaster] ( [StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (3,N'Stock In', 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[StatusMaster] ( [StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (4,N'Sold', 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[StatusMaster] ( [StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (5,N'Return Stock', 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[StatusMaster] ([StatusID],[StatusName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (6,N'Return Vendor', 0, NULL, NULL, NULL, NULL)
--/****** Object:  Default [DF_StatusMaster_Deleted]    Script Date: 04/23/2019 01:28:26 ******/
ALTER TABLE [dbo].[StatusMaster] ADD  CONSTRAINT [DF_StatusMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
GO
