SELECT getutcDate()


-----------------------------------User Master Insertion--------------------------------------------
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Rahul','Das','Naskar','54403',1,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kunal',NULL,'Mukherjee','54402',1,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kaustav',NULL,'Sen','54401',1,0,NULL,GETUTCDATE())

----------------Role Master Insertion-------------------------
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('ADMIN',0,NULL,GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('MASTER',0,'54401',GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('USER',0,'54401',GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('CASH AND RETURN',0,'54403',GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('BARCODE DEPARTMENT',0,'54402',GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('BARCODE SUPERVISOR',0,'54403',GETUTCDATE())


SELECT * FROM UserMaster
SELECT * FROM RoleMaster