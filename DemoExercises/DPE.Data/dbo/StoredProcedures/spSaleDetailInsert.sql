﻿CREATE PROCEDURE [dbo].[spSaleDetailInsert]
	@SaleId INT,
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice MONEY,
	@Tax MONEY
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[SaleDetail](SaleId, ProductId, Quanity, PuchasePrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax)

END
GO