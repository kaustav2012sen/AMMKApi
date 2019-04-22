SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_mst_StockMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tbl_mst_StockMaster](
		[ID] [int] NOT NULL,
		[Barcode] [nvarchar](50) NOT NULL,
		[Status] [int] NOT NULL,
		[Deleted] [bit] NULL,
		[CreatedBy] [nvarchar](100) NULL,
		[DateEntered] [datetime] NULL,
		[ModifiedBy] [nvarchar](100) NULL,
		[DateModified] [datetime] NULL,
	 CONSTRAINT [PK_tbl_mst_StockMaster] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[tbl_mst_StockMaster] ADD  CONSTRAINT [DF_tbl_mst_StockMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
GO
