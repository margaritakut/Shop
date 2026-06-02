USE [Databases]
GO

/****** Создание таблиц ******/

CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[PickUpPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostCode] [nvarchar](6) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Building] [nvarchar](6) NULL,
 CONSTRAINT [PK_PickUpPoint] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Producer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Producer] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Provider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Suname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronmic] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Article] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Unitid] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[ProviderId] [int] NOT NULL,
	[ProducerId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Discount] [decimal](4, 2) NOT NULL,
	[AmountinStock] [decimal](6, 2) NOT NULL,
	[Discription] [nvarchar](max) NOT NULL,
	[Photo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[DeliveriDate] [date] NOT NULL,
	[PickUpPointId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ReceptCode] [nvarchar](4) NOT NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[ProductinOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_ProductinOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
)

GO

/****** Заполнение таблиц данными ******/

-- Category
SET IDENTITY_INSERT [dbo].[Category] ON 
INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Прихожая')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Диван')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Обувница')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Пуф')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'Полка')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (6, N'Стул')
SET IDENTITY_INSERT [dbo].[Category] OFF

-- OrderStatus
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 
INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (1, N'Новый ')
INSERT [dbo].[OrderStatus] ([Id], [Name]) VALUES (2, N'Завершен')
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF

-- PickUpPoint
SET IDENTITY_INSERT [dbo].[PickUpPoint] ON 
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (1, N'420151', N'Лесной', N'Вишневая', N'32')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (2, N'125061', N'Лесной', N'Подгорная', N'8')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (3, N'630370', N'Лесной', N'Шоссейная', N'24')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (4, N'400562', N'Лесной', N'Зеленая', N'32')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (5, N'614510', N'Лесной', N'Маяковского', N'47')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (6, N'410542', N'Лесной', N'Светлая', N'46')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (7, N'620839', N'Лесной', N'Цветочная', N'8')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (8, N'443890', N'Лесной', N'Коммунистическая', N'1')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (9, N'603379', N'Лесной', N'Спортивная', N'46')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (10, N'603721', N'Лесной', N'Гоголя', N'41')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (11, N'410172', N'Лесной', N'Северная', N'13')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (12, N'614611', N'Лесной', N'Молодежная', N'50')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (13, N'454311', N'Лесной', N'Новая', N'19')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (14, N'660007', N'Лесной', N'Октябрьская', N'19')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (15, N'603036', N'Лесной', N'Садовая', N'4')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (16, N'394060', N'Лесной', N'Фрунзе', N'43')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (17, N'410661', N'Лесной', N'Школьная', N'50')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (18, N'625590', N'Лесной', N'Коммунистическая', N'20')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (19, N'625683', N'Лесной', N'8Марта', N'')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (20, N'450983', N'Лесной', N'Комсомольская', N'26')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (21, N'394782', N'Лесной', N'Чехова', N'3')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (22, N'603002', N'Лесной', N'Дзержинского', N'28')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (23, N'450558', N'Лесной', N'Набережная', N'30')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (24, N'344288', N'Лесной', N'Чехова', N'1')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (25, N'614164', N'Лесной', N'Степная', N'30')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (26, N'394242', N'Лесной', N'Коммунистическая', N'43')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (27, N'660540', N'Лесной', N'Солнечная', N'25')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (28, N'125837', N'Лесной', N'Шоссейная', N'40')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (29, N'125703', N'Лесной', N'Партизанская', N'49')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (30, N'625283', N'Лесной', N'Победы', N'46')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (31, N'614753', N'Лесной', N'Полевая', N'35')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (32, N'426030', N'Лесной', N'Маяковского', N'44')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (33, N'450375', N'Лесной ', N'Клубная', N'44')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (34, N'625560', N'Лесной  ', N'Некрасова', N'12')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (35, N'630201', N'Лесной  ', N'Комсомольская', N'17')
INSERT [dbo].[PickUpPoint] ([Id], [PostCode], [City], [Street], [Building]) VALUES (36, N'190949', N'Лесной ', N'Мичурина', N'26')
SET IDENTITY_INSERT [dbo].[PickUpPoint] OFF

-- Producer
SET IDENTITY_INSERT [dbo].[Producer] ON 
INSERT [dbo].[Producer] ([Id], [Name]) VALUES (1, N'SVМЕБЕЛЬ')
INSERT [dbo].[Producer] ([Id], [Name]) VALUES (2, N'Мебелони')
INSERT [dbo].[Producer] ([Id], [Name]) VALUES (3, N'Инвуд')
INSERT [dbo].[Producer] ([Id], [Name]) VALUES (4, N'RIDBERG')
SET IDENTITY_INSERT [dbo].[Producer] OFF

-- Provider
SET IDENTITY_INSERT [dbo].[Provider] ON 
INSERT [dbo].[Provider] ([Id], [Name]) VALUES (1, N'Стройландия')
INSERT [dbo].[Provider] ([Id], [Name]) VALUES (2, N'Кромма')
INSERT [dbo].[Provider] ([Id], [Name]) VALUES (3, N'ЗолотоеРуно')
INSERT [dbo].[Provider] ([Id], [Name]) VALUES (4, N'KRYLOVMANUFACTURA')
SET IDENTITY_INSERT [dbo].[Provider] OFF

