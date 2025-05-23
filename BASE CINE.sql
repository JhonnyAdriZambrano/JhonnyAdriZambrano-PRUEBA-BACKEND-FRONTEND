USE [master]
GO
/****** Object:  Database [CINE PRUEBA]    Script Date: 28/04/2025 5:06:45 ******/
CREATE DATABASE [CINE PRUEBA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CINE PRUEBA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CINE PRUEBA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CINE PRUEBA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CINE PRUEBA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CINE PRUEBA] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CINE PRUEBA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CINE PRUEBA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET ARITHABORT OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CINE PRUEBA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CINE PRUEBA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CINE PRUEBA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CINE PRUEBA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CINE PRUEBA] SET  MULTI_USER 
GO
ALTER DATABASE [CINE PRUEBA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CINE PRUEBA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CINE PRUEBA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CINE PRUEBA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CINE PRUEBA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CINE PRUEBA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CINE PRUEBA] SET QUERY_STORE = ON
GO
ALTER DATABASE [CINE PRUEBA] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CINE PRUEBA]
GO
/****** Object:  Table [dbo].[pelicula]    Script Date: 28/04/2025 5:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pelicula](
	[id_pelicula] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](200) NOT NULL,
	[duracion] [int] NULL,
	[activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pelicula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pelicula_salacine]    Script Date: 28/04/2025 5:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pelicula_salacine](
	[id_pelicula_salacine] [int] IDENTITY(1,1) NOT NULL,
	[id_sala_cine] [int] NOT NULL,
	[id_pelicula] [int] NOT NULL,
	[fecha_publicacion] [datetime] NOT NULL,
	[fecha_fin] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pelicula_salacine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sala_cine]    Script Date: 28/04/2025 5:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sala_cine](
	[id_sala] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[estado] [nvarchar](50) NULL,
	[activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_sala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[pelicula] ON 

INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2015, N'DMC5', 220, 1)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2016, N'WUWA', 225, 1)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2017, N'STAR', 300, 1)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2018, N'RAIL', 120, 1)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2019, N'POKEMON', 180, 1)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2020, N'DMCR', 200, 0)
INSERT [dbo].[pelicula] ([id_pelicula], [nombre], [duracion], [activo]) VALUES (2021, N'DMC2', 220, 1)
SET IDENTITY_INSERT [dbo].[pelicula] OFF
GO
SET IDENTITY_INSERT [dbo].[pelicula_salacine] ON 

INSERT [dbo].[pelicula_salacine] ([id_pelicula_salacine], [id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin]) VALUES (1, 1004, 2015, CAST(N'2025-04-28T00:00:00.000' AS DateTime), CAST(N'2025-04-29T00:00:00.000' AS DateTime))
INSERT [dbo].[pelicula_salacine] ([id_pelicula_salacine], [id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin]) VALUES (2, 1004, 2016, CAST(N'2025-04-28T00:00:00.000' AS DateTime), CAST(N'2025-04-30T00:00:00.000' AS DateTime))
INSERT [dbo].[pelicula_salacine] ([id_pelicula_salacine], [id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin]) VALUES (3, 1004, 2017, CAST(N'2025-04-29T00:00:00.000' AS DateTime), CAST(N'2025-04-30T00:00:00.000' AS DateTime))
INSERT [dbo].[pelicula_salacine] ([id_pelicula_salacine], [id_sala_cine], [id_pelicula], [fecha_publicacion], [fecha_fin]) VALUES (4, 1004, 2019, CAST(N'2025-04-28T00:00:00.000' AS DateTime), CAST(N'2025-04-30T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[pelicula_salacine] OFF
GO
SET IDENTITY_INSERT [dbo].[sala_cine] ON 

INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1003, N'Sala 1', N'ACTIVO', 1)
INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1004, N'Sala 5', N'ACTIVO', 1)
INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1005, N'Sala 11', N'ACTIVO', 1)
INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1006, N'Sala 9', N'ACTIVO', 1)
INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1007, N'Sala 24', N'ACTIVO', 0)
INSERT [dbo].[sala_cine] ([id_sala], [nombre], [estado], [activo]) VALUES (1008, N'string', N'string', 0)
SET IDENTITY_INSERT [dbo].[sala_cine] OFF
GO
ALTER TABLE [dbo].[pelicula] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[sala_cine] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[pelicula_salacine]  WITH CHECK ADD  CONSTRAINT [FK_pelicula_salacine_pelicula] FOREIGN KEY([id_pelicula])
REFERENCES [dbo].[pelicula] ([id_pelicula])
GO
ALTER TABLE [dbo].[pelicula_salacine] CHECK CONSTRAINT [FK_pelicula_salacine_pelicula]
GO
ALTER TABLE [dbo].[pelicula_salacine]  WITH CHECK ADD  CONSTRAINT [FK_pelicula_salacine_sala_cine] FOREIGN KEY([id_sala_cine])
REFERENCES [dbo].[sala_cine] ([id_sala])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[pelicula_salacine] CHECK CONSTRAINT [FK_pelicula_salacine_sala_cine]
GO
USE [master]
GO
ALTER DATABASE [CINE PRUEBA] SET  READ_WRITE 
GO
