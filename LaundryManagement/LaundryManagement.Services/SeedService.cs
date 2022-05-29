using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (1, N'Locations Administration', N'ADM_LOC')
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (2, N'Administration', NULL)
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (3, N'Reports', NULL)
				
				INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (4, N'Movements Report', N'REP_MOV')
				
				SET IDENTITY_INSERT [dbo].[Permission] OFF
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (2, 1)
				
				INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (3, 4)
				
				SET IDENTITY_INSERT [dbo].[User] ON 
				
				INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (1, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo')


				INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [Name], [LastName]) VALUES (4, N'test', N'gdyb21LQTcIANtvYMT7QVQ==', N'cperez', N'Carlos', N'Perez')


				SET IDENTITY_INSERT [dbo].[User] OFF
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 2)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 4)
				
				INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 1)
				
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
