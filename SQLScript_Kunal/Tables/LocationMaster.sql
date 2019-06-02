SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocationMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LocationMaster](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](100) NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DateEntered] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_LocationMaster] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LocationMaster] ADD  CONSTRAINT [DF_LocationMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]

END
GO