-- Role
SET IDENTITY_INSERT [dbo].[Role] ON 
INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Менеджер')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'Авторизированный клиент')
SET IDENTITY_INSERT [dbo].[Role] OFF

-- Unit
SET IDENTITY_INSERT [dbo].[Unit] ON 
INSERT [dbo].[Unit] ([Id], [Name]) VALUES (1, N'шт.')
SET IDENTITY_INSERT [dbo].[Unit] OFF

-- User
SET IDENTITY_INSERT [dbo].[User] ON 
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (1, N'Никифорова', N'Анна', N'Семеновна', N'94d5ous@gmail.com', N'uzWC67', 1)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (2, N'Стелина', N'Евгения', N'Петровна', N'uth4iz@mail.com', N'2L6KZG', 1)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (3, N'Никифорова', N'Весения', N'Николаевна', N'5d4zbu@tutanota.com', N'rwVDh9', 1)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (4, N'Сазонов', N'Руслан', N'Германович', N'ptec8ym@yahoo.com', N'LdNyos', 2)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (5, N'Одинцов', N'Серафим', N'Артёмович', N'1qz4kw@mail.com', N'gynQMT', 2)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (6, N'Старикова', N'Елена', N'Павловна', N'4np6se@mail.com', N'AtnDjr', 2)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (7, N'Степанов', N'Михаил', N'Артёмович', N'yzls62@outlook.com', N'JlFRCZ', 3)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (8, N'Михайлюк', N'Анна', N'Вячеславовна', N'1diph5e@tutanota.com', N'8ntwUp', 3)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (9, N'Ситдикова', N'Елена', N'Анатольевна', N'tjde7c@yahoo.com', N'YOyhfR', 3)
INSERT [dbo].[User] ([Id], [Suname], [Name], [Patronmic], [Login], [Password], [RoleId]) VALUES (10, N'Ворсин', N'Петр', N'Евгеньевич', N'wpmrc3do@tutanota.com', N'RSbvHv', 3)
SET IDENTITY_INSERT [dbo].[User] OFF

