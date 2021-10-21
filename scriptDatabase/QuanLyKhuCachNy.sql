use master
go

create database QLKCL
go

use QLKCL
go

create table HealthInformation
(
	id int identity(0,1),
	isFever bit not null, 
	isCough bit not null, 
	isSoreThroat bit not null, 
	isLossOfTatse bit not null, 
	isTired bit not null, 
	isShortnessOfBreath bit not null, 
	isOtherSymptoms bit not null, 
	isDisease bit not null, 

	/*Khóa chính*/
	primary key (id),
)



create table Severity
(
	level varchar(10),
	description nvarchar(50),

	/*Khóa chính*/
	primary key (level),

)


create table QuarantineRoom
(
	id int identity(0,1),
	displayName nvarchar(50) not null,
	capacity int not null,
	level varchar(10),

	/*Khóa chính*/
	primary key (id),
	/*Khóa ngoại*/
	foreign key (level) references Severity(level),
)


create table Address
(
	id int identity(0,1),
	province nvarchar(20) not null,
	district nvarchar(20) not null,
	ward nvarchar(20) not null,
	streetName nvarchar(20),
	apartmentNumber nvarchar(20),

	/*Khóa chính*/
	primary key (id),

)

create table Account
(
	id int identity(0,1),
	username varchar(20),
	password nvarchar(100),

	/*Khóa chính*/
	primary key (id),

)


create table Staff 
(
	id int identity(0,1),
	name nvarchar(50) not null,
	dateOfBirth date not null,
	sex nvarchar(10) not null,
	citizenID varchar(20) unique,
	nationality nvarchar(20) not null,
	healthInsuranceID varchar(20) unique,
	phoneNumber varchar(20),
	addressID int not null,
	jobTitle nvarchar(50) not null,
	department nvarchar(50) not null,

	/* khóa chính */
	primary key (id),

	/*ràng buộc*/
	constraint sexPersonStaff check (sex = N'Nam' or sex = N'Nữ'),


	/*khóa ngoại*/
	foreign key (addressID) references Address(id),

)

create table QuarantineArea
(
	id int identity(0,1),
	name nvarchar(50) not null,
	testCycle int not null,
	requiredDayToFinish int not null,
	addressID int,
	managerID int,

	/*Khóa chính*/
	primary key (id),

	/*Khóa ngoại*/
	foreign key (addressID) references Address(id),
	foreign key (managerID) references Staff(id),

)

create table QuarantinePerson 
(
	id int identity(0,1),
	name nvarchar(50) not null,
	dateOfBirth date not null,
	sex nvarchar(10) not null,
	citizenID varchar(20) unique,
	nationality nvarchar(20) not null,
	healthInsuranceID varchar(20) unique,
	phoneNumber varchar(20),
	level varchar(10) not null,
	arrivedDate date not null,
	leaveDate date not null,
	quarantineDays int not null,
	addressID int not null,
	healthInformationID int not null, 
	roomID int not null,

	/* khóa chính */
	primary key (id),

	/*ràng buộc*/
	constraint sexPersonQP check (sex = N'Nam' or sex = N'Nữ'),
	constraint arriveLeaveDate check (leaveDate >= arrivedDate),

	/*khóa ngoại*/
	foreign key (addressID) references Address(id),
	foreign key (healthInformationID) references HealthInformation(id),
	foreign key (roomID) references QuarantineRoom(id),
	foreign key (level) references Severity(level),

)

create table  InjectionRecord 
(
	id int identity,
	dateInjection date not null,
	vaccineName nvarchar(50) not null,
	quarantinePersonID int,

	/*Khóa chính*/
	primary key (id),

	/*Khóa ngoại*/
	foreign key (quarantinePersonID) references QuarantinePerson(id),


)

create table TestingResult
(
	id int identity,
	dateTesting date not null,
	isPositive bit not null,
	quarantinePersonID int,

	/*Khóa chính*/
	primary key (id),
	/*Khóa ngoại*/
	foreign key (quarantinePersonID) references QuarantinePerson(id),

)

create table DestinationHistory
(
	id int identity,
	dateArrive date not null,
	quarantinePersonID int,

	/*Khóa chính*/
	primary key (id),
	/*Khóa ngoại*/
	foreign key (quarantinePersonID) references QuarantinePerson(id),

)


insert into Account(username, password) 
values
( N'tunglete', N'a052015c462d64835f6dd40eecffabf4'),
( N'lamthon', N'45cdd9c9b0dcaeef9fbb154f45746a2f'),
( N'thonle', N'5647015c4a0be08b19fcf6ac21ad2d94'),
( N'phonluc', N'a6491f9211753cd5c857da1115492ba6')


