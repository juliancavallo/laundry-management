USE [LaundryManagement]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 28/5/2022 19:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Permission] [varchar](50) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionFamily]    Script Date: 28/5/2022 19:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionFamily](
	[IdPermissionParent] [int] NOT NULL,
	[IdPermission] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 28/5/2022 19:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 28/5/2022 19:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[IdUser] [int] NOT NULL,
	[IdPermission] [int] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 
GO
INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (1, N'Locations Administration', N'ADM_LOC')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (2, N'Administration', NULL)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (3, N'Reports', NULL)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (4, N'Movements Report', N'REP_MOV')
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (2, 1)
GO
INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (3, 4)
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (1, N'jcavallo11@gmail.com', N'sSbgW6ZJYgf7ixqblREVWw==', N'jcavallo', N'Julian', N'Cavallo')
GO
INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (4, N'test', N'4QrcOUm6Wau+VuBX8g+IPg==', N'cperez', N'Carlos', N'Perez')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 2)
GO
INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 4)
GO
INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 1)
GO
ALTER TABLE [dbo].[PermissionFamily]  WITH CHECK ADD  CONSTRAINT [FK_PermissionFamily_Permission] FOREIGN KEY([IdPermission])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[PermissionFamily] CHECK CONSTRAINT [FK_PermissionFamily_Permission]
GO
ALTER TABLE [dbo].[PermissionFamily]  WITH CHECK ADD  CONSTRAINT [FK_PermissionFamily_Permission1] FOREIGN KEY([IdPermissionParent])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[PermissionFamily] CHECK CONSTRAINT [FK_PermissionFamily_Permission1]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([IdPermission])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
GO