IF EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = 'udttSetOfNVarcharTwo')
BEGIN
		DROP TYPE dbo.udttSetOfNVarcharTwo;	
END

CREATE TYPE [dbo].[udttSetOfNVarcharTwo] AS TABLE(
	[VALUE1] [NVarchar] NULL,
	[VALUE2] [NVarchar] NULL
)
GO