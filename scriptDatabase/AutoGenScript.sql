USE [master]
GO
/****** Object:  Database [QLKCL]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[DestinationHistory]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[HealthInformation]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[InjectionRecord]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[QuarantineArea]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[QuarantinePerson]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[QuarantineRoom]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[Severity]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
/****** Object:  Table [dbo].[TestingResult]    Script Date: 12-Nov-21 3:02:11 PM ******/
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
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (21, N'Binh Duong', N'Quan 1', N'Phu Thanh', N'', N'')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (22, N'Binh Duong', N'Quan 1', N'Phu Thanh', N'ABC', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (23, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'123', N'123')
GO
INSERT [dbo].[Address] ([id], [province], [district], [ward], [streetName], [apartmentNumber]) VALUES (24, N'Ho Chi Minh', N'Quan 1', N'Phu Thanh', N'123', N'123')
GO
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
SET IDENTITY_INSERT [dbo].[HealthInformation] ON 
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (2, 0, 0, 1, 0, 0, 0, 0, 0, 8)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (5, 0, 0, 0, 0, 0, 0, 0, 0, 11)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (6, 0, 0, 0, 0, 0, 0, 0, 0, 12)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (7, 0, 0, 0, 0, 0, 0, 0, 0, 13)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (8, 0, 0, 0, 0, 0, 0, 0, 0, 14)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (13, 0, 0, 0, 0, 0, 0, 0, 0, 19)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (14, 0, 0, 0, 0, 0, 0, 0, 0, 20)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (15, 0, 0, 0, 0, 0, 0, 0, 0, 21)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 22)
GO
INSERT [dbo].[HealthInformation] ([id], [isFever], [isCough], [isSoreThroat], [isLossOfTatse], [isTired], [isShortnessOfBreath], [isOtherSymptoms], [isDisease], [quarantinePersonID]) VALUES (17, 0, 0, 0, 0, 0, 0, 0, 0, 23)
GO
SET IDENTITY_INSERT [dbo].[HealthInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[InjectionRecord] ON 
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (3, CAST(N'2021-02-01' AS Date), N'Astra', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (9, CAST(N'2021-11-11' AS Date), N'(ALO)', NULL)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (10, CAST(N'2021-11-11' AS Date), N'(ALO)', NULL)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (11, CAST(N'2021-11-11' AS Date), N'(ALO)', NULL)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (12, CAST(N'2021-11-12' AS Date), N'(ALO)', 19)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (13, CAST(N'2021-11-12' AS Date), N'(ALO)', 20)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (14, CAST(N'2021-11-12' AS Date), N'(ALO)', 20)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (15, CAST(N'2021-11-01' AS Date), N'Vac', 21)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (16, CAST(N'2021-11-09' AS Date), N'Overdose', 22)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (17, CAST(N'2021-11-02' AS Date), N'LOL', 22)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (18, CAST(N'2021-11-02' AS Date), N'Tung h', 23)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (19, CAST(N'2021-11-12' AS Date), N'(ALO)', NULL)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (20, CAST(N'2021-11-12' AS Date), N'DT', NULL)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (21, CAST(N'2021-11-12' AS Date), N'BC', 6)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (23, CAST(N'2021-11-12' AS Date), N'(ALO)', 6)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (24, CAST(N'2021-11-12' AS Date), N'(ALO)', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (25, CAST(N'2021-11-12' AS Date), N'DCM', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (26, CAST(N'2021-11-12' AS Date), N'Ex''s hate me', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (27, CAST(N'2021-11-12' AS Date), N'Loi ac', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (28, CAST(N'2021-11-12' AS Date), N'a', 7)
GO
INSERT [dbo].[InjectionRecord] ([id], [dateInjection], [vaccineName], [quarantinePersonID]) VALUES (29, CAST(N'2021-11-12' AS Date), N'DB', 6)
GO
SET IDENTITY_INSERT [dbo].[InjectionRecord] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantineArea] ON 
GO
INSERT [dbo].[QuarantineArea] ([id], [name], [testCycle], [requiredDayToFinish], [addressID], [managerID]) VALUES (0, N'BA', 2, 2, 9, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuarantineArea] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantinePerson] ON 
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (6, N'asdds', CAST(N'2021-11-04' AS Date), N'Nam', N' ', N'AM', N'123', NULL, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (7, N'abc', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-10' AS Date), CAST(N'2021-11-30' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (8, N'Lam Thon', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-11' AS Date), CAST(N'2021-11-13' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (11, N'ALO', CAST(N'2021-11-04' AS Date), N'Nam', N'', N'VietNam', N'123123', NULL, CAST(N'2021-11-11' AS Date), CAST(N'2021-11-13' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (12, N'cc', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-11' AS Date), CAST(N'2021-11-13' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (13, N'asd', CAST(N'2021-11-04' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-11' AS Date), CAST(N'2021-11-13' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (14, N'ST', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (19, N'abc', CAST(N'2021-11-03' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (20, N'Then lan nua', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'213', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (21, N'ABCASASASA', CAST(N'2021-11-05' AS Date), N'Nam', N'', N'VietNam', N'231321312', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (22, N'ICE', CAST(N'2021-11-11' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [phoneNumber], [levelID], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [roomID], [completeQuarantine]) VALUES (23, N'Ryhm', CAST(N'2021-11-03' AS Date), N'Nam', N'', N'VietNam', N'123', NULL, CAST(N'2021-11-12' AS Date), CAST(N'2021-11-14' AS Date), 0, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuarantinePerson] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] ON 
GO
INSERT [dbo].[QuarantineRoom] ([id], [displayName], [capacity], [levelID]) VALUES (1, N'Automatic', 20, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] OFF
GO
SET IDENTITY_INSERT [dbo].[Severity] ON 
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (1, N'Wake up and go!', N'F2                  ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (2, N'King come', N'F1                  ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (3, N'(Description)', N'(Level)             ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (4, N'(Description)', N'(Level)             ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (5, N'(Description)', N'(Level)             ')
GO
INSERT [dbo].[Severity] ([id], [description], [level]) VALUES (6, N'(Description)', N'(Level)             ')
GO
SET IDENTITY_INSERT [dbo].[Severity] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_CitizenID]    Script Date: 12-Nov-21 3:02:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_CitizenID] ON [dbo].[QuarantinePerson]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_CitizenID]    Script Date: 12-Nov-21 3:02:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_CitizenID] ON [dbo].[Staff]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL AND [citizenID]<>'')
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_HealthInsurID]    Script Date: 12-Nov-21 3:02:12 PM ******/
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
