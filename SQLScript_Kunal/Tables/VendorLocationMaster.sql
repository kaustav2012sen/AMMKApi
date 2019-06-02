SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VendorLocationMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VendorLocationMaster](
	[VendorLocationID] [int] IDENTITY(1,1) NOT NULL,
	[VendorLocationName] [nvarchar](255) NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_VendorLocationMaster] PRIMARY KEY CLUSTERED 
(
	[VendorLocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[VendorLocationMaster] ADD  CONSTRAINT [DF_VendorLocationMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]

SET IDENTITY_INSERT [dbo].[VendorLocationMaster] ON
INSERT [dbo].[VendorLocationMaster] ([VendorLocationID], [VendorLocationName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (1, N'LOCAL', 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[VendorLocationMaster] ([VendorLocationID], [VendorLocationName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (2, N'VARANASI', 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[VendorLocationMaster] ([VendorLocationID], [VendorLocationName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (3, N'BANGALORE', 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[VendorLocationMaster] ([VendorLocationID], [VendorLocationName], [Deleted], [CreatedBy], [DateEntered], [ModifiedBy], [DateModified]) VALUES (4, N'OTHERS', 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[VendorLocationMaster] OFF

END
GO