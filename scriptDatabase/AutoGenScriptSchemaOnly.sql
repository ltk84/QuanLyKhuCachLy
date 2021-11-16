USE [master]
GO
/****** Object:  Database [QLKCL]    Script Date: 16-Nov-21 8:07:06 PM ******/
CREATE DATABASE [QLKCL]
GO

USE [QLKCL]
GO


CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[username] [varchar](20) NULL,
	[password] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[province] [nvarchar](20) NOT NULL,
	[district] [nvarchar](20) NOT NULL,
	[ward] [nvarchar](20) NOT NULL,
	[streetName] [nvarchar](20) NULL,
	[apartmentNumber] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DestinationHistory]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DestinationHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateArrive] [date] NOT NULL,
	[quarantinePersonID] [int] NOT NULL,
	[addressID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthInformation]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthInformation](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[isFever] [bit] NOT NULL,
	[isCough] [bit] NOT NULL,
	[isSoreThroat] [bit] NOT NULL,
	[isLossOfTatse] [bit] NOT NULL,
	[isTired] [bit] NOT NULL,
	[isShortnessOfBreath] [bit] NOT NULL,
	[isOtherSymptoms] [bit] NOT NULL,
	[isDisease] [bit] NOT NULL,
	[quarantinePersonID] [int] NOT NULL,
 CONSTRAINT [PK__HealthIn__3213E83F1C6E3C93] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InjectionRecord]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InjectionRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateInjection] [date] NOT NULL,
	[vaccineName] [nvarchar](50) NOT NULL,
	[quarantinePersonID] [int] NULL,
 CONSTRAINT [PK__Injectio__3213E83FD945B0E4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineArea]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineArea](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[testCycle] [int] NOT NULL,
	[requiredDayToFinish] [int] NOT NULL,
	[addressID] [int] NOT NULL,
	[managerID] [int] NULL,
 CONSTRAINT [PK__Quaranti__3213E83F8BF8378F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantinePerson]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantinePerson](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[dateOfBirth] [date] NOT NULL,
	[sex] [nvarchar](10) NOT NULL,
	[citizenID] [varchar](20) NULL,
	[nationality] [nvarchar](20) NOT NULL,
	[phoneNumber] [varchar](20) NOT NULL,
	[levelID] [int] NULL,
	[arrivedDate] [date] NOT NULL,
	[leaveDate] [date] NOT NULL,
	[quarantineDays] [int] NOT NULL,
	[addressID] [int] NULL,
	[roomID] [int] NULL,
	[completeQuarantine] [bit] NULL,
 CONSTRAINT [PK__Quaranti__3213E83FE7EE8910] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineRoom]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineRoom](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[displayName] [nvarchar](50) NOT NULL,
	[capacity] [int] NOT NULL,
	[levelID] [int] NULL,
 CONSTRAINT [PK__Quaranti__3213E83F8214523F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Severity]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Severity](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[description] [nvarchar](50) NULL,
	[level] [nchar](20) NOT NULL,
 CONSTRAINT [PK__Severity__C03A140B17AA7CC4] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[dateOfBirth] [date] NOT NULL,
	[sex] [nvarchar](10) NOT NULL,
	[citizenID] [varchar](20) NOT NULL,
	[nationality] [nvarchar](20) NOT NULL,
	[healthInsuranceID] [varchar](20) NULL,
	[phoneNumber] [varchar](20) NOT NULL,
	[addressID] [int] NOT NULL,
	[jobTitle] [nvarchar](50) NOT NULL,
	[department] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestingResult]    Script Date: 16-Nov-21 8:07:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestingResult](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateTesting] [date] NOT NULL,
	[isPositive] [bit] NOT NULL,
	[quarantinePersonID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_CitizenID]    Script Date: 16-Nov-21 8:07:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_CitizenID] ON [dbo].[QuarantinePerson]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_CitizenID]    Script Date: 16-Nov-21 8:07:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_CitizenID] ON [dbo].[Staff]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_HealthInsurID]    Script Date: 16-Nov-21 8:07:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_HealthInsurID] ON [dbo].[Staff]
(
	[healthInsuranceID] ASC
)
WHERE ([healthInsuranceID] IS NOT NULL AND [healthInsuranceID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD  CONSTRAINT [FK__Destinati__quara__38996AB5] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DestinationHistory] CHECK CONSTRAINT [FK__Destinati__quara__38996AB5]
GO
ALTER TABLE [dbo].[HealthInformation]  WITH CHECK ADD  CONSTRAINT [FK_HealthInfor_Person] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HealthInformation] CHECK CONSTRAINT [FK_HealthInfor_Person]
GO
ALTER TABLE [dbo].[InjectionRecord]  WITH CHECK ADD  CONSTRAINT [FK__Injection__quara__398D8EEE] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InjectionRecord] CHECK CONSTRAINT [FK__Injection__quara__398D8EEE]
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__addre__3A81B327] FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantineArea] CHECK CONSTRAINT [FK__Quarantin__addre__3A81B327]
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD  CONSTRAINT [FK_QuarantineArea_managerID] FOREIGN KEY([managerID])
REFERENCES [dbo].[Staff] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[QuarantineArea] CHECK CONSTRAINT [FK_QuarantineArea_managerID]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__addre__3C69FB99] FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__addre__3C69FB99]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__level__3E52440B] FOREIGN KEY([levelID])
REFERENCES [dbo].[Severity] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__level__3E52440B]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__roomI__3F466844] FOREIGN KEY([roomID])
REFERENCES [dbo].[QuarantineRoom] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__roomI__3F466844]
GO
ALTER TABLE [dbo].[QuarantineRoom]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__level__403A8C7D] FOREIGN KEY([levelID])
REFERENCES [dbo].[Severity] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[QuarantineRoom] CHECK CONSTRAINT [FK__Quarantin__level__403A8C7D]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK__Staff__addressID__412EB0B6] FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK__Staff__addressID__412EB0B6]
GO
ALTER TABLE [dbo].[TestingResult]  WITH CHECK ADD  CONSTRAINT [FK__TestingRe__quara__4222D4EF] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestingResult] CHECK CONSTRAINT [FK__TestingRe__quara__4222D4EF]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [arriveLeaveDate] CHECK  (([leaveDate]>=[arrivedDate]))
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [arriveLeaveDate]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [sexPersonQP] CHECK  (([sex]=N'Nam' OR [sex]=N'Nữ'))
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [sexPersonQP]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [sexPersonStaff] CHECK  (([sex]=N'Nam' OR [sex]=N'Nữ'))
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [sexPersonStaff]
GO
USE [master]
GO
ALTER DATABASE [QLKCL] SET  READ_WRITE 
GO
