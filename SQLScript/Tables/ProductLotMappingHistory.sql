/****** Object:  Table [dbo].[ProductLotMappingHistory]    Script Date: 04/23/2019 01:38:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductLotMappingHistory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ProductLotMappingHistory](
	[ProductLotMappingHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [nvarchar](20) NULL,
	[BarcodeStatus] [int] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProductLotMappingHistory] PRIMARY KEY CLUSTERED 
(
	[ProductLotMappingHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