-- Product
SET IDENTITY_INSERT [dbo].[Product] ON 
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (1, N'А112Т4', N'ПрихожаяФаворит11420х2056х352мммДубДелано/ЦементСветлыйSV-М1шт', 1, 9577.0000, 1, 1, 1, CAST(10.00 AS Decimal(4, 2)), CAST(0.00 AS Decimal(6, 2)), N'Удивительно функциональная и практичная прихожая Фаворит 1, обладая характерными чертами Скандинавского стиля, выглядит эффектно и способна задать тон интерьеру дома, встречая вас и ваших гостей.', N'Res/1.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (2, N'G843H5', N'ПрихожаявкоридорТвистсзеркаломмебельсошкафами,120х37х202см', 1, 8803.0000, 1, 2, 1, CAST(25.00 AS Decimal(4, 2)), CAST(9.00 AS Decimal(6, 2)), N'Этот стеллаж со шкафом в прихожую комнату станет отличным элементом для вашего интерьера. Мебель для дома обеспечивает удобное хранение перчаток, шапок, зонтов, сумок и других аксессуаров.', N'Res/2.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (3, N'D325D4', N'УгловойдиванКроммаИнвудЛайт,серыйдвухместныйдиван,Velutto32', 1, 29125.0000, 2, 3, 2, CAST(5.00 AS Decimal(4, 2)), CAST(12.00 AS Decimal(6, 2)), N'Угловой диван Инвуд Лайт 2 - стильный и комфортный диван подойдет для комнаты любого размера.', N'Res/3.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (4, N'S432T5', N'ОбувницаRIDBERG,свешалкой,стальная,170x60x26см,5полок,вместимостьдо15пар', 1, 885.0000, 2, 4, 3, CAST(15.00 AS Decimal(4, 2)), CAST(15.00 AS Decimal(6, 2)), N'Обувница Ridberg с 5 полками и вешалкой - идеальное решение для организации хранения обуви в прихожей или гардеробной.', N'Res/4.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (5, N'F325D4', N'Диван,Прямойдиван,Диван-кроватьСититемно-коричневый.Квест-33', 1, 14322.0000, 3, 3, 2, CAST(18.00 AS Decimal(4, 2)), CAST(3.00 AS Decimal(6, 2)), N'Прямой диван-кровать Сити - это современное и функциональное решение для вашего дома.', N'Res/5.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (7, N'G432G6', N'Пуфтрансформеркроватьраскладушкасветло-коричневыйвелюр', 1, 6149.0000, 4, 3, 4, CAST(22.00 AS Decimal(4, 2)), CAST(3.00 AS Decimal(6, 2)), N'Пуф трансформер 5в1 представляет собой уникальное сочетание функций, выступая в качестве пуфика, столика, кресла, шезлонга и дополнительного спального места.', N'Res/6.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (8, N'H542F5', N'Диван,Прямойдиван,диванкровать,РиосимплмеханизмПантограф.Симпл-16', 1, 20708.0000, 3, 3, 2, CAST(4.00 AS Decimal(4, 2)), CAST(5.00 AS Decimal(6, 2)), N'Диван Рио симпл от "Золотое Руно" - это сочетание комфорта, функциональности и стильного дизайна.', N'Res/7.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (9, N'C346F5', N'ПолканастеннаяромбЛофт,черная,40см', 1, 2843.0000, 4, 4, 5, CAST(5.00 AS Decimal(4, 2)), CAST(4.00 AS Decimal(6, 2)), N'Полочки для цветов в стиле лофт. Подойдут как для цветов, так и в качестве декоративного элемента. Полки подойдут для дома, офиса, кафе, ресторана.', N'Res/8.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (10, N'F256G6', N'Стульядлякухни', 1, 4760.0000, 4, 4, 6, CAST(6.00 AS Decimal(4, 2)), CAST(2.00 AS Decimal(6, 2)), N'Набор из четырех стульев в лофт-дизайне станет любимой мебелью для отдыха и подойдет для взрослых и детей.', N'Res/9.jpg')
INSERT [dbo].[Product] ([Id], [Article], [Name], [Unitid], [Price], [ProviderId], [ProducerId], [CategoryId], [Discount], [AmountinStock], [Discription], [Photo]) VALUES (11, N'J532V5', N'Магнитнаяполка,дляхолодильника,металл,3шт,универсальная,чёрная', 1, 1387.0000, 4, 4, 5, CAST(8.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(6, 2)), N'Магнитная полка для холодильника - это удобный и практичный аксессуар, который поможет организовать пространство в вашем доме.', N'Res/10.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF

-- Order
SET IDENTITY_INSERT [dbo].[Order] ON 
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (1, CAST(N'2024-02-27' AS Date), CAST(N'2024-04-20' AS Date), 1, 7, N'901', 1)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (2, CAST(N'2024-09-28' AS Date), CAST(N'2024-04-21' AS Date), 11, 8, N'902', 1)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (3, CAST(N'2024-03-21' AS Date), CAST(N'2024-04-22' AS Date), 2, 9, N'903', 1)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (4, CAST(N'2024-02-20' AS Date), CAST(N'2024-04-23' AS Date), 11, 10, N'904', 2)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (5, CAST(N'2024-03-17' AS Date), CAST(N'2024-04-24' AS Date), 2, 7, N'905', 2)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (6, CAST(N'2024-03-01' AS Date), CAST(N'2024-04-25' AS Date), 15, 8, N'906', 2)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (7, CAST(N'2024-02-28' AS Date), CAST(N'2024-04-26' AS Date), 3, 9, N'907', 2)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (8, CAST(N'2024-03-31' AS Date), CAST(N'2024-04-27' AS Date), 19, 10, N'908', 1)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (9, CAST(N'2024-04-02' AS Date), CAST(N'2024-04-28' AS Date), 5, 9, N'909', 1)
INSERT [dbo].[Order] ([Id], [CreationDate], [DeliveriDate], [PickUpPointId], [UserId], [ReceptCode], [StatusId]) VALUES (10, CAST(N'2024-04-03' AS Date), CAST(N'2024-04-29' AS Date), 19, 10, N'910', 1)
SET IDENTITY_INSERT [dbo].[Order] OFF

-- ProductinOrder
SET IDENTITY_INSERT [dbo].[ProductinOrder] ON 
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (1, 1, 1, 2)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (2, 2, 2, 1)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (3, 3, 3, 10)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (4, 4, 5, 4)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (6, 6, 1, 2)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (7, 7, 2, 1)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (8, 8, 3, 10)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (9, 9, 5, 4)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (11, 1, 2, 2)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (12, 2, 1, 1)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (13, 3, 4, 10)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (14, 4, 3, 4)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (15, 5, 7, 20)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (16, 6, 2, 2)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (17, 7, 1, 1)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (18, 8, 4, 10)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (19, 9, 3, 4)
INSERT [dbo].[ProductinOrder] ([Id], [OrderId], [ProductId], [Amount]) VALUES (20, 10, 7, 20)
SET IDENTITY_INSERT [dbo].[ProductinOrder] OFF

GO

/****** Создание внешних ключей ******/

ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([StatusId]) REFERENCES [dbo].[OrderStatus] ([Id])
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_PickUpPoint] FOREIGN KEY([PickUpPointId]) REFERENCES [dbo].[PickUpPoint] ([Id])
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])

ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Category] ([Id])
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_Producer] FOREIGN KEY([ProducerId]) REFERENCES [dbo].[Producer] ([Id])
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_Provider] FOREIGN KEY([ProviderId]) REFERENCES [dbo].[Provider] ([Id])
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_Unit] FOREIGN KEY([Unitid]) REFERENCES [dbo].[Unit] ([Id])

ALTER TABLE [dbo].[ProductinOrder] ADD CONSTRAINT [FK_ProductinOrder_Order] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Order] ([Id])
ALTER TABLE [dbo].[ProductinOrder] ADD CONSTRAINT [FK_ProductinOrder_Product] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product] ([Id])

ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Role] ([Id])

GO