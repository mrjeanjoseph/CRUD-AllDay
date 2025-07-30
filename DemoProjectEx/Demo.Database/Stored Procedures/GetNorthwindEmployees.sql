CREATE PROCEDURE DBO.GetNorthwindEmployees AS
BEGIN
	SELECT 
		EmployeeID,
		Title,
		CONCAT(FirstName, ' ', LastName) AS [FullName],		
		CONVERT(VARCHAR, HireDate, 107) AS [HireDate],
		CONCAT(City, ', ', Region, ' ', PostalCode) AS [Location],
		HomePhone AS [PhoneNumber]
	FROM DBO.Employees
END

