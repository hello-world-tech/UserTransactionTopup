USE [master]
GO
/****** Object:  Database [EdenredApp]    Script Date: 2/14/2024 11:34:19 PM ******/
CREATE DATABASE [EdenredApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EdenredApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EdenredApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EdenredApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EdenredApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EdenredApp] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EdenredApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EdenredApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EdenredApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EdenredApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EdenredApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EdenredApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [EdenredApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EdenredApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EdenredApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EdenredApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EdenredApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EdenredApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EdenredApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EdenredApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EdenredApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EdenredApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EdenredApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EdenredApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EdenredApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EdenredApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EdenredApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EdenredApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EdenredApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EdenredApp] SET RECOVERY FULL 
GO
ALTER DATABASE [EdenredApp] SET  MULTI_USER 
GO
ALTER DATABASE [EdenredApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EdenredApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EdenredApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EdenredApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EdenredApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EdenredApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EdenredApp', N'ON'
GO
ALTER DATABASE [EdenredApp] SET QUERY_STORE = ON
GO
ALTER DATABASE [EdenredApp] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EdenredApp]
GO
/****** Object:  Table [dbo].[Beneficiary]    Script Date: 2/14/2024 11:34:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beneficiary](
	[BeneficiaryId] [int] IDENTITY(1,1) NOT NULL,
	[Nickname] [nvarchar](20) NOT NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BeneficiaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 2/14/2024 11:34:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[IsCredit] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/14/2024 11:34:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[IsVerified] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Beneficiary] ON 

INSERT [dbo].[Beneficiary] ([BeneficiaryId], [Nickname], [UserId]) VALUES (1, N'Aks', 1)
INSERT [dbo].[Beneficiary] ([BeneficiaryId], [Nickname], [UserId]) VALUES (2, N'Mis', 1)
INSERT [dbo].[Beneficiary] ([BeneficiaryId], [Nickname], [UserId]) VALUES (3, N'Test-1', 2)
INSERT [dbo].[Beneficiary] ([BeneficiaryId], [Nickname], [UserId]) VALUES (4, N'Test-2', 1)
SET IDENTITY_INSERT [dbo].[Beneficiary] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([TransactionId], [UserId], [Amount], [TransactionDate], [IsCredit]) VALUES (1005, NULL, CAST(500.00 AS Decimal(18, 2)), CAST(N'2024-02-14T18:53:57.297' AS DateTime), 0)
INSERT [dbo].[Transaction] ([TransactionId], [UserId], [Amount], [TransactionDate], [IsCredit]) VALUES (1006, NULL, CAST(99.00 AS Decimal(18, 2)), CAST(N'2024-02-14T18:58:16.513' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Balance], [IsVerified]) VALUES (1, N'Akshay Mistry', CAST(401.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[User] ([UserId], [UserName], [Balance], [IsVerified]) VALUES (2, N'Akash', CAST(1500.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_TransactionDate]  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsVerified]  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[Beneficiary]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
USE [master]
GO
ALTER DATABASE [EdenredApp] SET  READ_WRITE 
GO
