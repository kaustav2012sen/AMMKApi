IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_GetProductLotIDBarcodeDetails')
	DROP PROCEDURE [dbo].[stp_srv_GetProductLotIDBarcodeDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE stp_srv_GetProductLotIDBarcodeDetails 
(
@ProductId int,
@Lotnumber int
)
AS
BEGIN
	
	SET NOCOUNT ON;

   select distinct ProductLotMapping.Barcode from ProductLotMapping where ProductID = @ProductId And LotNumber=@Lotnumber
END
GO
