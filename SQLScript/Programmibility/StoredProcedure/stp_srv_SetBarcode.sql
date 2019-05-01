IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_SetBarcode')
	DROP PROCEDURE [dbo].[stp_srv_SetBarcode]
GO
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
CREATE PROCEDURE [dbo].[stp_srv_SetBarcode]
(
@BarcodeList udttBarcodeGenerationTable READONLY,
@ProductIDforRetreival INT,
@LotNumberforRetreival INT
)
AS
SET NOCOUNT ON;
BEGIN

	

	INSERT ProductDetails (ProductID,Barcode,Deleted,DateEntered) (SELECT VALUE3,VALUE1,0,GETUTCDATE() FROM @BarcodeList)

	INSERT ProductLotMapping (ProductID,Barcode,LotNumber,BarcodeStatus,LotDate,Deleted,DateEntered)(SELECT VALUE3,VALUE1,VALUE2,1,GETUTCDATE(),0,GETUTCDATE() FROM @Barcodelist)

	SELECT ProductID,Barcode,BarcodeStatus,LotNumber,LotDate FROM ProductLotMapping WHERE ProductID=@ProductIDforRetreival AND LotNumber=@LotNumberforRetreival;
	
	
END
GO