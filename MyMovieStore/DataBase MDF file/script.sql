USE [master]
GO
/****** Object:  Database [MovieStore]    Script Date: 03-10-2019 7.11.55 AM ******/
CREATE DATABASE [MovieStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MovieStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MovieStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MovieStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MovieStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MovieStore] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MovieStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MovieStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MovieStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MovieStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MovieStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MovieStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [MovieStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MovieStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MovieStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MovieStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MovieStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MovieStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MovieStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MovieStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MovieStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MovieStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MovieStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MovieStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MovieStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MovieStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MovieStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MovieStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MovieStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MovieStore] SET RECOVERY FULL 
GO
ALTER DATABASE [MovieStore] SET  MULTI_USER 
GO
ALTER DATABASE [MovieStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MovieStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MovieStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MovieStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MovieStore] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MovieStore', N'ON'
GO
ALTER DATABASE [MovieStore] SET QUERY_STORE = OFF
GO
USE [MovieStore]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [int] IDENTITY(101,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Address] [varchar](max) NULL,
	[Phone] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1001,1) NOT NULL,
	[Title] [varchar](500) NULL,
	[Year] [varchar](25) NULL,
	[RentalCost] [int] NULL,
	[Genre] [varchar](50) NULL,
	[Rating] [int] NULL,
	[Flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentedMovies]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentedMovies](
	[RentId] [int] IDENTITY(1,1) NOT NULL,
	[MovieID] [int] NULL,
	[CustID] [int] NULL,
	[RentedDate] [date] NULL,
	[ReturnDate] [date] NULL,
	[TotalRent] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD FOREIGN KEY([CustID])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
/****** Object:  StoredProcedure [dbo].[prcCustWhoBorrowMostMovies]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE proc [dbo].[prcCustWhoBorrowMostMovies]
	as
	SELECT 
    rm.CustID,
	name as CustomerName,
    COUNT(*) occurrences
FROM RentedMovies rm join customer cc on cc.custid = rm.custid
GROUP BY
    rm.CustID, name
  
HAVING 
    COUNT(*) >1;
GO
/****** Object:  StoredProcedure [dbo].[prcDelCust]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create proc [dbo].[prcDelCust](@custId int)
  as
  delete customer where custId=@custId
GO
/****** Object:  StoredProcedure [dbo].[prcMostPopularMovies]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE proc [dbo].[prcMostPopularMovies]
 as
SELECT 
    rm.MovieID,title, COUNT(*) occurrences
FROM RentedMovies rm join movies m on m.movieid = rm.movieid
GROUP BY
    rm.MovieID, title
  
HAVING 
    COUNT(*) >1;
GO
/****** Object:  StoredProcedure [dbo].[prcRentMovie]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE proc [dbo].[prcRentMovie](@MovieID int,@CustID int,@RentDate date, @ReturnDate date,@TotalRent int)
  as
  insert into RentedMovies(MovieID,CustID,RentedDate,ReturnDate,TotalRent) values(@MovieID,@CustID,@RentDate,@ReturnDate,@TotalRent)
GO
/****** Object:  StoredProcedure [dbo].[prcReturnMovie]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create proc [dbo].[prcReturnMovie](@MovieID int,@Flag varchar(50))
  as
  delete from RentedMovies where MovieID=@MovieID;
  update Movies set Flag=@Flag where MovieID=@MovieID;
GO
/****** Object:  StoredProcedure [dbo].[prcUpdateMovieFlag]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create proc [dbo].[prcUpdateMovieFlag](@flag varchar(100),@movieID int)
  as
  update movies 
  set flag=@flag
  where MovieID=@movieID
GO
/****** Object:  StoredProcedure [dbo].[ShowRentedData]    Script Date: 03-10-2019 7.11.55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE proc [dbo].[ShowRentedData]
  as
  select RentId, name as CustomerName, Address, Title as MovieTitle,RentalCost,RentedDate,ReturnDate,TotalRent from RentedMovies rm join Customer c on c.CustID = rm.CustID join Movies m on m.MovieID= rm.MovieID
  order by RentedDate desc
GO
USE [master]
GO
ALTER DATABASE [MovieStore] SET  READ_WRITE 
GO
