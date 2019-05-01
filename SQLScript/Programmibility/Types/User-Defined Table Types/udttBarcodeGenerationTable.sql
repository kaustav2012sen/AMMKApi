IF EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = 'udttBarcodeGenerationTable')
BEGIN
		DROP TYPE dbo.udttBarcodeGenerationTable;	
END

CREATE TYPE [dbo].[udttBarcodeGenerationTable] AS TABLE(
	[VALUE1] [NVarchar](20) NULL,
	[VALUE2] [int] NULL,
	[VALUE3] [int] NULL
)
GO