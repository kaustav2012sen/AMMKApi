IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_GetUniqueVendorProductDetails')
	DROP PROCEDURE [dbo].[stp_srv_GetUniqueVendorProductDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kunal Mukherjee>
-- Create date: <09/05/2019>
-- Description:	<To Get Details of All Distributer>
-- Execute: EXEC stp_srv_GetUniqueVendorProductDetails 
-- 
-- =============================================
CREATE PROCEDURE [dbo].[stp_srv_GetUniqueVendorProductDetails]

AS
SET NOCOUNT ON;
BEGIN

	SELECT PM.ProductID,PM.ProductName,PM.BillingName,PM.[Type],PM.GST,PM.HSN 
	FROM ProductMaster PM 
	WHERE PM.Deleted = 0
	ORDER BY PM.ProductName
	
	SELECT VM.VendorID, VLM.VendorLocationName LocationName, VM.VendorName as VendorName
	FROM VendorMaster VM INNER JOIN VendorLocationMaster VLM ON VM.VendorLocationID = VLM.VendorLocationID
	WHERE VM.Deleted = 0 AND VLM.Deleted = 0
	ORDER BY VM.VendorLocationID, VM.VendorName
END
GO