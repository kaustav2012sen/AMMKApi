IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_GetLotNumber')
	DROP PROCEDURE [dbo].[stp_srv_GetLotNumber]
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
	EXEC stp_srv_GetLotNumber 'PAS-001',@Number OUTPUT
	PRINT @Number*/
-- 
-- =============================================
CREATE PROCEDURE [dbo].[stp_srv_GetLotNumber]
(
@BillingName NVARCHAR(100),
@LotNumber INT OUTPUT
)
AS
SET NOCOUNT ON;
BEGIN

	SET @LotNumber=(SELECT LM.LotNumber FROM LottingMaster LM WHERE BillingName=@BillingName);
	SET @LotNumber=@LotNumber+1;

	UPDATE LottingMaster SET LotNumber=@LotNumber WHERE BillingName=@BillingName;
	
END
GO

--SELECT * FROM LottingMAster