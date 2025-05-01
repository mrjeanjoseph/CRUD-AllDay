CREATE PROCEDURE [dbo].[spUserLookup] @id NVARCHAR(128)	AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, FirstName, LastName, EmailAddress, CreatedDate
	FROM [dbo].[User]
	WHERE Id = @id;
END