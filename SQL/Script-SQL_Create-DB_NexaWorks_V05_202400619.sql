USE [master]
GO
/****** Object:  Database [P6NexaWorks_01] ******/
CREATE DATABASE [P6NexaWorks_01]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'P6NexaWorks_01', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\P6NexaWorks_01.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'P6NexaWorks_01_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\P6NexaWorks_01_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [P6NexaWorks_01] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [P6NexaWorks_01].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [P6NexaWorks_01] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET ARITHABORT OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [P6NexaWorks_01] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [P6NexaWorks_01] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [P6NexaWorks_01] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET  ENABLE_BROKER 
GO

ALTER DATABASE [P6NexaWorks_01] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [P6NexaWorks_01] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [P6NexaWorks_01] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [P6NexaWorks_01] SET  MULTI_USER 
GO

ALTER DATABASE [P6NexaWorks_01] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [P6NexaWorks_01] SET DB_CHAINING OFF 
GO

ALTER DATABASE [P6NexaWorks_01] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [P6NexaWorks_01] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [P6NexaWorks_01] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [P6NexaWorks_01] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [P6NexaWorks_01] SET QUERY_STORE = OFF
GO

ALTER DATABASE [P6NexaWorks_01] SET  READ_WRITE 
GO

USE [P6NexaWorks_01]
GO
/****** Object:  Table [dbo].[Produit] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomProduit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Produit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Version] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomVersion] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemeExploitation] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemeExploitation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomSystemeExploitation] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_SystemeExploitation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incident] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incident](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Statut] [bit] NOT NULL,
	[Probleme] [nvarchar](300) NOT NULL,
	[Resolution] [nvarchar](300) NOT NULL,
	[DateCreation] [datetime2](7) NOT NULL,
	[DateResolution] [datetime2](7) NOT NULL,
	[ProduitVersion_SystemeExploitationId] [int] NOT NULL,
 CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit_Version] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit_Version](
	[Id] [int] IDENTITY(1,1) NOT NULL, --V03 update 	
	[ProduitId] [int] NOT NULL,
	[VersionId] [int] NOT NULL,
 CONSTRAINT [PK_Produit_Version] PRIMARY KEY CLUSTERED  
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO 
/****** Object:  Table [dbo].[ProduitVersion_SystemeExploitation] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProduitVersion_SystemeExploitation](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[Produit_VersionId] [int] NOT NULL,	
	[SystemeExploitationId] [int] NOT NULL,
 CONSTRAINT [PK_ProduitVersion_SystemeExploitation] PRIMARY KEY CLUSTERED  
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO 	
/****** Object: [[dbo].Incident] : defaults ******/
ALTER TABLE [dbo].[Incident] ADD  DEFAULT (CONVERT([bit],(1))) FOR [EnCours]
GO
/****** Object: [[dbo].Produit_Version] : foreign keys ******/
ALTER TABLE [dbo].[Produit_Version]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Version_Produit_ProduitId] FOREIGN KEY([ProduitId])
REFERENCES [dbo].[Produit] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produit_Version] CHECK CONSTRAINT [FK_Produit_Version_Produit_ProduitId]
GO
ALTER TABLE [dbo].[Produit_Version]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Version_Produit_VersionId] FOREIGN KEY([VersionId])
REFERENCES [dbo].[Version] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produit_Version] CHECK CONSTRAINT [FK_Produit_Version_Produit_VersionId]
GO
/****** Object: [[dbo].ProduitVersion_SystemeExploitation] : foreign keys ******/
ALTER TABLE [dbo].[ProduitVersion_SystemeExploitation]  WITH CHECK ADD  CONSTRAINT [FK_ProduitVersion_SystemeExploitation_Produit_Version_Produit_VersionId] FOREIGN KEY([Produit_VersionId])
REFERENCES [dbo].[Produit_Version] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProduitVersion_SystemeExploitation] CHECK CONSTRAINT [FK_ProduitVersion_SystemeExploitation_Produit_Version_Produit_VersionId]
GO
ALTER TABLE [dbo].[ProduitVersion_SystemeExploitation]  WITH CHECK ADD  CONSTRAINT [FK_ProduitVersion_SystemeExploitation_SystemeExploitation_SystemeExploitationId] FOREIGN KEY([SystemeExploitationId])
REFERENCES [dbo].[SystemeExploitation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProduitVersion_SystemeExploitation] CHECK CONSTRAINT [FK_ProduitVersion_SystemeExploitation_SystemeExploitation_SystemeExploitationId]
GO 
/****** Object: [[dbo].Incident] : foreign keys ******/
ALTER TABLE [dbo].[Incident]  WITH CHECK ADD  CONSTRAINT [FK_Incident_ProduitVersion_SystemeExploitation_ProduitVersion_SystemeExploitationId] FOREIGN KEY([ProduitVersion_SystemeExploitationId])
REFERENCES [dbo].[ProduitVersion_SystemeExploitation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Incident] CHECK CONSTRAINT [FK_Incident_ProduitVersion_SystemeExploitation_ProduitVersion_SystemeExploitationId]
GO
USE [master]
GO
ALTER DATABASE [P6NexaWorks_01] SET  READ_WRITE 
GO