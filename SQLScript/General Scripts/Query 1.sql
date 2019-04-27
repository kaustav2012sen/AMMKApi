SELECT getutcDate()


-----------------------------------User Master Insertion--------------------------------------------
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Rahul','Das','Naskar','54403','@abcd1234',1,1,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kunal',NULL,'Mukherjee','54402','@abcd1234',1,1,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kaustav',NULL,'Sen','54401','@abcd1234',1,1,0,NULL,GETUTCDATE())

----------------------------Dummy User List to check test pages------------------------------------
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kaustav',NULL,'Sen','4401','@abcd1234',1,4,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kaustav',NULL,'Sen','4501','@abcd1234',1,7,0,NULL,GETUTCDATE())
INSERT INTO UserMaster (FirstName,MiddleName,LastName,UserName,[Password],IsDefaultPassword,RoleID,Deleted,CreatedBy,DateEntered)
VALUES ('Kaustav',NULL,'Sen','4502','@abcd1234',1,8,0,NULL,GETUTCDATE())

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
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('STOCK OUT DEPARTMENT',0,'54402',GETUTCDATE())
INSERT INTO RoleMaster (RoleName,Deleted,CreatedBy,DateEntered)
VALUES ('STOCK IN DEPARTMENT',0,'54403',GETUTCDATE())

SELECT * FROM UserMaster
SELECT * FROM RoleMaster