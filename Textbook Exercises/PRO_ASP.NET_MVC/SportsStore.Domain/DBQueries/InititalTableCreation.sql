

DROP TABLE IF EXISTS [SportsStore].[dbo].[Merchandise]

CREATE TABLE [dbo].[Merchandise](
	[Id] [INT] IDENTITY(5,5) NOT NULL,
	[Name] [NVARCHAR](128) NOT NULL,
	[Description] [NVARCHAR](512) NOT NULL,
	[Category] [NVARCHAR](64) NOT NULL,
	[Price] [DECIMAL](16, 2) NOT NULL,
	[IsValid] [BIT] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Merchandise] ADD  DEFAULT ((1)) FOR [IsValid]
GO


ALTER TABLE [SportsStore].[dbo].[Merchandise]
DROP COLUMN [IsValid]

ALTER TABLE [SportsStore].[dbo].[Merchandise]
ADD [IsValid] [bit] NOT NULL DEFAULT 1



--Readd the same data a few time
BEGIN

	INSERT INTO SportsStore.dbo.Merchandise ([Name], [Description], [Category], [Price])
	SELECT [Name], [Description], [Category], [Price]
	FROM SportsStore.dbo.Merchandise


END	
GO 
RETURN;




BEGIN
	--Mass creating Categories
	UPDATE merch
	SET merch.Category = 
	CASE WHEN RandomNumber = 5 THEN 'Structural'
		WHEN RandomNumber = 0 THEN 'Masonry'
		WHEN RandomNumber = 3 THEN 'HVAC'
		WHEN RandomNumber = 4 THEN 'Asphalt Paving'
		WHEN RandomNumber = 1 THEN 'Casework'
		WHEN RandomNumber = 2 OR RandomNumber = 6 THEN 'Maintenance'
	END
	FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY RAND()) % 6 [RandomNumber] 
	FROM SportsStore.dbo.Merchandises merch) AS merch

	SELECT * FROM SportsStore.dbo.Merchandises

END






SELECT * FROM SportsStore.dbo.[Merchandises]

ALTER TABLE SportsStore.dbo.[Merchandises]
	ADD [ImageData]		VARBINARY(MAX) NULL,
		[ImageMimeType] VARCHAR(50);
