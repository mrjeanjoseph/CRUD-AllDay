

DROP TABLE IF EXISTS [SportsStore].[dbo].[Merchandise]

CREATE TABLE [dbo].[Merchandise](
	[Id] [int] IDENTITY(5,5) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Category] [nvarchar](64) NOT NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[IsValid] [bit] NOT NULL,
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