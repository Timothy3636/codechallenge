-- Drop table if exists
IF OBJECT_ID('EmailNotifications', 'U') IS NOT NULL
    DROP TABLE EmailNotifications;
IF OBJECT_ID('UserCashBalance', 'U') IS NOT NULL
    DROP TABLE UserCashBalance;
IF OBJECT_ID('FXRates', 'U') IS NOT NULL
    DROP TABLE FXRates;
IF OBJECT_ID('Contracts', 'U') IS NOT NULL
    DROP TABLE Contracts;

IF OBJECT_ID('Users', 'U') IS NOT NULL
    DROP TABLE Users;
IF OBJECT_ID('Admins', 'U') IS NOT NULL
    DROP TABLE Admins;

-- Create table: Users
CREATE TABLE Users (
    UserID INT IDENTITY(1, 1) PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255),
    Password NVARCHAR(255),
    IsActive BIT,
    RegistrationDate DATETIME
);

-- Create table: Admins
CREATE TABLE Admins (
    AdminID INT IDENTITY(1, 1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL,
    AddedDate DATETIME NOT NULL,
    Role NVARCHAR(255) NOT NULL,

);

-- Create table: UserCashBalance
CREATE TABLE UserCashBalance (
    BalanceID INT IDENTITY(1, 1) PRIMARY KEY,
    UserID INT,
    Currency NVARCHAR(3),
    CashBalance DECIMAL(18, 2),
	OnHold DECIMAL(18, 2),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- Create table: Contracts
CREATE TABLE Contracts (
    ContractID INT IDENTITY(1, 1) PRIMARY KEY,
    ContractDate DATETIME,
    Status NVARCHAR(255),
    FundFromAmount DECIMAL(18, 2),
    ConvertToAmount DECIMAL(18, 2),
    ExchangeRate DECIMAL(18, 2),
    UserID INT,
    ConvertToCurrency NVARCHAR(3),
    FundFromCurrency NVARCHAR(3),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
);

-- Create table: EmailNotifications
CREATE TABLE EmailNotifications (
    NotificationID INT IDENTITY(1, 1) PRIMARY KEY,
    ContractID INT,
    RecipientEmail NVARCHAR(255),
    EmailSubject NVARCHAR(255),
    EmailBody NVARCHAR(MAX),
    EmailSentDate DATETIME,
    IsEmailDelivered BIT,
    SenderEmail NVARCHAR(255),
    EmailDeliveredDate DATETIME,
    EmailStatus NVARCHAR(255),
    FOREIGN KEY (ContractID) REFERENCES Contracts(ContractID)
);

---- Data

delete from UserCashBalance where UserId = 1;

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'AUD', 10000000000, 0);

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'JPY', 10000000000, 0);

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'USD', 10000000000, 0);

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'HKD', 10000000000, 0);

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'EUR', 10000000000, 0);

INSERT INTO UserCashBalance (UserID, Currency, CashBalance, OnHold)
VALUES (1, 'GBP', 10000000000, 0);



