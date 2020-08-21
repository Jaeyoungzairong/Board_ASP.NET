CREATE TABLE [dbo].[Accounts]
(
	No Int Identity(1, 1) Not Null PRIMARY KEY,
	Id NVarChar(25) Not Null,
	Password NVarChar(25) Not Null,
	Name NVarChar(25) Not Null,
	Email NVarChar(100) Not Null
)
