SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VendorMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VendorMaster](
	[VendorID] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](255) NULL,
	[LocationID] [int] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_VendorMaster] PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[VendorMaster] ADD  CONSTRAINT [DF_VendorMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]
ALTER TABLE [dbo].[VendorMaster] ADD CONSTRAINT [FK_VendorMaster_LocationMaster] FOREIGN KEY (LocationID) REFERENCES LocationMaster(LocationID)
END
GO