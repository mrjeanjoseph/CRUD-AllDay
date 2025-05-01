CREATE PROCEDURE [dbo].[spProductGetAll] AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, ProductName, [Description], QuantityInStock, RetailPrice, IsTaxable
	FROM dbo.Product
	ORDER BY ProductName;
END
RETURN 0
