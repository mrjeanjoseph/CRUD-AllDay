CREATE PROCEDURE [dbo].[spSaleSalesReport]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [u].[FirstName], [u].[LastName], [u].[EmailAddress],
			[s].[CashierId], [s].[SaleDate], [s].[SubTotal], [s].[Tax], [s].[Total]
	FROM [dbo].[Sale] s
	INNER JOIN [dbo].[User] u ON s.[CashierId] = u.[Id]
END