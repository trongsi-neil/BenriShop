USE [BenriShop]
GO
INSERT [dbo].[ACCOUNT] ([USERNAME], [PASSWORD], [ROLE], [PHONENUMBER], [FULLNAME], [ADDRESS]) VALUES (N'admin', N'admin', N'Admin', NULL, NULL, NULL)
INSERT [dbo].[ACCOUNT] ([USERNAME], [PASSWORD], [ROLE], [PHONENUMBER], [FULLNAME], [ADDRESS]) VALUES (N'customer', N'customer', N'Customer', NULL, NULL, NULL)
INSERT [dbo].[ACCOUNT] ([USERNAME], [PASSWORD], [ROLE], [PHONENUMBER], [FULLNAME], [ADDRESS]) VALUES (N'mod', N'mod', N'Mod', NULL, NULL, NULL)
INSERT [dbo].[ACCOUNT] ([USERNAME], [PASSWORD], [ROLE], [PHONENUMBER], [FULLNAME], [ADDRESS]) VALUES (N'testfortuan2', N'test', N'Customer', N'0949690002', N'Đỗ Trọng Sĩ', N'Mỹ Điền')
GO
INSERT [dbo].[COLOR] ([COLORID]) VALUES (N'BLUE')
INSERT [dbo].[COLOR] ([COLORID]) VALUES (N'GREEN')
INSERT [dbo].[COLOR] ([COLORID]) VALUES (N'RED')
GO
INSERT [dbo].[SIZE] ([SIZEID]) VALUES (N'L')
INSERT [dbo].[SIZE] ([SIZEID]) VALUES (N'M')
INSERT [dbo].[SIZE] ([SIZEID]) VALUES (N'S')
INSERT [dbo].[SIZE] ([SIZEID]) VALUES (N'XL')
GO
INSERT [dbo].[CATEGORY] ([CATEGORYID]) VALUES (N'KID')
INSERT [dbo].[CATEGORY] ([CATEGORYID]) VALUES (N'MEN')
INSERT [dbo].[CATEGORY] ([CATEGORYID]) VALUES (N'WOMEN')
GO
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([PRODUCT_ID], [CATEGORY_ID], [PRODUCT_NAME], [PRODUCT_DESCRIPTION], [PRICE], [STORAGE_QUANTITY]) VALUES (1, N'MEN', N'Doraemon 50 Years Hoodie - Black', N'Chiếc Hoodie màu Black căn bản sở hữu nhiều graphic truyện tranh Doraemon ẩn giấu. Điểm nhấn đặc biệt từ logo Doraemon phiên bản tiếng Nhật đặc trưng với hiệu ứng ép nổi 3D độc đáo và chiếc túi trước bụng, tuy không phải túi thần kì nhưng sẽ mang đến cho bạn những ứng dụng bất ngờ.', 590000, 130)
INSERT [dbo].[PRODUCT] ([PRODUCT_ID], [CATEGORY_ID], [PRODUCT_NAME], [PRODUCT_DESCRIPTION], [PRICE], [STORAGE_QUANTITY]) VALUES (2, N'MEN', N'Beanie-hat', N'Beanie - Go Skate đặc biệt ấn tượng với phối màu Olive cùng tem dệt artwork màu sắc, thể hiện năng lượng tích cực. Đây chắc chắn là chiếc mũ không thể thiếu mỗi lần gặp gỡ, đi trượt cùng anh em.', 190000, 140)
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
GO
INSERT [dbo].[SIZE_OF_PRODUCT_HAD_COLOR] ([SIZE_ID], [COLOR_ID], [PRODUCT_ID], [QUANTITY_IN_SIZE_OF_COLOR]) VALUES (N'M', N'BLUE', 2, 46)
INSERT [dbo].[SIZE_OF_PRODUCT_HAD_COLOR] ([SIZE_ID], [COLOR_ID], [PRODUCT_ID], [QUANTITY_IN_SIZE_OF_COLOR]) VALUES (N'M', N'GREEN', 1, 20)
INSERT [dbo].[SIZE_OF_PRODUCT_HAD_COLOR] ([SIZE_ID], [COLOR_ID], [PRODUCT_ID], [QUANTITY_IN_SIZE_OF_COLOR]) VALUES (N'M', N'GREEN', 2, 30)
INSERT [dbo].[SIZE_OF_PRODUCT_HAD_COLOR] ([SIZE_ID], [COLOR_ID], [PRODUCT_ID], [QUANTITY_IN_SIZE_OF_COLOR]) VALUES (N'S', N'GREEN', 1, 30)
GO
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'cotton')
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'hiphop')
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'luxury')
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'short')
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'street')
INSERT [dbo].[TAG] ([TAGID]) VALUES (N'wool')
GO
INSERT [dbo].[HAVETAG] ([PRODUCTID], [TAGID]) VALUES (1, N'hiphop')
INSERT [dbo].[HAVETAG] ([PRODUCTID], [TAGID]) VALUES (2, N'hiphop')
INSERT [dbo].[HAVETAG] ([PRODUCTID], [TAGID]) VALUES (2, N'street')
GO
INSERT [dbo].[IMAGE] ([PRODUCT_ID], [IMAGE_ID], [LINK]) VALUES (1, N'1_Doraemon 50 Years Hoodie - Black.jpg', N'\images\1_Doraemon 50 Years Hoodie - Black.jpg')
INSERT [dbo].[IMAGE] ([PRODUCT_ID], [IMAGE_ID], [LINK]) VALUES (2, N'2_beanie-hat.jpg', N'\images\2_beanie-hat.jpg')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210501014816_InitialCreate', N'3.1.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210501070737_InitialCreate', N'3.1.13')
GO
