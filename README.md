# ProjetNetApple

# SQL COMMAND :

-- Create the APPLEDB database
CREATE DATABASE APPLEDB;

-- Use the APPLEDB database
USE APPLEDB;

-- Create the CATEGORY table
CREATE TABLE CATEGORY (
    ID INT PRIMARY KEY,
    CatName NVARCHAR(255) NOT NULL
);

-- Create the PRODUCT table with a foreign key reference to CATEGORY
CREATE TABLE PRODUCT (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Description NVARCHAR(MAX),
    Image NVARCHAR(MAX),
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES CATEGORY(ID)
);

-- Create the USERSS table
CREATE TABLE USERSS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Role NVARCHAR(255) NOT NULL,
    Fname NVARCHAR(255) NOT NULL,
    Lname NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

-- Create the CART table with foreign key references to USERSS and PRODUCT
CREATE TABLE CART (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (UserID) REFERENCES USERSS(ID),
    FOREIGN KEY (ProductID) REFERENCES PRODUCT(ID)
);

-- Create the CARTLINES table with foreign key references to CART and PRODUCT
CREATE TABLE CARTLINES (
    ID INT PRIMARY KEY IDENTITY(1,1),
    CartID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (CartID) REFERENCES CART(ID),
    FOREIGN KEY (ProductID) REFERENCES PRODUCT(ID)
);
