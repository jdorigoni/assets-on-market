SET IDENTITY_INSERT [dbo].[Asset] ON
INSERT INTO [dbo].[Asset]([AssetId], [AssetName]) VALUES 
			(1, 'Asset 1'),
			(2, 'Asset 2'),
			(3, 'Asset 3'),
			(4, 'Asset 4'),
			(5, 'Asset 5'),
			(6, 'Asset 6'),
			(7, 'Asset 7'),
			(8, 'Asset 8'),
			(9, 'Asset 9'),
			(10, 'Asset 10'),
			(11, 'Asset 11'),
			(12, 'Asset 12'),
			(13, 'Asset 13'),
			(14, 'Asset 14'),
			(15, 'Asset 15'),
			(16, 'Asset 16'),
			(17, 'Asset 17'),
			(18, 'Asset 18'),
			(19, 'Asset 19'),
			(20, 'Asset 20')

SET IDENTITY_INSERT [dbo].[Asset] OFF
GO

SET IDENTITY_INSERT [dbo].[AssetProperty] ON
INSERT INTO [dbo].[AssetProperty] ([AssetPropertyId], [AssetId], [Property],[Value],[Timestamp]) VALUES
           (1, 1, 'is fix income', 1, '2020-07-01 16:32:32'),
		   (2, 1, 'is convertible', 1, '2020-07-01 16:32:32'),
		   (3, 1, 'is swap', 1, '2020-07-01 16:32:32'),
		   (4, 1, 'is cash', 0, '2020-07-01 16:32:32'),
		   (5, 1, 'is future', 1, '2020-07-01 16:32:32'),
		   (6, 2, 'is fix income', 1, '2020-07-01 16:32:32'),
		   (7, 2, 'is convertible', 1, '2020-07-01 16:32:32'),
		   (8, 3, 'is swap', 1, '2020-07-01 16:32:32'),
		   (9, 3, 'is cash', 1, '2020-07-01 16:32:32'),
		   (10, 3, 'is future', 1, '2020-07-01 16:32:32')
SET IDENTITY_INSERT [dbo].[AssetProperty] OFF
GO

