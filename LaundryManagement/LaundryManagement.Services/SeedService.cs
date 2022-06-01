using System;
using System.Data.SqlClient;

namespace LaundryManagement.Services
{
    public class SeedService
    {
        private SqlConnection _connection;
        public SeedService(string connectionString)
        {
			_connection = new SqlConnection(connectionString);
        }

        public void SeedData()
        {
			if (this.CheckExistingData())
				return;

            _connection.Open();

            SqlCommand cmdCreateTables = new SqlCommand($@"
				BEGIN TRANSACTION
				USE [LaundryManagement]

				SET ANSI_NULLS ON
				SET QUOTED_IDENTIFIER ON

				CREATE TABLE [dbo].[Permission](
					[Id] [int] IDENTITY(1,1) NOT NULL,
					[Name] [varchar](50) NOT NULL,
					[Permission] [varchar](50) NULL,
					CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
				(
					[Id] ASC
				)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
				) ON [PRIMARY]

				SET ANSI_NULLS ON
				SET QUOTED_IDENTIFIER ON
				CREATE TABLE [dbo].[PermissionFamily](
					[IdPermissionParent] [int] NOT NULL,
					[IdPermission] [int] NOT NULL
				) ON [PRIMARY]

				SET ANSI_NULLS ON
				SET QUOTED_IDENTIFIER ON
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

				SET ANSI_NULLS ON
				SET QUOTED_IDENTIFIER ON
				CREATE TABLE [dbo].[UserPermission](
					[IdUser] [int] NOT NULL,
					[IdPermission] [int] NOT NULL
				) ON [PRIMARY]

				COMMIT TRANSACTION");
			cmdCreateTables.Connection = _connection;
			cmdCreateTables.ExecuteNonQuery();

			SqlCommand cmdInsert = new SqlCommand($@"
				BEGIN TRANSACTION
				USE [LaundryManagement]

				SET IDENTITY_INSERT [dbo].[Permission] ON 
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (6, N'Administration', NULL)
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (7, N'Reports', NULL)
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (8, N'Processes', NULL)
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (9, N'Locations Administration', N'ADM_LOC')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (10, N'Categories Administration', N'ADM_CAT')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (11, N'Item Types Administration', N'ADM_TYP')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (12, N'Articles Administration', N'ADM_ART')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (13, N'Sizes Administration', N'ADM_SIZ')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (14, N'Colors Administration', N'ADM_COL')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (15, N'Users Administration', N'ADM_USR')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (16, N'Roles Administration', N'ADM_SIZ')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (17, N'Laundry Shipping Process', N'PRO_SHP_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (18, N'Clinic Shipping Process', N'PRO_SHP_CLI')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (19, N'Internal Shipping Process', N'PRO_SHP_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (20, N'Laundry Reception Process', N'PRO_REC_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (21, N'Clinic Reception Process', N'PRO_REC_CLI')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (22, N'Item Creation Process', N'PRO_ITM_NEW')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (23, N'Item Delete Process', N'PRO_ITM_DEL')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (24, N'Road Map Process', N'PRO_ROA')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (25, N'Laundry Shipping Report', N'REP_SHP_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (26, N'Clinic Shipping Report', N'REP_SHP_CLI')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (27, N'Internal Shipping Report', N'REP_SHP_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (28, N'Laundry Reception Report', N'REP_REC_LDY')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (29, N'Clinic Reception Report', N'REP_REC_CLI')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (30, N'Roadmap Report', N'REP_ROA')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (31, N'Movements Report', N'REP_MOV')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (32, N'Create Locations', N'ADM_LOC_CRE')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (33, N'Edit Locations', N'ADM_LOC_EDI')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (34, N'Delete Locations', N'ADM_LOC_DEL')
				
				SET IDENTITY_INSERT [dbo].[Permission] OFF
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 9)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 10)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 11)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 12)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 13)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 14)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 15)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 16)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 17)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 18)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 19)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 20)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 21)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 22)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 23)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 24)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 25)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 26)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 27)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 28)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 29)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 30)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 31)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 32)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 33)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 34)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 32)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 33)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (9, 34)
				
				SET IDENTITY_INSERT [dbo].[User] ON 
				
				INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (1, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo')
				
				INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (4, N'test', N'gdyb21LQTcIANtvYMT7QVQ==', N'cperez', N'Carlos', N'Perez')
				
				SET IDENTITY_INSERT [dbo].[User] OFF
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 34)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 9)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 32)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 33)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 6)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 10)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 11)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 12)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 13)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 14)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 15)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 16)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 8)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 17)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 19)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 21)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 24)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 25)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 27)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 30)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 31)
								
				ALTER TABLE [dbo].[PermissionFamily]  WITH CHECK ADD  CONSTRAINT [FK_PermissionFamily_Permission] FOREIGN KEY([IdPermission])
				REFERENCES [dbo].[Permission] ([Id])
				
				ALTER TABLE [dbo].[PermissionFamily] CHECK CONSTRAINT [FK_PermissionFamily_Permission]
				
				ALTER TABLE [dbo].[PermissionFamily]  WITH CHECK ADD  CONSTRAINT [FK_PermissionFamily_Permission1] FOREIGN KEY([IdPermissionParent])
				REFERENCES [dbo].[Permission] ([Id])
				
				ALTER TABLE [dbo].[PermissionFamily] CHECK CONSTRAINT [FK_PermissionFamily_Permission1]
				
				ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([IdPermission])
				REFERENCES [dbo].[Permission] ([Id])
				
				ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission]
				
				ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([IdUser])
				REFERENCES [dbo].[User] ([Id])
				
				ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
				COMMIT TRANSACTION");
			cmdInsert.Connection = _connection;
			cmdInsert.ExecuteNonQuery();

			_connection.Close();
        }

		private bool CheckExistingData()
        {
			_connection.Open();
			
			SqlCommand cmdCheck = new SqlCommand($@"
				IF OBJECT_ID (N'[User]', N'U') IS NOT NULL 
				   SELECT 1 AS res ELSE SELECT 0 AS res;", _connection);

			bool result = (Int32)cmdCheck.ExecuteScalar() == 1;

			_connection.Close();

			return result;
        }
    }
}
