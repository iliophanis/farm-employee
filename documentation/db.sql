CREATE DATABASE farmemployee;

USE farmemployee;

CREATE TABLE Cultivation (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate timestamp NOT NULL,
	updateDate timestamp NOT NULL,
	name varchar(255) NOT NULL	 
);

CREATE TABLE Package (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	name varchar(255) NOT NULL,
	price decimal NOT NULL,
	discount decimal NOT NULL,
	maxRequests int NOT NULL
);

CREATE TABLE Location (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	longtitude decimal NOT NULL,
	latitude decimal NOT NULL,
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

CREATE TABLE File (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	type varchar(255) NOT NULL,
	name varchar(255) NOT NULL,
	data longblob NOT NULL	
);

CREATE TABLE ContactInfo (
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
	roleId int NOT NULL,
	FOREIGN KEY (roleId) REFERENCES Roles(id)
);

CREATE TABLE Farmer (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	avgRate decimal NOT NULL DEFAULT 0,
	avgWorkPlaceRate decimal NOT NULL DEFAULT 0,
	avgPaymentConsequenceRate decimal NOT NULL DEFAULT 0,
  	paymentStatus ENUM('pendingPayment', 'processing', 'onHold', 'completed', 'canceled', 'refunded', 'failed') NOT NULL,
	paymentMethod ENUM('bankTransfer', 'paypal', 'ebanking') NOT NULL,
	userId int NOT NULL,
	contactInfo int NOT NULL,
	FOREIGN KEY (userId) REFERENCES User(id),
	FOREIGN KEY (contactInfo) REFERENCES ContactInfo(id)
);

CREATE TABLE Employee (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	avgRate decimal NOT NULL DEFAULT 0,
	avgJobQuality decimal NOT NULL DEFAULT 0,
	avgPrice decimal NOT NULL DEFAULT 0,
	avgContactQuality decimal NOT NULL DEFAULT 0,   	
	userId int NOT NULL,
	fileId int NULL,
	contactInfo int NOT NULL,
	FOREIGN KEY (userId) REFERENCES User(id),
	FOREIGN KEY (fileId) REFERENCES File(id),
	FOREIGN KEY (contactInfo) REFERENCES ContactInfo(id)
);

CREATE TABLE FarmerLocation (
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
	price decimal NULL,
	stayAmount decimal NULL,
	travelAmount decimal NULL,
	foodAmount decimal NULL,
	locationId int NOT NULL,
	farmerId int NOT NULL,
	cultivationId int NOT NULL,
	FOREIGN KEY (locationId) REFERENCES Location(id),
	FOREIGN KEY (cultivationId) REFERENCES Cultivation(id),
	FOREIGN KEY (farmerId) REFERENCES Farmer(id)
); 

CREATE TABLE EmployeeRequest (
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

CREATE TABLE FarmerRating (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	stars decimal NOT NULL, 
	workPlaceRate decimal NOT NULL,
	paymentConsequence decimal NOT NULL,
	CHECK (stars BETWEEN 1 AND 5),
	CHECK (workPlaceRate BETWEEN 1 AND 5),
	CHECK (paymentConsequence BETWEEN 1 AND 5),  	
	employeeRequestId int NOT NULL,
	FOREIGN KEY (employeeRequestId) REFERENCES EmployeeRequest(id)
);

CREATE TABLE EmployeeRating (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	insertDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	updateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
	description text NULL,
	stars decimal NOT NULL,  
	jobQualityRate decimal NOT NULL,
	ContactQualityRate decimal NOT NULL,
	CHECK (stars BETWEEN 1 AND 5),
	CHECK (jobQualityRate BETWEEN 1 AND 5), 
	CHECK (ContactQualityRate BETWEEN 1 AND 5), 
	employeeRequestId int NOT NULL,
	FOREIGN KEY (employeeRequestId) REFERENCES EmployeeRequest(id)
);