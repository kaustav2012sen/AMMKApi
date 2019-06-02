IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stv_srv_GetProductLotIdDetails')
	DROP PROCEDURE [dbo].[stv_srv_GetProductLotIdDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gautam Giri>
-- Create date: <27/05/2019>
-- Description:	<To fetch the Product LotDetails>
-- =============================================
CREATE PROCEDURE stv_srv_GetProductLotIdDetails 
(
@ProductId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  SELECT DISTINCT ProductLotMapping.LotNumber FROM ProductLotMapping WHERE ProductID = @ProductId
END
GO
