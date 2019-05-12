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



USE [AMMK_InventorySystem]
GO
/****** Object:  StoredProcedure [dbo].[stp_srv_SetBarcode]    Script Date: 12-05-2019 12:30:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kaustav Sen>
-- Create date: <22/07/2018>
-- Description:	<To Get Details of All Distributer>
-- Execute: 
	/*DECLARE @Number INT
	EXEC stp_srv_GetLotNumber 8,@Number OUTPUT
	PRINT @Number*/
-- 
-- =============================================
ALTER PROCEDURE [dbo].[stp_srv_SetBarcode]
(
@BarcodeList udttBarcodeGenerationTable READONLY,
@ProductIDforRetreival INT,
@LotNumberforRetreival INT,
@VendorIDforRetreival INT
)
AS
SET NOCOUNT ON;
BEGIN

	

	INSERT ProductDetails (ProductID,Barcode,Deleted,DateEntered) (SELECT VALUE3,VALUE1,0,GETUTCDATE() FROM @BarcodeList)

	INSERT ProductLotMapping (VendorID, ProductID,Barcode,LotNumber,BarcodeStatus,LotDate,Deleted,DateEntered)(SELECT @VendorIDforRetreival,VALUE3,VALUE1,VALUE2,1,GETUTCDATE(),0,GETUTCDATE() FROM @Barcodelist)

	SELECT PM.ProductID,LM.ProductName,PM.Barcode,PM.BarcodeStatus,PM.LotNumber,LotDate FROM ProductLotMapping PM 
	INNER JOIN LottingMaster LM ON PM.ProductID=LM.LotID
	WHERE PM.ProductID=@ProductIDforRetreival AND PM.LotNumber=@LotNumberforRetreival;
	
END
