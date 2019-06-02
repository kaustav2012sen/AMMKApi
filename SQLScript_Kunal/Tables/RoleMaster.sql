/****** Object:  Table [dbo].[RoleMaster]    Script Date: 04/23/2019 00:27:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[RoleMaster](
		[RoleID] [int] IDENTITY(1,1) NOT NULL,
		[RoleName] [nvarchar](30) NOT NULL,
		[Deleted] [bit] NULL,
		[CreatedBy] [nvarchar](100) NULL,
		[DateEntered] [datetime] NULL,
		[ModifiedBy] [nvarchar](100) NULL,
		[DateModified] [datetime] NULL,
	 CONSTRAINT [PK_RoleMaster] PRIMARY KEY CLUSTERED 
	(
		[RoleID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[RoleMaster] ADD  CONSTRAINT [DF_RoleMaster_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
GO