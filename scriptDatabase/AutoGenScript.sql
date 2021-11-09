USE [master]
GO
/****** Object:  Database [QLKCL]    Script Date: 09-Nov-21 4:26:52 PM ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[DestinationHistory]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[HealthInformation]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[InjectionRecord]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[QuarantineArea]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[QuarantinePerson]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
	[level] [varchar](10) NOT NULL,
	[arrivedDate] [date] NOT NULL,
	[leaveDate] [date] NOT NULL,
	[quarantineDays] [int] NOT NULL,
	[addressID] [int] NULL,
	[healthInformationID] [int] NULL,
	[roomID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuarantineRoom]    Script Date: 09-Nov-21 4:26:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuarantineRoom](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[displayName] [nvarchar](50) NOT NULL,
	[capacity] [int] NOT NULL,
	[level] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Severity]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
/****** Object:  Table [dbo].[TestingResult]    Script Date: 09-Nov-21 4:26:53 PM ******/
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
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (1, N'Yeah', CAST(N'0001-01-01' AS Date), N'Nam', N'123', N'VietNam', N'123', N'123', N'F1', CAST(N'2021-11-08' AS Date), CAST(N'2021-11-28' AS Date), 0, NULL, 1, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (3, N'123', CAST(N'0001-01-01' AS Date), N'Nam', N'3232323', N'VietNam', N'432423423423', N'12312312', N'F1', CAST(N'2021-11-08' AS Date), CAST(N'2021-11-28' AS Date), 0, NULL, 3, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (4, N'Obito', CAST(N'0001-01-01' AS Date), N'Nam', N'55555', N'VietNam', N'322323122222', N'123123', N'F1', CAST(N'2021-11-08' AS Date), CAST(N'2021-11-28' AS Date), 0, 3, 4, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (5, N'323232', CAST(N'2021-11-05' AS Date), N'Nữ', NULL, N'VietNam', NULL, N'123123', N'F1', CAST(N'2021-11-08' AS Date), CAST(N'2021-11-28' AS Date), 0, 4, 5, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (14, N'Con Cho Lam', CAST(N'2021-11-04' AS Date), N'Nam', N'123345', N'VietNam', N'123456', N'123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, 5, 14, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (15, N'123123123', CAST(N'2021-11-05' AS Date), N'Nam', N'9999', N'VietNam', N'9999', N'3123123123123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, 6, 15, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (16, N'Bray', CAST(N'2021-11-03' AS Date), N'Nam', N'', N'VietNam', N'', N'3213123123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, 7, 16, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (19, N'MCK', CAST(N'2021-11-04' AS Date), N'Nam', N'3123123123', N'VietNam', N'123123123123', N'123123123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, NULL, 19, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (20, N'adasdas', CAST(N'2021-11-04' AS Date), N'Nam', N'1231223', N'VietNam', N'32323211122', N'123123123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, NULL, 20, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (21, N'3123123', CAST(N'2021-11-04' AS Date), N'Nam', N'4444', N'VietNam', N'44444', N'1232', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, NULL, 21, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (22, N'abc', CAST(N'2021-11-05' AS Date), N'Nam', N'123322', N'VietNam', N'11111', N'123123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, NULL, 22, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (23, N'LowG', CAST(N'2021-11-05' AS Date), N'Nam', NULL, N'VietNam', NULL, N'123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 0, NULL, 22, NULL)
GO
INSERT [dbo].[QuarantinePerson] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [level], [arrivedDate], [leaveDate], [quarantineDays], [addressID], [healthInformationID], [roomID]) VALUES (25, N'High', CAST(N'2021-11-05' AS Date), N'Nam', N'5516461', N'America', NULL, N'123', N'F1', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-29' AS Date), 2, NULL, 22, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuarantinePerson] OFF
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] ON 
GO
INSERT [dbo].[QuarantineRoom] ([id], [displayName], [capacity], [level]) VALUES (0, N'ab', 20, N'F2')
GO
SET IDENTITY_INSERT [dbo].[QuarantineRoom] OFF
GO
INSERT [dbo].[Severity] ([level], [description]) VALUES (N'F1', N'Say goodbye')
GO
INSERT [dbo].[Severity] ([level], [description]) VALUES (N'F2', N'Never forget')
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 
GO
INSERT [dbo].[Staff] ([id], [name], [dateOfBirth], [sex], [citizenID], [nationality], [healthInsuranceID], [phoneNumber], [addressID], [jobTitle], [department]) VALUES (0, N'Soobin HS', CAST(N'2001-02-02' AS Date), N'Nam', N'123123', N'VN', N'123123', N'0202020', 1, N'Manager', N'Manage')
GO
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_CitizenID]    Script Date: 09-Nov-21 4:26:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_CitizenID] ON [dbo].[QuarantinePerson]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Quarantine_HealthInsurID]    Script Date: 09-Nov-21 4:26:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Quarantine_HealthInsurID] ON [dbo].[QuarantinePerson]
(
	[healthInsuranceID] ASC
)
WHERE ([healthInsuranceID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_CitizenID]    Script Date: 09-Nov-21 4:26:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_CitizenID] ON [dbo].[Staff]
(
	[citizenID] ASC
)
WHERE ([citizenID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Staff_HealthInsurID]    Script Date: 09-Nov-21 4:26:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Staff_HealthInsurID] ON [dbo].[Staff]
(
	[healthInsuranceID] ASC
)
WHERE ([healthInsuranceID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD FOREIGN KEY([addressID])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[DestinationHistory]  WITH CHECK ADD FOREIGN KEY([quarantinePersonID])
REFERENCES [dbo].[QuarantinePerson] ([id])
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
