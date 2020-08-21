CREATE TABLE [dbo].[Boards]
(
	Id Int Identity(1, 1) Not Null PRIMARY KEY,
	Name NVarChar(25) Not Null,
	Email NVarChar(100) Not Null,
	Title NVarChar(150) Not Null,
	PostDate DateTime Default GetDate() Not Null,
	PostIp NVarChar(15) Null,
	Content NText Not Null,
	Password NVarChar(20) Null,
	ReadCount Int Default 0,
	Encoding NVarChar(10) Null,
	Homepage NVarChar(100) Null,
	ModifyDate DateTime Not Null,
	ModifyIp NVarChar(15) Null
)
