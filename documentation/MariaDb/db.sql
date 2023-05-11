CREATE DATABASE farm_employee;

USE farm_employee;

CREATE TABLE Cultivation (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	name varchar(255) NOT NULL	 
);

CREATE TABLE Package (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	name varchar(255) NOT NULL,
	price decimal(10,2) NOT NULL,
	discount decimal(10,2) NOT NULL,
	maxRequests int NOT NULL
);

CREATE TABLE Location (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	longtitude decimal(8,6) NOT NULL,
	latitude decimal(8,6) NOT NULL,
	prefecture varchar(255) NOT NULL,
	country varchar(255) NOT NULL,
	region varchar(255) NOT NULL,
	city varchar(255) NOT NULL,
	postCode varchar(255) NOT NULL,
	street varchar(255) NOT NULL
);

CREATE TABLE Roles (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	name varchar(255) NOT NULL,
	description text NULL
);

CREATE TABLE Document (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	type varchar(255) NOT NULL,
	name varchar(255) NOT NULL,
	data longblob NOT NULL	
);

CREATE TABLE Contact_Info (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	address varchar(255) NOT NULL,
	city varchar(255) NOT NULL,
	tk varchar(255) NOT NULL,
	phoneNo varchar(255) NULL,
	mobilePhoneNo varchar(255) NOT NULL
);

CREATE TABLE User (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	firstName varchar(255) NOT NULL,
	lastName varchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	password varchar(255) NOT NULL,
	emailConformed boolean NOT NULL DEFAULT FALSE,
	isActive boolean NOT NULL DEFAULT TRUE,
	lastLoginDate datetime NULL,
	authProvider ENUM('Google', 'Facebook') NULL, 
	roleId int NULL,
	FOREIGN KEY (roleId) REFERENCES Roles(id)
);

CREATE TABLE Farmer (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	avgRate decimal(5,2) NOT NULL DEFAULT 0,
	avgWorkPlaceRate decimal(5,2) NOT NULL DEFAULT 0,
	avgPaymentConsequenceRate decimal(5,2) NOT NULL DEFAULT 0,
  	paymentStatus ENUM('pendingPayment', 'processing', 'onHold', 'completed', 'canceled', 'refunded', 'failed') NULL,
	paymentMethod ENUM('bankTransfer', 'paypal', 'ebanking') NULL,
	userId int NOT NULL,
	contactInfoId int NULL,
	FOREIGN KEY (userId) REFERENCES User(id),
	FOREIGN KEY (contactInfoId) REFERENCES Contact_Info(id)
);

CREATE TABLE Employee (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	avgRate decimal(5,2) NOT NULL DEFAULT 0,
	avgJobQuality decimal(5,2) NOT NULL DEFAULT 0,
	avgPrice decimal(5,2) NOT NULL DEFAULT 0,
	avgContactQuality decimal(5,2) NOT NULL DEFAULT 0,   	
	userId int NOT NULL,
	documentId int NULL,
	contactInfoId int NULL,
	FOREIGN KEY (userId) REFERENCES User(id),
	FOREIGN KEY (documentId) REFERENCES Document(id),
	FOREIGN KEY (contactInfoId) REFERENCES Contact_Info(id)
);

CREATE TABLE Farmer_Location (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	name varchar (255) NOT NULL,
	data longblob NOT NULL,
	farmerId int NOT NULL,
	locationId int NOT NULL,
	FOREIGN KEY (locationId) REFERENCES Location(id),
	FOREIGN KEY (farmerId) REFERENCES Farmer(id)
);

CREATE TABLE Request (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	jobType varchar(255),
	startJobDate DATE,
	estimatedDuration int NULL,           
	price decimal(10,2) NULL,
	stayAmount decimal(10,2) NULL,
	travelAmount decimal(10,2) NULL,
	foodAmount decimal(10,2) NULL,
	locationId int NOT NULL,
	farmerId int NOT NULL,
	cultivationId int NOT NULL,
	FOREIGN KEY (locationId) REFERENCES Location(id),
	FOREIGN KEY (cultivationId) REFERENCES Cultivation(id),
	FOREIGN KEY (farmerId) REFERENCES Farmer(id)
); 

CREATE TABLE Employee_Request (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	messageSent BOOLEAN DEFAULT FALSE,
	paymentStatus ENUM('pendingPayment', 'processing', 'onHold', 'completed', 'canceled', 'refunded', 'failed') NOT NULL,
	paymentMethod ENUM('bankTransfer', 'paypal', 'ebanking') NOT NULL,
	employeeId int NOT NULL,
	requestId int NOT NULL,
	packageId int NOT NULL,
	FOREIGN KEY (employeeId) REFERENCES Employee(id),
	FOREIGN KEY (requestId) REFERENCES Request(id),
	FOREIGN KEY (packageId) REFERENCES Package(id)
);

CREATE TABLE SubEmployee (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	firstName varchar(255) NOT NULL,
	lastName varchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	employeeRequestId int NOT NULL,
	contactInfoId int NOT NULL,
	FOREIGN KEY (employeeRequestId) REFERENCES Employee_Request(id),
	FOREIGN KEY (contactInfoId) REFERENCES Contact_Info(id)
);

CREATE TABLE Farmer_Rating (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	stars decimal(5,2) NOT NULL, 
	workPlaceRate decimal(5,2) NOT NULL,
	paymentConsequence decimal(5,2) NOT NULL,
	CHECK (stars BETWEEN 1 AND 5),
	CHECK (workPlaceRate BETWEEN 1 AND 5),
	CHECK (paymentConsequence BETWEEN 1 AND 5),  	
	employeeRequestId int NOT NULL,
	FOREIGN KEY (employeeRequestId) REFERENCES Employee_Request(id)
);

CREATE TABLE Employee_Rating (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	stars decimal(5,2) NOT NULL,  
	jobQualityRate decimal(5,2) NOT NULL,
	ContactQualityRate decimal(5,2) NOT NULL,
	CHECK (stars BETWEEN 1 AND 5),
	CHECK (jobQualityRate BETWEEN 1 AND 5), 
	CHECK (ContactQualityRate BETWEEN 1 AND 5), 
	employeeRequestId int NOT NULL,
	FOREIGN KEY (employeeRequestId) REFERENCES Employee_Request(id)
);