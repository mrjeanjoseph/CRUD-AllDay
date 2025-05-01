﻿CREATE PROCEDURE [dbo].[spSaleInsert] 
	@Id INT = OUTPUT,
	@CashierId NVARCHAR(128),
	@SaleDate DATETIME2,
	@SubTotal MONEY,
	@Tax MONEY,
	@Total MONEY
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Sale](CashierId, SaleDate, SubTotal, Tax, Total)
	VALUES (@CashierId, @SaleDate, @SubTotal, @Tax, @Total)

	SELECT @Id = SCOPE_IDENTITY();
	--SELECT @Id = @@IDENTITY;
END