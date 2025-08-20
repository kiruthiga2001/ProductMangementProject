create database ProductManagementDB;
use ProductManagementDB;
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL
);
CREATE TABLE Attributes (
    AttributeId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
CREATE TABLE AttributeValues (
    AttributeValueId INT IDENTITY(1,1) PRIMARY KEY,
    Value NVARCHAR(100) NOT NULL,
    AttributeId INT NOT NULL,
    FOREIGN KEY (AttributeId) REFERENCES Attributes(AttributeId)
);
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE ProductAttributeValues (
    ProductAttributeValueId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    AttributeValueId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (AttributeValueId) REFERENCES AttributeValues(AttributeValueId)
);
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL
);

CREATE TABLE Complaints (
    ComplaintId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    ComplaintText NVARCHAR(500) NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, In Progress, Resolved
    CreatedDate DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Complaints_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Complaints_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);



