IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_StockAction')
	DROP PROCEDURE [dbo].[stp_srv_StockAction]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kunal Mukherjee>
-- Create date: <01/05/2019>
-- Description:	<>
-- Execute: EXEC stp_srv_StockAction '0010001010519001', 2
-- 
-- =============================================
CREATE PROCEDURE [dbo].[stp_srv_StockAction]
(
	@BarcodeNumber nvarchar(20),
	@StockAction int,
	@CreatedBy nvarchar(100)=null
)
AS
SET NOCOUNT ON;
BEGIN
	DECLARE @ActionCompletion BIT, @Message nvarchar(512)
	IF EXISTS(SELECT * FROM ProductLotMApping WHERE Barcode = @BarcodeNumber AND BarcodeStatus <> @StockAction)
	BEGIN
		UPDATE ProductLotMApping
		SET BarcodeStatus = @StockAction, ModifiedBy = @CreatedBy, DateModified = GETDATE()
		WHERE Barcode = @BarcodeNumber;
		
		SET @ActionCompletion = 1;
		SET @Message = 'Barcode status updated successfully.';
	END
	ELSE
	BEGIN
		SET @ActionCompletion = 0;
		SET @Message = 'Barcode status failed to update. Please contact with admin.';
	END
	
	SELECT @ActionCompletion ActionCompletion, @Message [Message];
END
GO
