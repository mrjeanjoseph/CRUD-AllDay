CREATE PROCEDURE [dbo].[spProductGetById] @Id INT AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [ProductName], [RetailPrice], [QuantityInStock], [IsTaxable]
	FROM [dbo].[Product]
	WHERE [Id]= @Id;
END
