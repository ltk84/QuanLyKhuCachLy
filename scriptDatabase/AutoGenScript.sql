USE [master]
GO
/****** Object:  Database [QLKCL]    Script Date: 10-Nov-21 5:09:55 PM ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
/****** Object:  Table [dbo].[DestinationHistory]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
/****** Object:  Table [dbo].[HealthInformation]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
/****** Object:  Table [dbo].[InjectionRecord]    Script Date: 10-Nov-21 5:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InjectionRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateInjection] [date] NOT NULL,
	[vaccineName] [nvarchar](50) NOT NULL,
	[quarantinePersonID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineArea]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
	[managerID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantinePerson]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
	[phoneNumber] [varchar](20) NOT NULL,
	[levelID] [int] NOT NULL,
	[arrivedDate] [date] NOT NULL,
	[leaveDate] [date] NOT NULL,
	[quarantineDays] [int] NOT NULL,
	[addressID] [int] NULL,
	[healthInformationID] [int] NULL,
	[roomID] [int] NULL,
 CONSTRAINT [PK__Quaranti__3213E83FE7EE8910] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineRoom]    Script Date: 10-Nov-21 5:09:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineRoom](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[displayName] [nvarchar](50) NOT NULL,
	[capacity] [int] NOT NULL,
	[levelID] [int] NOT NULL,
 CONSTRAINT [PK__Quaranti__3213E83F8214523F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Severity]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
/****** Object:  Table [dbo].[TestingResult]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (1, N'tunglete', N'a052015c462d64835f6dd40eecffabf4')
GO
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (2, N'lamthon', N'45cdd9c9b0dcaeef9fbb154f45746a2f')
GO
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (3, N'thonle', N'5647015c4a0be08b19fcf6ac21ad2d94')
GO
INSERT [dbo].[Account] ([id], [username], [password]) VALUES (4, N'phonluc', N'a6491f9211753cd5c857da1115492ba6')
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Address] ON 
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (0, N'HCM', N'Binh Tan', N'BBHA', N'MBD', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (1, N'Binh Duong', N'Quan 1', N'Phu Tho Hoa', N'123', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (2, N'HCM', N'A', N'B', N'C', N'D')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (3, N'Binh Duong', N'Quan 1', N'Phu Tho Hoa', N'1223', N'33222')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (4, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'123', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (5, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'123', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (6, N'Binh Duong', N'Quan 1', N'Phu Thanh', N'adsadasd', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (7, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'1233123123', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (8, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', NULL, NULL)
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (9, N'Binh Duong', N'Quan 2', N'Phu Tho Hoa', NULL, NULL)
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (18, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'ABCASAS', N'12312')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (19, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
SET IDENTITY_INSERT [dbo].[HealthInformation] ON 
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (2, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (3, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (4, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (5, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (14, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (15, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (16, 1, 1, 1, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (19, 1, 1, 1, 1, 1, 1, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (20, 1, 1, 1, 0, 1, 1, 0, 1)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (21, 0, 1, 1, 1, 0, 0, 0, 1)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (22, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (23, 0, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease]) VALUES (28, 0, 0, 0, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[HealthInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantineArea] ON 
GO
INSERT [dbo].[QuarantineArea] ([id], [name], [testCycle], [requiredDayToFinish], [addressID], [managerID]) VALUES (0, N'BT', 2, 20, 2, 0)
GO
SET IDENTITY_INSERT [dbo].[QuarantineArea] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantinePerson] ON 
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (0, N'ALoa', CAST(N'2021-01-01' AS Date), N'Nữ', N'123123213', N'Ameriden', N'3213123123123', N'123123', 0, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, 23, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (5, N'asd', CAST(N'2021-11-04' AS Date), N'Nam', N' ', N'AM', NULL, N'123', 0, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, 23, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (6, N'asdds', CAST(N'2021-11-04' AS Date), N'Nam', N' ', N'AM', NULL, N'123', 0, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, 23, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (7, N'abc', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'', N'123', 0, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, 28, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuarantinePerson] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] ON 
GO
INSERT [dbo].[QuarantineRoom] ([id], [displayName], [capacity], [levelID]) VALUES (0, N'MoneyMan2', 21222, 1)
GO
INSERT [dbo].[QuarantineRoom] ([id], [displayName], [capacity], [levelID]) VALUES (1, N'Automatic', 20, 0)
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] OFF
GO
SET IDENTITY_INSERT [dbo].[Severity] ON 
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (0, N'Say goodbye', N'F1                  ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (1, N'Wake up and go!', N'F2                  ')
GO
SET IDENTITY_INSERT [dbo].[Severity] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 
GO
INSERT [dbo].[Staff] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [addressID], [jobTitle], [department]) VALUES (0, N'Soobin HS', CAST(N'2001-02-02' AS Date), N'Nam', N'123123', N'VN', N'123123', N'0202020', 1, N'Manager', N'Manage')
GO
INSERT [dbo].[Staff] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [addressID], [jobTitle], [department]) VALUES (5, N'ALo', CAST(N'2021-11-05' AS Date), N'Nam', N'123123123', N'VietNam', N'123123123213', N'123123123123', 18, N'ABC', N'BCA')
GO
INSERT [dbo].[Staff] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [addressID], [jobTitle], [department]) VALUES (6, N'BACASA', CAST(N'2021-11-05' AS Date), N'Nữ', N'32323232', N'VietNam', NULL, N'12312312312', 19, N'asd', N'asd')
GO
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_CitizenID]    Script Date: 10-Nov-21 5:09:56 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_CitizenID] ON [dbo].[QuarantinePerson]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_HealthInsurID]    Script Date: 10-Nov-21 5:09:56 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_HealthInsurID] ON [dbo].[QuarantinePerson]
(
	[healthInsuranceID] ASC
)
WHERE ([healthInsuranceID] IS NOT NULL AND [healthInsuranceID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_CitizenID]    Script Date: 10-Nov-21 5:09:56 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_CitizenID] ON [dbo].[Staff]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_HealthInsurID]    Script Date: 10-Nov-21 5:09:56 PM ******/
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
GO
ALTER TABLE [dbo].[DestinationHistory] CHECK CONSTRAINT [FK__Destinati__quara__38996AB5]
GO
ALTER TABLE [dbo].[InjectionRecord]  WITH CHECK ADD  CONSTRAINT [FK__Injection__quara__398D8EEE] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
GO
ALTER TABLE [dbo].[InjectionRecord] CHECK CONSTRAINT [FK__Injection__quara__398D8EEE]
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantineArea]  WITH CHECK ADD FOREIGN KEY([managerID])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__addre__3C69FB99] FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__addre__3C69FB99]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__healt__3D5E1FD2] FOREIGN KEY([healthInformationID])
REFERENCES [dbo].[HealthInformation] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__healt__3D5E1FD2]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__level__3E52440B] FOREIGN KEY([levelID])
REFERENCES [dbo].[Severity] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__level__3E52440B]
GO
ALTER TABLE [dbo].[QuarantinePerson]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__roomI__3F466844] FOREIGN KEY([roomID])
REFERENCES [dbo].[QuarantineRoom] ([id])
GO
ALTER TABLE [dbo].[QuarantinePerson] CHECK CONSTRAINT [FK__Quarantin__roomI__3F466844]
GO
ALTER TABLE [dbo].[QuarantineRoom]  WITH CHECK ADD  CONSTRAINT [FK__Quarantin__level__403A8C7D] FOREIGN KEY([levelID])
REFERENCES [dbo].[Severity] ([id])
GO
ALTER TABLE [dbo].[QuarantineRoom] CHECK CONSTRAINT [FK__Quarantin__level__403A8C7D]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[TestingResult]  WITH CHECK ADD  CONSTRAINT [FK__TestingRe__quara__4222D4EF] FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
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
