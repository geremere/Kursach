SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [Name], [Login], [Password], [Position]) VALUES (1, N'Zen Snow', N'Zen', N'123', N'Tester')
INSERT INTO [dbo].[Users] ([Id], [Name], [Login], [Password], [Position]) VALUES (2, N'Alex Ferguson', N'Alex', N'1234', N'Manager')
SET IDENTITY_INSERT [dbo].[Users] OFF
