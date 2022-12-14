namespace LaundryManagement.DAL.Scripts
{
    public static class CreateDatabaseScript
    {
		public const string Content = @"
			USE [master]
			GO
			CREATE DATABASE [LaundryManagement]
			GO
			ALTER DATABASE [LaundryManagement] SET ANSI_NULL_DEFAULT OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET ANSI_NULLS OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET ANSI_PADDING OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET ANSI_WARNINGS OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET ARITHABORT OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET AUTO_CLOSE OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET AUTO_SHRINK OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET AUTO_UPDATE_STATISTICS ON 
			GO
			ALTER DATABASE [LaundryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET CURSOR_DEFAULT  GLOBAL 
			GO
			ALTER DATABASE [LaundryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET NUMERIC_ROUNDABORT OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET QUOTED_IDENTIFIER OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET RECURSIVE_TRIGGERS OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET  DISABLE_BROKER 
			GO
			ALTER DATABASE [LaundryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET TRUSTWORTHY OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET PARAMETERIZATION SIMPLE 
			GO
			ALTER DATABASE [LaundryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET HONOR_BROKER_PRIORITY OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET RECOVERY SIMPLE 
			GO
			ALTER DATABASE [LaundryManagement] SET  MULTI_USER 
			GO
			ALTER DATABASE [LaundryManagement] SET PAGE_VERIFY CHECKSUM  
			GO
			ALTER DATABASE [LaundryManagement] SET DB_CHAINING OFF 
			GO
			ALTER DATABASE [LaundryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
			GO
			ALTER DATABASE [LaundryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
			GO
			ALTER DATABASE [LaundryManagement] SET DELAYED_DURABILITY = DISABLED 
			GO
			ALTER DATABASE [LaundryManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
			GO
			ALTER DATABASE [LaundryManagement] SET QUERY_STORE = OFF
			GO
			USE [LaundryManagement]
			GO
			/****** Object:  Table [dbo].[Article]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Article](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
				[IdItemType] [int] NOT NULL,
				[IdSize] [int] NOT NULL,
				[IdColor] [int] NOT NULL,
				[Washes] [int] NOT NULL,
			 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Category]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Category](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Color]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Color](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Item]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Item](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Code] [varchar](50) NOT NULL,
				[IdArticle] [int] NOT NULL,
				[Created] [datetime] NOT NULL,
				[IdItemStatus] [int] NOT NULL,
				[IdLocation] [int] NOT NULL,
				[Washes] [int] NULL,
			 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ItemStatus]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ItemStatus](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_ItemStatus] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ItemType]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ItemType](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
				[IdCategory] [int] NOT NULL,
			 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Language]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Language](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
				[Default] [bit] NOT NULL,
			 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Location]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Location](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
				[Address] [varchar](50) NOT NULL,
				[IsInternal] [bit] NOT NULL,
				[IdParentLocation] [int] NULL,
				[IdLocationType] [int] NOT NULL,
			 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[LocationType]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[LocationType](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_LocationType] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Log]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Log](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Date] [datetime] NOT NULL,
				[IdUser] [int] NULL,
				[IdMovementType] [int] NOT NULL,
				[Message] [varchar](max) NOT NULL,
				[IdLogLevel] [int] NOT NULL,
			 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[LogLevel]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[LogLevel](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Description] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_LogLevel] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[MovementType]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[MovementType](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_MovementType] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Permission]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Permission](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
				[Permission] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[PermissionFamily]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[PermissionFamily](
				[IdPermissionParent] [int] NOT NULL,
				[IdPermission] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Reception]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Reception](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[IdLocationOrigin] [int] NOT NULL,
				[IdLocationDestination] [int] NOT NULL,
				[CreationDate] [datetime] NOT NULL,
				[IdCreationUser] [int] NOT NULL,
			 CONSTRAINT [PK_Reception] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ReceptionDetail]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ReceptionDetail](
				[IdReception] [int] NOT NULL,
				[IdItem] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Roadmap]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Roadmap](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[CreatedDate] [datetime] NOT NULL,
				[IdCreationUser] [int] NOT NULL,
				[IdRoadmapStatus] [int] NOT NULL,
				[IdLocationOrigin] [int] NOT NULL,
				[IdLocationDestination] [int] NOT NULL,
			 CONSTRAINT [PK_Roadmap] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[RoadmapReception]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[RoadmapReception](
				[IdRoadmap] [int] NOT NULL,
				[IdReception] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[RoadmapShippings]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[RoadmapShippings](
				[IdRoadmap] [int] NOT NULL,
				[IdShipping] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[RoadmapStatus]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[RoadmapStatus](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_RoadmapStatus] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Shipping]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Shipping](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[IdLocationOrigin] [int] NOT NULL,
				[IdLocationDestination] [int] NOT NULL,
				[CreatedDate] [datetime] NOT NULL,
				[IdShippingType] [int] NOT NULL,
				[IdShippingStatus] [int] NOT NULL,
				[IdResponsibleUser] [int] NOT NULL,
				[IdCreatedUser] [int] NOT NULL,
				[CheckDigit] [binary](16) NULL,
			 CONSTRAINT [PK_Shipping] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ShippingDetail]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ShippingDetail](
				[IdShipping] [int] NOT NULL,
				[IdItem] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ShippingStatus]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ShippingStatus](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_ShippingStatus] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[ShippingType]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[ShippingType](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_ShippingType] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Size]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Size](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Tag]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Tag](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Name] [varchar](50) NOT NULL,
			 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Traceability]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Traceability](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[IdItem] [int] NOT NULL,
				[Date] [datetime] NOT NULL,
				[IdMovementType] [int] NOT NULL,
				[IdUser] [int] NOT NULL,
				[IdLocationOrigin] [int] NOT NULL,
				[IdLocationDestination] [int] NOT NULL,
				[IdItemStatus] [int] NOT NULL,
			 CONSTRAINT [PK_Traceability] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[Translations]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[Translations](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[IdTag] [int] NOT NULL,
				[IdLanguage] [int] NOT NULL,
				[Description] [varchar](250) NOT NULL,
			 CONSTRAINT [PK_Translations] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[User]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[User](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Email] [varchar](50) NOT NULL,
				[Password] [varchar](50) NOT NULL,
				[UserName] [varchar](50) NOT NULL,
				[FirstName] [varchar](50) NULL,
				[LastName] [varchar](50) NULL,
				[IdLanguage] [int] NOT NULL,
				[IdLocation] [int] NOT NULL,
				[CheckDigit] [binary](16) NOT NULL,
			 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[UserHistory]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[UserHistory](
				[Id] [int] IDENTITY(1,1) NOT NULL,
				[Email] [varchar](50) NOT NULL,
				[Password] [varchar](50) NOT NULL,
				[UserName] [varchar](50) NOT NULL,
				[FirstName] [varchar](50) NOT NULL,
				[LastName] [varchar](50) NOT NULL,
				[IdLanguage] [int] NOT NULL,
				[IdLocation] [int] NOT NULL,
				[IdUser] [int] NOT NULL,
				[Date] [datetime] NOT NULL,
			 CONSTRAINT [PK_UserHistory] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[UserPermission]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[UserPermission](
				[IdUser] [int] NOT NULL,
				[IdPermission] [int] NOT NULL
			) ON [PRIMARY]
			GO
			/****** Object:  Table [dbo].[VerticalCheckDigit]    Script Date: 14/12/2022 02:08:47 ******/
			SET ANSI_NULLS ON
			GO
			SET QUOTED_IDENTIFIER ON
			GO
			CREATE TABLE [dbo].[VerticalCheckDigit](
				[TableName] [varchar](50) NOT NULL,
				[CheckDigit] [binary](16) NOT NULL
			) ON [PRIMARY]
			GO
			SET IDENTITY_INSERT [dbo].[Article] ON 

			INSERT [dbo].[Article] ([Id], [Name], [IdItemType], [IdSize], [IdColor], [Washes]) VALUES (1, N'SAB001', 1, 1, 1, 10)
			INSERT [dbo].[Article] ([Id], [Name], [IdItemType], [IdSize], [IdColor], [Washes]) VALUES (2, N'SAB002', 1, 2, 1, 10)
			INSERT [dbo].[Article] ([Id], [Name], [IdItemType], [IdSize], [IdColor], [Washes]) VALUES (3, N'FUN001', 2, 3, 2, 10)
			SET IDENTITY_INSERT [dbo].[Article] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Category] ON 

			INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Ropa de cama')
			SET IDENTITY_INSERT [dbo].[Category] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Color] ON 

			INSERT [dbo].[Color] ([Id], [Name]) VALUES (1, N'Blanco')
			INSERT [dbo].[Color] ([Id], [Name]) VALUES (2, N'Gris')
			INSERT [dbo].[Color] ([Id], [Name]) VALUES (3, N'Verde')
			SET IDENTITY_INSERT [dbo].[Color] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Item] ON 

			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (1, N'00000001', 1, CAST(N'2022-06-20T22:40:15.167' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (2, N'00000002', 1, CAST(N'2022-06-20T22:40:15.167' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (3, N'00000003', 2, CAST(N'2022-06-20T22:40:15.167' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (4, N'00000004', 2, CAST(N'2022-06-20T22:40:15.167' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (5, N'00000005', 2, CAST(N'2022-06-20T22:40:15.167' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (6, N'00000006', 3, CAST(N'2022-08-10T01:08:35.543' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (7, N'00000007', 3, CAST(N'2022-08-10T01:08:35.543' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (8, N'00000008', 3, CAST(N'2022-08-10T01:08:35.543' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (9, N'00000009', 1, CAST(N'2022-08-10T01:08:35.543' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (10, N'00000010', 2, CAST(N'2022-08-10T01:08:35.543' AS DateTime), 1, 1, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (11, N'00000011', 1, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (12, N'00000012', 1, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (13, N'00000013', 2, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (14, N'00000014', 2, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (15, N'00000015', 3, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (16, N'00000016', 3, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (17, N'00000017', 3, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (18, N'00000018', 3, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (19, N'00000019', 1, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			INSERT [dbo].[Item] ([Id], [Code], [IdArticle], [Created], [IdItemStatus], [IdLocation], [Washes]) VALUES (20, N'00000020', 2, CAST(N'2022-11-05T18:46:41.333' AS DateTime), 1, 4, 10)
			SET IDENTITY_INSERT [dbo].[Item] OFF
			GO
			SET IDENTITY_INSERT [dbo].[ItemStatus] ON 

			INSERT [dbo].[ItemStatus] ([Id], [Name]) VALUES (1, N'On location')
			INSERT [dbo].[ItemStatus] ([Id], [Name]) VALUES (2, N'Pending sent')
			INSERT [dbo].[ItemStatus] ([Id], [Name]) VALUES (3, N'Sent')
			INSERT [dbo].[ItemStatus] ([Id], [Name]) VALUES (4, N'Deleted')
			SET IDENTITY_INSERT [dbo].[ItemStatus] OFF
			GO
			SET IDENTITY_INSERT [dbo].[ItemType] ON 

			INSERT [dbo].[ItemType] ([Id], [Name], [IdCategory]) VALUES (1, N'Sabana', 1)
			INSERT [dbo].[ItemType] ([Id], [Name], [IdCategory]) VALUES (2, N'Funda de almohada', 1)
			SET IDENTITY_INSERT [dbo].[ItemType] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Language] ON 

			INSERT [dbo].[Language] ([Id], [Name], [Default]) VALUES (1, N'English', 1)
			INSERT [dbo].[Language] ([Id], [Name], [Default]) VALUES (2, N'Español', 0)
			SET IDENTITY_INSERT [dbo].[Language] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Location] ON 

			INSERT [dbo].[Location] ([Id], [Name], [Address], [IsInternal], [IdParentLocation], [IdLocationType]) VALUES (1, N'Lavanderia', N'Test 123', 0, NULL, 1)
			INSERT [dbo].[Location] ([Id], [Name], [Address], [IsInternal], [IdParentLocation], [IdLocationType]) VALUES (2, N'Esterilización', N'Test 123 PB', 1, 1, 1)
			INSERT [dbo].[Location] ([Id], [Name], [Address], [IsInternal], [IdParentLocation], [IdLocationType]) VALUES (3, N'Costura', N'Test 123 PB', 1, 1, 1)
			INSERT [dbo].[Location] ([Id], [Name], [Address], [IsInternal], [IdParentLocation], [IdLocationType]) VALUES (4, N'Sanatorio Juncal', N'Av. Alsina 2469', 0, NULL, 2)
			INSERT [dbo].[Location] ([Id], [Name], [Address], [IsInternal], [IdParentLocation], [IdLocationType]) VALUES (5, N'Piso 1', N'Av. Alsina 2469 P1', 1, 4, 2)
			SET IDENTITY_INSERT [dbo].[Location] OFF
			GO
			SET IDENTITY_INSERT [dbo].[LocationType] ON 

			INSERT [dbo].[LocationType] ([Id], [Name]) VALUES (1, N'Laundry')
			INSERT [dbo].[LocationType] ([Id], [Name]) VALUES (2, N'Clinic')
			SET IDENTITY_INSERT [dbo].[LocationType] OFF
			GO
			SET IDENTITY_INSERT [dbo].[LogLevel] ON 

			INSERT [dbo].[LogLevel] ([Id], [Description]) VALUES (1, N'Information')
			INSERT [dbo].[LogLevel] ([Id], [Description]) VALUES (2, N'Warning')
			INSERT [dbo].[LogLevel] ([Id], [Description]) VALUES (3, N'Error')
			SET IDENTITY_INSERT [dbo].[LogLevel] OFF
			GO
			SET IDENTITY_INSERT [dbo].[MovementType] ON 

			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (1, N'Laundry Shipping')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (2, N'Clinic Shipping')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (3, N'Internal Shipping')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (4, N'Road Map')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (5, N'Laundry Reception')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (6, N'Clinic Reception')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (7, N'Login')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (8, N'Reset Password')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (9, N'Logout')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (10, N'Manual Password Reset')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (11, N'User')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (12, N'User History')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (13, N'Backup')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (14, N'Restore')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (15, N'User Roles')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (16, N'Permission')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (17, N'CorruptedEntities')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (18, N'IntegrityRestore')
			INSERT [dbo].[MovementType] ([Id], [Name]) VALUES (19, N'Reception')
			SET IDENTITY_INSERT [dbo].[MovementType] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Permission] ON 

			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (6, N'Administration', N'ADM')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (7, N'Reports', N'REP')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (8, N'Processes', N'PRO')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (10, N'Categories Administration', N'ADM_CAT')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (11, N'Item Types Administration', N'ADM_TYP')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (12, N'Articles Administration', N'ADM_ART')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (13, N'Sizes Administration', N'ADM_SIZ')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (14, N'Colors Administration', N'ADM_COL')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (15, N'Users Administration', N'ADM_USR')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (16, N'Roles Administration', N'ADM_ROL')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (17, N'Laundry Shipping Process', N'PRO_SHP_LDY')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (19, N'Internal Shipping Process', N'PRO_SHP_INT')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (20, N'Reception Process', N'PRO_REC')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (22, N'Item Creation Process', N'PRO_ITM_NEW')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (23, N'Item Delete Process', N'PRO_ITM_DEL')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (24, N'Road Map Process', N'PRO_ROA')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (25, N'Laundry Shipping Report', N'REP_SHP_LDY')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (27, N'Internal Shipping Report', N'REP_SHP_INT')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (29, N'Clinic Report', N'REP_REC')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (31, N'Movements Report', N'REP_MOV')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (35, N'Language', N'LAN')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (42, N'Locations Administration', N'ADM_LOC')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (43, N'Add Locations', N'ADM_LOC_ADD')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (44, N'Edit Locations', N'ADM_LOC_EDI')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (46, N'Traceability Report', N'REP_TRA')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (47, N'Stock Report', N'REP_STK')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (50, N'Backup Administration', N'ADM_BCK')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (51, N'Roadmap Rerport', N'REP_ROA')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (52, N'Logs Report', N'REP_LOGS')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (53, N'User History', N'ADM_USR_HIST')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (54, N'Reception Report', N'REP_REC')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (55, N'Clinic Shipping Process', N'PRO_SHP_CLI')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (56, N'Clinic Shipping Report', N'REP_SHP_CLI')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (57, N'Laundry Shipping Responsible', N'RESP_SHP_LDY')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (58, N'Clinic Shipping Responsible', N'RESP_SHP_CLI')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (59, N'Internal Shipping Responsible', N'RESP_SHP_INT')
			INSERT [dbo].[Permission] ([Id], [Name], [Permission]) VALUES (60, N'Stock Process', N'PRO_STK')
			SET IDENTITY_INSERT [dbo].[Permission] OFF
			GO
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 25)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 17)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 27)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 29)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 31)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 46)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 47)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 51)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 19)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 20)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 22)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 23)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 24)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 55)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 52)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 54)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (7, 56)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 57)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 10)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 11)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 12)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 13)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 14)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 15)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 16)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 42)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (6, 50)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 58)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 59)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (15, 53)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (42, 44)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (42, 43)
			INSERT [dbo].[PermissionFamily] ([IdPermissionParent], [IdPermission]) VALUES (8, 60)
			GO
			SET IDENTITY_INSERT [dbo].[RoadmapStatus] ON 

			INSERT [dbo].[RoadmapStatus] ([Id], [Name]) VALUES (1, N'Sent')
			INSERT [dbo].[RoadmapStatus] ([Id], [Name]) VALUES (2, N'Received')
			SET IDENTITY_INSERT [dbo].[RoadmapStatus] OFF
			GO
			SET IDENTITY_INSERT [dbo].[ShippingStatus] ON 

			INSERT [dbo].[ShippingStatus] ([Id], [Name]) VALUES (1, N'Created')
			INSERT [dbo].[ShippingStatus] ([Id], [Name]) VALUES (2, N'Sent')
			INSERT [dbo].[ShippingStatus] ([Id], [Name]) VALUES (3, N'Received')
			SET IDENTITY_INSERT [dbo].[ShippingStatus] OFF
			GO
			SET IDENTITY_INSERT [dbo].[ShippingType] ON 

			INSERT [dbo].[ShippingType] ([Id], [Name]) VALUES (1, N'ToClinic')
			INSERT [dbo].[ShippingType] ([Id], [Name]) VALUES (2, N'ToLaundry')
			INSERT [dbo].[ShippingType] ([Id], [Name]) VALUES (3, N'Internal')
			SET IDENTITY_INSERT [dbo].[ShippingType] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Size] ON 

			INSERT [dbo].[Size] ([Id], [Name]) VALUES (1, N'Chico')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (2, N'Mediano')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (3, N'Grande')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (4, N'S')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (5, N'M')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (6, N'L')
			INSERT [dbo].[Size] ([Id], [Name]) VALUES (7, N'XL')
			SET IDENTITY_INSERT [dbo].[Size] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Tag] ON 

			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (1, N'Login')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (2, N'Email')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (3, N'Password')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (4, N'ResetPassword')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (5, N'MainMenu')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (6, N'Administration')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (7, N'Processes')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (8, N'Reports')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (9, N'Users')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (10, N'Articles')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (11, N'Categories')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (12, N'ItemTypes')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (13, N'ClinicReception')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (14, N'ClinicShipping')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (15, N'InternalShipping')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (16, N'ItemCreation')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (17, N'ItemRemoval')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (18, N'LaundryReception')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (19, N'LaundryShipping')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (20, N'RoadMap')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (21, N'Movements')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (22, N'Shippings')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (23, N'NewPassword')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (24, N'RepeatPassword')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (25, N'Accept')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (27, N'Create')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (28, N'Edit')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (29, N'Delete')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (30, N'ViewRoles')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (31, N'EditRoles')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (32, N'Name')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (33, N'LastName')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (34, N'UserName')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (35, N'Save')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (36, N'User')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (37, N'Language')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (38, N'Logout')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (39, N'SessionAlreadyOpen')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (40, N'NonexistentUser')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (41, N'IncorrectPassword')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (42, N'DeleteLoggedUser')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (43, N'UserDuplicate')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (44, N'PasswordReset')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (45, N'EditLoggedUser')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (46, N'PasswordPlaceholder')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (47, N'FormValidationTextboxes')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (48, N'FormValidationGridRow')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (49, N'PasswordMatch')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (50, N'EmailFormat')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (51, N'Notification')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (52, N'PasswordPolicyHeader')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (53, N'PasswordPolicyHeader')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (54, N'PasswordPolicyLength')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (55, N'PasswordPolicyUppercase')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (56, N'PasswordPolicyLowercase')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (57, N'PasswordPolicySpecialCharacters')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (58, N'PasswordPolicyNumbers')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (60, N'Translations')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (61, N'AddRow')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (62, N'DeleteRow')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (63, N'ManageLanguages')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (64, N'DeleteDefaultLanguage')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (69, N'Origin')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (70, N'Destination')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (71, N'Item')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (72, N'Shipping')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (73, N'Families')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (74, N'Leafs')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (75, N'PermissionCode')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (76, N'Permissions')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (77, N'Add')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (78, N'Childs')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (79, N'LaundryShippings')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (80, N'FormValidationCombo')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (81, N'FormValidationTreeView')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (82, N'DateFrom')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (83, N'DateTo')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (84, N'Search')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (85, N'ShippingDetail')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (86, N'ViewDetail')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (87, N'ItemCode')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (88, N'Traceability')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (89, N'Responsible')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (90, N'ClinicShippings')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (91, N'On location')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (92, N'Deleted')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (93, N'Pending sent')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (94, N'Sent')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (95, N'All')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (96, N'Created')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (97, N'Received')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (98, N'PasswordResetEmailSubject')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (99, N'EmailAddressName')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (100, N'PasswordLimitMessage')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (101, N'PasswordResetEmailBody')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (102, N'ShippingEmailBody')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (103, N'ShippingEmailBody')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (104, N'Status')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (105, N'ItemType')
			GO
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (106, N'Ststus')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (107, N'SearchShippings')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (108, N'View')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (109, N'History')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (110, N'Apply')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (111, N'Location')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (112, N'Roadmaps')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (113, N'IntegrityAdminCredentials')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (114, N'ViewCorruptedEntities')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (115, N'IntegrityLastBackup')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (116, N'IntegrityManualChange')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (117, N'IntegrityRecalculate')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (118, N'ValidateCredentials')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (119, N'SearchRoadmaps')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (120, N'ExportSelected')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (121, N'StockImport')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (122, N'Import')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (123, N'ImportedItems')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (124, N'Export')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (125, N'Reception')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (126, N'InternalShippings')
			INSERT [dbo].[Tag] ([Id], [Name]) VALUES (127, N'StockDelete')
			SET IDENTITY_INSERT [dbo].[Tag] OFF
			GO
			SET IDENTITY_INSERT [dbo].[Translations] ON 

			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (1, 1, 1, N'Login')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (2, 2, 1, N'Email')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (3, 3, 1, N'Password')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (4, 4, 1, N'Reset password')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (5, 1, 2, N'Iniciar sesión')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (6, 2, 2, N'Correo electrónico')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (7, 3, 2, N'Contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (8, 4, 2, N'Restablecer contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (9, 5, 1, N'Main menu')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (10, 6, 1, N'Administration')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (11, 7, 1, N'Processes')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (12, 8, 1, N'Reports')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (13, 9, 1, N'Users')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (14, 10, 1, N'Articles')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (15, 11, 1, N'Categories')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (16, 12, 1, N'Item types')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (17, 13, 1, N'Clinic reception')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (18, 14, 1, N'Clinic shipping')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (19, 15, 1, N'Internal shipping')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (20, 16, 1, N'Item creation')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (21, 17, 1, N'Item removal')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (22, 18, 1, N'Laundry reception')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (23, 19, 1, N'Laundry shipping')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (24, 20, 1, N'Road map')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (25, 21, 1, N'Movements')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (26, 22, 1, N'Shippings')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (27, 5, 2, N'Menú principal')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (28, 6, 2, N'Administración')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (29, 7, 2, N'Procesos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (30, 8, 2, N'Reportes')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (31, 9, 2, N'Usuarios')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (32, 10, 2, N'Artículos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (33, 11, 2, N'Categorías')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (34, 12, 2, N'Tipos de prenda')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (35, 13, 2, N'Recepción en clínica')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (36, 14, 2, N'Envío a clínica')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (37, 15, 2, N'Envío interno')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (38, 16, 2, N'Alta de prendas')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (39, 17, 2, N'Baja de prendas')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (40, 18, 2, N'Recepción en lavadero')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (41, 19, 2, N'Envío a lavadero')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (42, 20, 2, N'Hoja de ruta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (43, 21, 2, N'Movimientos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (44, 22, 2, N'Envíos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (45, 23, 1, N'New password')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (46, 24, 1, N'Repeat the password')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (47, 23, 2, N'Nueva contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (48, 24, 2, N'Ingrese nuevamente la contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (49, 25, 1, N'Accept')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (50, 25, 2, N'Aceptar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (54, 27, 1, N'Create')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (55, 28, 1, N'Edit')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (56, 29, 1, N'Delete')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (57, 30, 1, N'View roles')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (58, 31, 1, N'Edit roles')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (60, 27, 2, N'Crear')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (61, 28, 2, N'Editar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (62, 29, 2, N'Eliminar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (63, 30, 2, N'Ver roles')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (64, 31, 2, N'Editar roles')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (65, 32, 1, N'Name')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (66, 33, 1, N'Last name')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (67, 34, 1, N'User name')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (68, 35, 1, N'Save')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (69, 36, 1, N'User')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (70, 32, 2, N'Nombre')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (71, 33, 2, N'Apellido')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (72, 34, 2, N'Nombre de usuario')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (73, 35, 2, N'Guardar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (74, 36, 2, N'Usuario')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (75, 37, 1, N'Language')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (76, 37, 2, N'Idioma')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (77, 38, 1, N'Logout')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (78, 38, 2, N'Cerrar sesión')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (79, 39, 1, N'A session is already open')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (80, 39, 2, N'Ya hay una sesión abierta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (81, 40, 1, N'User does not exists')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (82, 40, 2, N'El usuario no existe')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (83, 41, 1, N'The password is incorrect')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (84, 41, 2, N'La contraseña es incorrecta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (130, 42, 1, N'Cannot delete the logged user')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (131, 42, 2, N'No se puede eliminar el usuario con el que inició sesión')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (132, 43, 1, N'A user with the same name or email already exists')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (133, 43, 2, N'Ya existe un usuario con el mismo nombre o correo')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (134, 44, 1, N'The password has been reset')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (135, 44, 2, N'La contraseña se reestableció')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (136, 45, 1, N'Cannot edit the logged user')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (137, 45, 2, N'No se puede editar el usuario con el que inició sesión')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (138, 46, 1, N'Type here to change the password')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (139, 46, 2, N'Escriba aquí para modificar la contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (140, 47, 1, N'Must complete all the fields')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (141, 47, 2, N'Debe completar todos los campos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (142, 48, 1, N'Must select a row')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (143, 48, 2, N'Debe seleccionar un registro')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (144, 49, 1, N'The passwords must match')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (145, 49, 2, N'Las contraseñas deben coincidir')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (146, 50, 1, N'The email is not in a valid format')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (147, 50, 2, N'El correo no está en un formato válido')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (148, 51, 1, N'Notification')
			GO
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (149, 51, 2, N'Notificación')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (150, 52, 1, N'The password is not secure. It requires: ')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (151, 52, 2, N'La contraseña no es segura. Se requiere: ')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (152, 54, 1, N'At least {0} characters long')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (153, 54, 2, N'Al menos {0} caracteres de longitud')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (154, 55, 1, N'At least {0} uppercase letters')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (155, 55, 2, N'Al menos {0} letras mayúsculas')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (156, 56, 1, N'At least {0} lowercase letters')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (157, 56, 2, N'Al menos {0} letras minúsculas')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (158, 57, 1, N'At least {0} special characters')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (159, 57, 2, N'Al menos {0} caracters especiales')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (160, 58, 1, N'At least {0} numbers')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (161, 58, 2, N'Al menos {0} números')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (162, 60, 1, N'Translations')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (163, 60, 2, N'Traducciones')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (164, 61, 1, N'Add row')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (165, 61, 2, N'Agregar registro')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (166, 62, 1, N'Delete row')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (167, 62, 2, N'Eliminar registro')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (168, 63, 1, N'Manage language')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (169, 63, 2, N'Administrar lenguajes')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (170, 64, 1, N'Cannot delete the default language')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (171, 64, 2, N'No puede eliminar el lenguaje por defecto')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (486, 69, 2, N'Origen')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (487, 70, 2, N'Destino')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (488, 71, 2, N'Prenda')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (489, 69, 1, N'Origin')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (490, 70, 1, N'Destination')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (491, 71, 1, N'Item')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (492, 72, 2, N'Envío')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (493, 72, 1, N'Shipping')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (494, 73, 2, N'Familias')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (495, 74, 2, N'Hojas')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (496, 75, 2, N'Codigo de permiso')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (497, 74, 1, N'Leafs')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (498, 73, 1, N'Families')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (499, 75, 1, N'Permission code')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (500, 76, 2, N'Permisos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (501, 77, 2, N'Agregar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (502, 78, 2, N'Hijos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (503, 79, 2, N'Envíos a Lavadero')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (504, 79, 1, N'Laundry shippings')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (505, 80, 2, N'Debe completar todos los datos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (506, 80, 1, N'Must complete all the items')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (507, 81, 2, N'Debe seleccionar al menos un item del arbol')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (508, 81, 1, N'Must select at least one item from the tree')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (509, 82, 2, N'Fecha desde')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (510, 83, 2, N'Fecha hasta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (511, 84, 2, N'Buscar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (512, 85, 2, N'Detalle de envío')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (513, 86, 2, N'Ver detalle')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (514, 82, 1, N'Date from')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (515, 83, 1, N'Date to')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (516, 86, 1, N'View detail')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (517, 85, 1, N'Shipping detail')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (518, 87, 2, N'Codigo de prenda')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (519, 88, 2, N'Trazabilidad')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (520, 87, 1, N'Item code')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (835, 89, 2, N'Responsable')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (836, 90, 2, N'Envíos a Clínica')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (837, 91, 2, N'En ubicación')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (838, 92, 2, N'Eliminado')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (839, 93, 2, N'Pendiente de Envío')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (840, 91, 1, N'On Location')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (841, 93, 1, N'Pending Sent')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (842, 92, 1, N'Deleted')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (843, 94, 2, N'Enviado')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (844, 94, 1, N'Sent')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (845, 95, 2, N'Todos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (846, 95, 1, N'All')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (847, 96, 2, N'Creado')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (848, 97, 2, N'Recibido')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (849, 96, 1, N'Created')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (850, 97, 1, N'Received')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (851, 98, 2, N'Restablecimiento de contraseña')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (852, 99, 2, N'Sistema de Lavandería - Soporte')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (853, 98, 1, N'Password Reset')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (854, 99, 1, N'Laundry Management - Support')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (855, 100, 2, N'Ha superado el limite máximo de intentos de inicio de sesión. Recomendamos restablecer su contraseña en la siguiente pantalla')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (856, 100, 1, N'You have exceeded the maximum number of login attempts. We reccomend resetting your password in the following screen')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (857, 101, 1, N'<p>Your new password is <h1>{0}</h1> </p> <p>Remember to change it when you log in again</p> <p>Thank you</p>')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (858, 101, 2, N'<p>Su nueva contraseña es <h1>{0}</h1> </p> <p>Recuerde cambiarla cuando inicie sesión nuevamente</p> <p>Muchas gracias</p>')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (859, 102, 1, N'<p>The shipping {0} changed its state to {1}.</p> <p>Transaction date: {2}</p> <p>Transaction user: {3}')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (860, 102, 2, N'<p>El envío {0} cambió su estado a {1}.</p> <p>Fecha de operación: {2}</p> <p>Usuario de operación: {3}')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (861, 104, 2, N'Estado')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (862, 105, 2, N'Tipo de prenda')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (875, 107, 2, N'Buscar envíos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (876, 107, 1, N'Search shippings')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (877, 108, 2, N'Ver')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (878, 108, 1, N'View')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (879, 109, 2, N'Historial')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (880, 109, 1, N'History')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (881, 110, 2, N'Aplicar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (882, 110, 1, N'Apply')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (883, 111, 2, N'Ubicacion')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (884, 112, 2, N'Hojas de ruta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (885, 113, 1, N'The database integrity has been corrupted. The administrator has to enter his credentials to choose what to do')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (886, 113, 2, N'La integridad de la base de datos fue corrompida. El administrador debe ingresar sus credenciales para decidir como continuar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (887, 114, 1, N'View corrupted entities')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (888, 114, 2, N'Ver entidades corruptas')
			GO
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (889, 115, 1, N'Restore last backup')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (890, 115, 2, N'Restaurar ultimo backup')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (891, 116, 1, N'Change the database rows manually')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (892, 116, 2, N'Modificar los datos manualmente')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (893, 117, 1, N'Recalcultate the check digits')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (894, 117, 2, N'Recalcular los digitos verificadores')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (895, 118, 2, N'Validar credenciales')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (896, 118, 1, N'Validate credentials')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (897, 119, 2, N'Buscar hojas de ruta')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (898, 119, 1, N'Search roadmaps')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (899, 120, 2, N'Exportar item seleccionado')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (900, 120, 1, N'Export selected item')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (901, 121, 2, N'Subida de stock')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (902, 121, 1, N'Stock import')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (903, 122, 2, N'Importar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (904, 122, 1, N'Import')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (905, 123, 2, N'Registros importados')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (906, 123, 1, N'Imported items')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (907, 124, 2, N'Exportar')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (908, 124, 1, N'Export')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (909, 125, 2, N'Recepcion')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (910, 125, 1, N'Reception')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (911, 126, 2, N'Envios internos')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (912, 126, 1, N'Internal shippings')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (913, 127, 2, N'Eliminacion de stock')
			INSERT [dbo].[Translations] ([Id], [IdTag], [IdLanguage], [Description]) VALUES (914, 127, 1, N'Stock delete')
			SET IDENTITY_INSERT [dbo].[Translations] OFF
			GO
			SET IDENTITY_INSERT [dbo].[User] ON 

			INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [CheckDigit]) VALUES (1, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 0xF921B54B35A338A05D5096FFB5709C48)
			INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [CheckDigit]) VALUES (4, N'c@test.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'cperez', N'Carlos', N'Pérez', 2, 1, 0x3A361DFDDB07BF3BAADD47BEEE4767DE)
			INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [CheckDigit]) VALUES (5, N'lavanderia@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'lavanderia', N'Lavanderia', N'Usuario', 2, 1, 0x1DAC59A7EF34D516CA610E28D8FD9248)
			INSERT [dbo].[User] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [CheckDigit]) VALUES (6, N'clinica@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'clinica', N'Clinica', N'Usuario', 2, 4, 0x937BE1FD50BE807AB316C5FD0B6491ED)
			SET IDENTITY_INSERT [dbo].[User] OFF
			GO
			SET IDENTITY_INSERT [dbo].[UserHistory] ON 

			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (1, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-10-31T02:14:28.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (2, N'jcavallo112@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-10-31T02:14:52.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (3, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-10-31T02:15:23.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (4, N'1234567@test.com', N'A1EvBpGSYDtAkh7qq0b7ZQ==', N'cperez', N'Carlos', N'Pérez', 1, 5, 4, CAST(N'2022-10-31T22:45:36.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (5, N'1234567@test.com', N'Pa+0xzle3Pm9ztNtHz0hpQ==', N'cperez', N'Carlos', N'Pérez', 1, 5, 4, CAST(N'2022-10-31T22:49:51.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (6, N'1234567@test.com', N'kRSuQoY316el594jnoMGVg==', N'cperez', N'Carlos', N'Pérez', 1, 5, 4, CAST(N'2022-10-31T22:51:09.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (7, N'c@test.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'cperez', N'Carlos', N'Pérez', 1, 1, 4, CAST(N'2022-11-02T00:26:29.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (8, N'c@test.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'cperez', N'Carlos', N'Pérez', 2, 1, 4, CAST(N'2022-11-02T00:26:42.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (9, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 1, 1, CAST(N'2022-11-05T18:53:23.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (10, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-11-06T17:27:42.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (11, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 1, 1, CAST(N'2022-11-06T17:29:05.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (12, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-11-22T00:05:36.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (13, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 1, 1, CAST(N'2022-11-23T23:47:53.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (14, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-11-23T23:50:19.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (15, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 1, 1, CAST(N'2022-11-23T23:51:07.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (16, N'jcavallo11@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'jcavallo', N'Julian', N'Cavallo', 2, 4, 1, CAST(N'2022-11-29T00:00:38.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (17, N'lavanderia@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'lavanderia', N'Lavanderia', N'Usuario', 1, 1, 5, CAST(N'2022-11-30T21:28:04.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (18, N'clinica@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'clinica', N'Clinica', N'Usuario', 1, 4, 6, CAST(N'2022-11-30T21:28:26.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (19, N'lavanderia@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'lavanderia', N'Lavanderia', N'Usuario', 2, 1, 5, CAST(N'2022-11-30T21:30:28.000' AS DateTime))
			INSERT [dbo].[UserHistory] ([Id], [Email], [Password], [UserName], [FirstName], [LastName], [IdLanguage], [IdLocation], [IdUser], [Date]) VALUES (20, N'clinica@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'clinica', N'Clinica', N'Usuario', 2, 4, 6, CAST(N'2022-12-12T00:38:40.000' AS DateTime))
			SET IDENTITY_INSERT [dbo].[UserHistory] OFF
			GO
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 6)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 10)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 11)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 12)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 13)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 14)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 15)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 16)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 7)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 25)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 6)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 27)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 29)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 31)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 8)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 17)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 10)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 11)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 12)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 13)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 19)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 20)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 22)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 23)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 24)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (4, 35)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 14)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 15)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 53)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 16)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 42)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 43)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 44)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 50)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 7)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 25)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 27)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 29)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 31)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 46)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 47)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 51)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 52)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 54)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 8)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 17)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 19)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 20)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 22)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 23)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 24)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (6, 35)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 6)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 10)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 11)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 12)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 13)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 14)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 15)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 53)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 16)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 42)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 43)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 44)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 50)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 7)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 27)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 29)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 31)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 46)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 47)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 51)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 52)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 54)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 56)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 8)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 19)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 20)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 22)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 23)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 24)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 55)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 60)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 6)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 10)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 11)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 12)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 13)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 14)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 15)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 53)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 16)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 42)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 43)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 44)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 50)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 7)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 25)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 27)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 29)
			GO
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 31)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 46)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 47)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 51)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 52)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 54)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 8)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 17)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 19)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 20)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 22)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 23)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 24)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 57)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 58)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 59)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (1, 35)
			INSERT [dbo].[UserPermission] ([IdUser], [IdPermission]) VALUES (5, 35)
			GO
			INSERT [dbo].[VerticalCheckDigit] ([TableName], [CheckDigit]) VALUES (N'User', 0x5042D80960ED185104E54B2BE955B067)
			GO
			/****** Object:  Index [IX_ShippingDetail]    Script Date: 14/12/2022 02:08:48 ******/
			ALTER TABLE [dbo].[ShippingDetail] ADD  CONSTRAINT [IX_ShippingDetail] UNIQUE NONCLUSTERED 
			(
				[IdShipping] ASC,
				[IdItem] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			GO
			/****** Object:  Index [UK_Translations]    Script Date: 14/12/2022 02:08:48 ******/
			ALTER TABLE [dbo].[Translations] ADD  CONSTRAINT [UK_Translations] UNIQUE NONCLUSTERED 
			(
				[IdLanguage] ASC,
				[IdTag] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
			GO
			ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Color] FOREIGN KEY([IdColor])
			REFERENCES [dbo].[Color] ([Id])
			GO
			ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Color]
			GO
			ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ItemType] FOREIGN KEY([IdItemType])
			REFERENCES [dbo].[ItemType] ([Id])
			GO
			ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_ItemType]
			GO
			ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Size] FOREIGN KEY([IdSize])
			REFERENCES [dbo].[Size] ([Id])
			GO
			ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Size]
			GO
			ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Article] FOREIGN KEY([IdArticle])
			REFERENCES [dbo].[Article] ([Id])
			GO
			ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Article]
			GO
			ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemStatus] FOREIGN KEY([IdItemStatus])
			REFERENCES [dbo].[ItemStatus] ([Id])
			GO
			ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemStatus]
			GO
			ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Location] FOREIGN KEY([IdLocation])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Location]
			GO
			ALTER TABLE [dbo].[ItemType]  WITH CHECK ADD  CONSTRAINT [FK_ItemType_Category] FOREIGN KEY([IdCategory])
			REFERENCES [dbo].[Category] ([Id])
			GO
			ALTER TABLE [dbo].[ItemType] CHECK CONSTRAINT [FK_ItemType_Category]
			GO
			ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Location] FOREIGN KEY([IdParentLocation])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Location]
			GO
			ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_LocationType] FOREIGN KEY([IdLocationType])
			REFERENCES [dbo].[LocationType] ([Id])
			GO
			ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_LocationType]
			GO
			ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_LogLevel] FOREIGN KEY([IdLogLevel])
			REFERENCES [dbo].[LogLevel] ([Id])
			GO
			ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_LogLevel]
			GO
			ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Logs_MovementType] FOREIGN KEY([IdMovementType])
			REFERENCES [dbo].[MovementType] ([Id])
			GO
			ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Logs_MovementType]
			GO
			ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Logs_User] FOREIGN KEY([IdUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Logs_User]
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
			ALTER TABLE [dbo].[Reception]  WITH CHECK ADD  CONSTRAINT [FK_Reception_Location] FOREIGN KEY([IdLocationOrigin])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Reception] CHECK CONSTRAINT [FK_Reception_Location]
			GO
			ALTER TABLE [dbo].[Reception]  WITH CHECK ADD  CONSTRAINT [FK_Reception_Location1] FOREIGN KEY([IdLocationDestination])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Reception] CHECK CONSTRAINT [FK_Reception_Location1]
			GO
			ALTER TABLE [dbo].[Reception]  WITH CHECK ADD  CONSTRAINT [FK_Reception_User] FOREIGN KEY([IdCreationUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Reception] CHECK CONSTRAINT [FK_Reception_User]
			GO
			ALTER TABLE [dbo].[ReceptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceptionDetail_Item] FOREIGN KEY([IdItem])
			REFERENCES [dbo].[Item] ([Id])
			GO
			ALTER TABLE [dbo].[ReceptionDetail] CHECK CONSTRAINT [FK_ReceptionDetail_Item]
			GO
			ALTER TABLE [dbo].[ReceptionDetail]  WITH CHECK ADD  CONSTRAINT [FK_ReceptionDetail_Reception] FOREIGN KEY([IdReception])
			REFERENCES [dbo].[Reception] ([Id])
			GO
			ALTER TABLE [dbo].[ReceptionDetail] CHECK CONSTRAINT [FK_ReceptionDetail_Reception]
			GO
			ALTER TABLE [dbo].[Roadmap]  WITH CHECK ADD  CONSTRAINT [FK_Roadmap_Location] FOREIGN KEY([IdLocationOrigin])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Roadmap] CHECK CONSTRAINT [FK_Roadmap_Location]
			GO
			ALTER TABLE [dbo].[Roadmap]  WITH CHECK ADD  CONSTRAINT [FK_Roadmap_Location1] FOREIGN KEY([IdLocationDestination])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Roadmap] CHECK CONSTRAINT [FK_Roadmap_Location1]
			GO
			ALTER TABLE [dbo].[Roadmap]  WITH CHECK ADD  CONSTRAINT [FK_Roadmap_RoadmapStatus] FOREIGN KEY([IdRoadmapStatus])
			REFERENCES [dbo].[RoadmapStatus] ([Id])
			GO
			ALTER TABLE [dbo].[Roadmap] CHECK CONSTRAINT [FK_Roadmap_RoadmapStatus]
			GO
			ALTER TABLE [dbo].[Roadmap]  WITH CHECK ADD  CONSTRAINT [FK_Roadmap_User] FOREIGN KEY([IdCreationUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Roadmap] CHECK CONSTRAINT [FK_Roadmap_User]
			GO
			ALTER TABLE [dbo].[RoadmapReception]  WITH CHECK ADD  CONSTRAINT [FK_RoadmapReception_Reception] FOREIGN KEY([IdReception])
			REFERENCES [dbo].[Reception] ([Id])
			GO
			ALTER TABLE [dbo].[RoadmapReception] CHECK CONSTRAINT [FK_RoadmapReception_Reception]
			GO
			ALTER TABLE [dbo].[RoadmapReception]  WITH CHECK ADD  CONSTRAINT [FK_RoadmapReception_Roadmap] FOREIGN KEY([IdRoadmap])
			REFERENCES [dbo].[Roadmap] ([Id])
			GO
			ALTER TABLE [dbo].[RoadmapReception] CHECK CONSTRAINT [FK_RoadmapReception_Roadmap]
			GO
			ALTER TABLE [dbo].[RoadmapShippings]  WITH CHECK ADD  CONSTRAINT [FK_RoadmapShippings_Roadmap] FOREIGN KEY([IdRoadmap])
			REFERENCES [dbo].[Roadmap] ([Id])
			GO
			ALTER TABLE [dbo].[RoadmapShippings] CHECK CONSTRAINT [FK_RoadmapShippings_Roadmap]
			GO
			ALTER TABLE [dbo].[RoadmapShippings]  WITH CHECK ADD  CONSTRAINT [FK_RoadmapShippings_Shipping] FOREIGN KEY([IdShipping])
			REFERENCES [dbo].[Shipping] ([Id])
			GO
			ALTER TABLE [dbo].[RoadmapShippings] CHECK CONSTRAINT [FK_RoadmapShippings_Shipping]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_Location] FOREIGN KEY([IdLocationDestination])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_Location]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_Location1] FOREIGN KEY([IdLocationOrigin])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_Location1]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_ShippingStatus] FOREIGN KEY([IdShippingStatus])
			REFERENCES [dbo].[ShippingStatus] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_ShippingStatus]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_ShippingType] FOREIGN KEY([IdShippingType])
			REFERENCES [dbo].[ShippingType] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_ShippingType]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_User] FOREIGN KEY([IdResponsibleUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_User]
			GO
			ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_User1] FOREIGN KEY([IdCreatedUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_User1]
			GO
			ALTER TABLE [dbo].[ShippingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShippingDetail_Shipping] FOREIGN KEY([IdShipping])
			REFERENCES [dbo].[Shipping] ([Id])
			GO
			ALTER TABLE [dbo].[ShippingDetail] CHECK CONSTRAINT [FK_ShippingDetail_Shipping]
			GO
			ALTER TABLE [dbo].[ShippingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShippingDetail_ShippingDetail] FOREIGN KEY([IdItem])
			REFERENCES [dbo].[Item] ([Id])
			GO
			ALTER TABLE [dbo].[ShippingDetail] CHECK CONSTRAINT [FK_ShippingDetail_ShippingDetail]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_Item] FOREIGN KEY([IdItem])
			REFERENCES [dbo].[Item] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_Item]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_ItemStatus] FOREIGN KEY([IdItemStatus])
			REFERENCES [dbo].[ItemStatus] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_ItemStatus]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_Location] FOREIGN KEY([IdLocationOrigin])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_Location]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_Location1] FOREIGN KEY([IdLocationDestination])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_Location1]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_MovementType] FOREIGN KEY([IdMovementType])
			REFERENCES [dbo].[MovementType] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_MovementType]
			GO
			ALTER TABLE [dbo].[Traceability]  WITH CHECK ADD  CONSTRAINT [FK_Traceability_User] FOREIGN KEY([IdUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[Traceability] CHECK CONSTRAINT [FK_Traceability_User]
			GO
			ALTER TABLE [dbo].[Translations]  WITH CHECK ADD  CONSTRAINT [FK_Translations_Language] FOREIGN KEY([IdLanguage])
			REFERENCES [dbo].[Language] ([Id])
			GO
			ALTER TABLE [dbo].[Translations] CHECK CONSTRAINT [FK_Translations_Language]
			GO
			ALTER TABLE [dbo].[Translations]  WITH CHECK ADD  CONSTRAINT [FK_Translations_Tag] FOREIGN KEY([IdTag])
			REFERENCES [dbo].[Tag] ([Id])
			GO
			ALTER TABLE [dbo].[Translations] CHECK CONSTRAINT [FK_Translations_Tag]
			GO
			ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Language] FOREIGN KEY([IdLanguage])
			REFERENCES [dbo].[Language] ([Id])
			GO
			ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Language]
			GO
			ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Location] FOREIGN KEY([IdLocation])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Location]
			GO
			ALTER TABLE [dbo].[UserHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserHistory_Language] FOREIGN KEY([IdLanguage])
			REFERENCES [dbo].[Language] ([Id])
			GO
			ALTER TABLE [dbo].[UserHistory] CHECK CONSTRAINT [FK_UserHistory_Language]
			GO
			ALTER TABLE [dbo].[UserHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserHistory_Location] FOREIGN KEY([IdLocation])
			REFERENCES [dbo].[Location] ([Id])
			GO
			ALTER TABLE [dbo].[UserHistory] CHECK CONSTRAINT [FK_UserHistory_Location]
			GO
			ALTER TABLE [dbo].[UserHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserHistory_User] FOREIGN KEY([IdUser])
			REFERENCES [dbo].[User] ([Id])
			GO
			ALTER TABLE [dbo].[UserHistory] CHECK CONSTRAINT [FK_UserHistory_User]
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
			GO";
	}
}
