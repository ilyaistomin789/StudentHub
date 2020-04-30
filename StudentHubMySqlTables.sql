CREATE DATABASE StudentHub
GO
USE StudentHub
-- ---
-- Table 'User'
-- 
-- ---

DROP TABLE IF EXISTS Users;
		
CREATE TABLE Users (
  UserId INTEGER PRIMARY KEY NOT NULL IDENTITY(1,1),
  UserName NVARCHAR(30) NOT NULL ,
  UserPassword NVARCHAR(30) NOT NULL,
);

-- ---
-- Table 'Student'
-- 
-- ---

DROP TABLE IF EXISTS Student;
		
CREATE TABLE Student (
  StudentId INTEGER NOT NULL IDENTITY(1000,1),
  UserId INTEGER NOT NULL constraint STUDENT_USER_FK foreign key references Users(UserId),
  StudentStatus NVARCHAR(20) NULL DEFAULT 'Student',
  Course INTEGER NULL DEFAULT NULL,
  GroupId INTEGER NULL DEFAULT NULL,
  Specialization NVARCHAR(20) NULL DEFAULT NULL,
  Faculty NVARCHAR(30) NULL DEFAULT NULL,
  Birthday DATE NULL DEFAULT NULL,
  PRIMARY KEY(StudentId)
);

-- ---
-- Table 'subject'
-- 
-- ---

DROP TABLE IF EXISTS Subject;
		
CREATE TABLE Subject (
  Subject NVARCHAR(20) NULL DEFAULT NULL,
  SubjectName NVARCHAR(50) NOT NULL,
  CONSTRAINT PK_Subject PRIMARY KEY (SubjectName)
);


-- ---
-- Table 'progress'
-- 
-- ---

DROP TABLE IF EXISTS Progress;
		
CREATE TABLE Progress (
  ProgressId INTEGER NOT NULL IDENTITY(1,1),
  StudentId INTEGER NOT NULL constraint PROGRESS_STUDENT_FK foreign key references Student(StudentId),
  SubjectName NVARCHAR(50) NOT NULL constraint PROGRESS_SUBJECT_FK foreign key references Subject(SubjectName),
  Note INTEGER NULL DEFAULT NULL,
  PDate DATE NULL DEFAULT NULL,
  PRIMARY KEY (ProgressId, StudentId, SubjectName)
);
-- ---
-- Table 'admin'
-- 
-- ---

DROP TABLE IF EXISTS Admin;
		
CREATE TABLE Admin (
  AdminId INTEGER NOT NULL IDENTITY(1,1),
  AdminName NVARCHAR(80) NULL DEFAULT NULL,
  UserId INTEGER constraint ADMIN_USER_FK foreign key references Users(UserId)
);

-- ---
-- Table 'adjustment'
-- 
-- ---

DROP TABLE IF EXISTS Adjustment;
		
CREATE TABLE Adjustment (
  AdjustmentId INTEGER NOT NULL IDENTITY(1,1),
  SubjectName NVARCHAR(50) NULL DEFAULT NULL,
  ADate DATE NULL DEFAULT NULL,
  PRIMARY KEY (AdjustmentId)
);

-- ---
-- Table 'Retake'
-- 
-- ---

DROP TABLE IF EXISTS Retake;
		
CREATE TABLE Retake (
  RetakeId INTEGER NOT NULL IDENTITY(1,1),
  SubjectName NVARCHAR(50) NULL DEFAULT NULL,
  RDate DATE NULL DEFAULT NULL,
  PRIMARY KEY (RetakeId)
);

CREATE PROCEDURE ADD_USER
@UserName NVARCHAR(30),
@UserPassword NVARCHAR(30)
AS
BEGIN
	INSERT INTO Users(UserName,UserPassword) VALUES (@UserName,@UserPassword);
	DECLARE @userId INTEGER;
	SET @userId = (SELECT MAX(UserId) FROM Users);
	INSERT INTO Student(UserId) VALUES (@userId);
END

-- ---
-- Foreign Keys 
-- ---

--ALTER TABLE Users ADD FOREIGN KEY (UserId) REFERENCES admin (UserId);
--ALTER TABLE Student ADD FOREIGN KEY (StudentId) REFERENCES Progress (StudentId);
--ALTER TABLE Student ADD FOREIGN KEY (userId) REFERENCES Users (UserId);
--ALTER TABLE Subject ADD FOREIGN KEY (subjectName) REFERENCES progress (subjectName);
--ALTER TABLE Admin ADD FOREIGN KEY (UserId) REFERENCES Users (UserId);
--ALTER TABLE Progress ADD FOREIGN KEY (StudentId) REFERENCES Student (StudentId);
--ALTER TABLE Student ADD FOREIGN KEY (userId) REFERENCES Users (UserId);
--ALTER TABLE Progress ADD FOREIGN KEY (subjectName) REFERENCES Subject (subjectName);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `User` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `Student` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `progress` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `subject` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `admin` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `adjustment` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `Retake` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `User` (`id`,`UserName`,`userPassword`) VALUES
-- ('','','');
-- INSERT INTO `Student` (`id`,`userId`,`studentStatus`,`course`,`group`,`specialization`,`faculty`,`birthday`) VALUES
-- ('','','','','','','','');
-- INSERT INTO `progress` (`id`,`studentId`,`subjectName`,`note`,`PDate`) VALUES
-- ('','','','','');
-- INSERT INTO `subject` (`id`,`subject`,`subjectname`) VALUES
-- ('','','');
-- INSERT INTO `admin` (`id`,`adminName`,`userId`) VALUES
-- ('','','');
-- INSERT INTO `adjustment` (`id`,`subjectName`,`Date`) VALUES
-- ('','','');
-- INSERT INTO `Retake` (`id`,`SubjectName`,`Date`) VALUES
-- ('','','');