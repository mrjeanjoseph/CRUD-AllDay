USE [WideWorldImporters]

UPDATE sales.orderlines 
SET quantity=quantity+1000 
WHERE orderID IN (
	SELECT TOP 100 OL.orderID FROM sales.orderlines OL
	inner join sales.orders O on ol.orderID = O.orderID
	WHERE o.OrderDate > DATEADD(day,-10,GETDATE())
	and StockItemID=164)

