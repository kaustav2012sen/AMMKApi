/****** Object:  Table [dbo].[ProductLotMapping]    Script Date: 04/23/2019 01:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductLotMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductLotMapping](
	[ProductLotMappingID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[VendorID] [int] NULL,
	[Barcode] [nvarchar](20) NULL,
	[BarcodeStatus] [int] NOT NULL,
	[LotNumber] [int] NOT NULL,
	[LotDate] [datetime] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProductLotMapping] PRIMARY KEY CLUSTERED 
(
	[ProductLotMappingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ProductLotMapping] ADD  CONSTRAINT [DF_ProductLotMapping_Deleted]  DEFAULT ((0)) FOR [Deleted]
ALTER TABLE [dbo].[ProductLotMapping] ADD CONSTRAINT [FK_ProductLotMapping_VendorMaster] FOREIGN KEY (VendorID) REFERENCES VendorMaster(VendorID)
END
GO