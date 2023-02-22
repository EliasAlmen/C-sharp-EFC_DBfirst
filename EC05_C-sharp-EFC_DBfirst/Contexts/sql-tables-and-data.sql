CREATE TABLE Addresses (
	Id int not null identity primary key,
	StreetName nvarchar(100) not null,
	PostalCode char(6) not null,
	City nvarchar(100) not null
)
GO

CREATE TABLE Customers (
	Id uniqueidentifier not null primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(100) not null unique,
	PhoneNumber char(13) null,

	AddressId int not null references Addresses(Id)
)
GO

INSERT INTO Addresses VALUES
	('Nordanvägen 5', '73373', 'Ransta'),
	('Nordanvägen 5', '73373', 'Ransta'),
	('Nordanvägen 5', '73373', 'Ransta')
GO

INSERT INTO Customers VALUES
	('b273e263-7543-4efb-8898-c44e123580dd','Elias','Almén','almen.elias@gmail.com','073-951 55 44', 1),
	('6c0bc120-5b04-4507-9f4b-7d3e73831667','Camilla','Westerlund','camillawesterlund@hotmail.com','070-730 57 29', 1),
	('0830bada-bda0-4c85-9beb-1506edea4ac6','Malou','Almén','N/A','N/A', 2)
GO