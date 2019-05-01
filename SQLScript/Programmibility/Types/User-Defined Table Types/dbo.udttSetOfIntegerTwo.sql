IF EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = 'udttSetOfIntegerTwo')
BEGIN
		DROP TYPE dbo.udttSetOfIntegerTwo;	
END

CREATE TYPE [dbo].[udttSetOfIntegerTwo] AS TABLE(
	[ID1] [int] NULL,
	[ID2] [int] NULL
)
GO