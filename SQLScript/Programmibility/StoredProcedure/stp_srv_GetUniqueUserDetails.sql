IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'stp_srv_GetUniqueUserDetails')
	DROP PROCEDURE [dbo].[stp_srv_GetUniqueUserDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kaustav Sen>
-- Create date: <22/07/2018>
-- Description:	<To Get Details of All Distributer>
-- Execute: EXEC stp_srv_GetUniqueUserDetails '54402','@abcd1234'
-- 
-- =============================================
CREATE PROCEDURE [dbo].[stp_srv_GetUniqueUserDetails]
(
@UserID	NVARCHAR(100),
@Password NVARCHAR(100)
)
AS
SET NOCOUNT ON;
BEGIN

	SELECT UM.UserName,UM.FirstName,UM.MiddleName,UM.LastName,RM.RoleName FROM UserMaster UM 
	INNER JOIN 
	RoleMaster RM
	ON UM.RoleID=RM.RoleID
	WHERE UM.UserName=@UserID AND UM.[Password]=@Password;

	--SELECT DistributerID,DistributerCode,DistributerName FROM tbl_mst_distributer WHERE DistributerCode=@DistributerCode

END
GO