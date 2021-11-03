USE [master]
GO
/****** Object:  Database [QLKCL]    Script Date: 21-Oct-21 9:15:01 PM ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 21-Oct-21 9:15:02 PM ******/
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
/****** Object:  Table [dbo].[DestinationHistory]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DestinationHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateArrive] [date] NOT NULL,
	[quarantinePersonID] [int] NULL,
	[addressID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthInformation]    Script Date: 21-Oct-21 9:15:02 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InjectionRecord]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InjectionRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateInjection] [date] NOT NULL,
	[vaccineName] [nvarchar](50) NOT NULL,
	[quarantinePersonID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineArea]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineArea](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[testCycle] [int] NOT NULL,
	[requiredDayToFinish] [int] NOT NULL,
	[addressID] [int] NULL,
	[managerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantinePerson]    Script Date: 21-Oct-21 9:15:02 PM ******/
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
	[healthInsuranceID] [varchar](20) NULL,
	[phoneNumber] [varchar](20) NULL,
	[level] [varchar](10) NOT NULL,
	[arrivedDate] [date] NOT NULL,
	[leaveDate] [date] NOT NULL,
	[quarantineDays] [int] NOT NULL,
	[addressID] [int] NOT NULL,
	[healthInformationID] [int] NOT NULL,
	[roomID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineRoom]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineRoom](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[displayName] [nvarchar](50) NOT NULL,
	[capacity] [int] NOT NULL,
	[level] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Severity]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Severity](
	[level] [varchar](10) NOT NULL,
	[description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[level] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[dateOfBirth] [date] NOT NULL,
	[sex] [nvarchar](10) NOT NULL,
	[citizenID] [varchar](20) NULL,
	[nationality] [nvarchar](20) NOT NULL,
	[healthInsuranceID] [varchar](20) NULL,
	[phoneNumber] [varchar](20) NULL,
	[addressID] [int] NOT NULL,
	[jobTitle] [nvarchar](50) NOT NULL,
	[department] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestingResult]    Script Date: 21-Oct-21 9:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestingResult](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateTesting] [date] NOT NULL,
	[isPositive] [bit] NOT NULL,
	[quarantinePersonID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([id], [username], [password]) VALUES (1, N'tunglete', N'a052015c462d64835f6dd40eecffabf4')
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (2, N'lamthon', N'45cdd9c9b0dcaeef9fbb154f45746a2f')
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (3, N'thonle', N'5647015c4a0be08b19fcf6ac21ad2d94')
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (4, N'phonluc', N'a6491f9211753cd5c857da1115492ba6')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Quaranti__E66AB1CF4A5DF9EB]    Script Date: 21-Oct-21 9:15:02 PM ******/
ALTER TABLE [dbo].[QuarantinePerson] ADD UNIQUE NONCLUSTERED 
(
	[healthInsuranceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Quaranti__E838FE0027C4E25F]    Script Date: 21-Oct-21 9:15:02 PM ******/
ALTER TABLE [dbo].[QuarantinePerson] ADD UNIQUE NONCLUSTERED 
(
	[citizenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Staff__E66AB1CF82A94ED3]    Script Date: 21-Oct-21 9:15:02 PM ******/
ALTER TABLE [dbo].[Staff] ADD UNIQUE NONCLUSTERED 
(
	[healthInsuranceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Staff__E838FE00E95504D9]    Script Date: 21-Oct-21 9:15:02 PM ******/
ALTER TABLE [dbo].[Staff] ADD UNIQUE NONCLUSTERED 
(
	[citizenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[InjectionRecord]  WITH CHECK ADD FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD FOREIGN KEY([managerID])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD FOREIGN KEY([healthInformationID])
REFERENCES [dbo].[HealthInformation] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD FOREIGN KEY([level])
REFERENCES [dbo].[Severity] ([level])
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD FOREIGN KEY([roomID])
REFERENCES [dbo].[QuarantineRoom] ([id])
GO
ALTER TABLE [dbo].[QuarantineRoom]  WITH CHECK ADD FOREIGN KEY([level])
REFERENCES [dbo].[Severity] ([level])
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[TestingResult]  WITH CHECK ADD FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
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