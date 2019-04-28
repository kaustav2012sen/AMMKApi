IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_GetUniqueProductDetails')
	DROP PROCEDURE [dbo].[stp_srv_GetUniqueProductDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kaustav Sen>
-- Create date: <22/07/2018>
-- Description:	<To Get Details of All Distributer>
-- Execute: EXEC stp_srv_GetUniqueProductDetails 
-- 
-- =============================================
CREATE PROCEDURE [dbo].[stp_srv_GetUniqueProductDetails]

AS
SET NOCOUNT ON;
BEGIN

	SELECT PM.ProductID,PM.ProductName,PM.BillingName,PM.[Type],PM.GST,PM.HSN FROM 	 
	ProductMaster PM ORDER BY PM.ProductName
	
END
GO