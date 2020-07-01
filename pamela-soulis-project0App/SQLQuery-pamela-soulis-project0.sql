
-- make a table for Customer
CREATE TABLE Customer (
	CustomerId INT IDENTITY (1, 1) NOT NULL,
	FirstName NVARCHAR(250) NOT NULL,
	LastName NVARCHAR(250) NOT NULL
);

--add constraints to Customer table
ALTER TABLE Customer
	ADD CONSTRAINT PK_Customer PRIMARY KEY (CustomerId);




--make a table for the products
CREATE TABLE Product (
	ProductId INT IDENTITY (1, 1) NOT NULL,
	Name NVARCHAR(250) NOT NULL,
	Price DECIMAL(5,2) NOT NULL
);

--add constraints to Product table
ALTER TABLE Product
	ADD CONSTRAINT PK_Product PRIMARY KEY (ProductId);




--make a table for the store location
CREATE TABLE Location (
	LocationId INT IDENTITY (1, 1) NOT NULL,
	Name NVARCHAR(250) NOT NULL
);

--add constraints to Location table
ALTER TABLE Location
	ADD CONSTRAINT PK_Location PRIMARY KEY (LocationId);




-- make an Orders table
CREATE TABLE Orders (
	OrderId INT IDENTITY (1, 1) NOT NULL,
	LocationId INT NOT NULL,
	CustomerId INT NOT NULL,
	Date DATE NOT NULL,
	Time TIME(7) NOT NULL
);

-- add constraints to Orders table
ALTER TABLE Orders
	ADD CONSTRAINT PK_Orders PRIMARY KEY (OrderId);					
ALTER TABLE Orders
	ADD CONSTRAINT FK_Orders_Customer_CustomerId FOREIGN KEY (CustomerId)
		REFERENCES Customer (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE Orders
	ADD CONSTRAINT FK_Orders_Location_LocationId FOREIGN KEY (LocationId)
		REFERENCES Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE;



-- make an OrderLine table
CREATE TABLE OrderLine (
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	Quantity INT CHECK (Quantity >= 0 AND Quantity <= 10) NOT NULL
);

-- add constraints to OrderLine table
ALTER TABLE OrderLine
	ADD CONSTRAINT PK_OrderLine PRIMARY KEY (OrderId, ProductId);					
ALTER TABLE OrderLine
	ADD CONSTRAINT FK_OrderLine_Orders_OrderId FOREIGN KEY (OrderId)
		REFERENCES Orders (OrderId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE OrderLine
	ADD CONSTRAINT FK_OrderLine_Product_ProductId FOREIGN KEY (ProductId)
		REFERENCES Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE;


 
-- make a table for the store's inventory
CREATE TABLE Inventory (
	LocationId INT NOT NULL,
	ProductId INT NOT NULL,
	Quantity INT CHECK (Quantity > 0) NOT NULL
);

-- add constraints to Inventory table
ALTER TABLE Inventory
	ADD CONSTRAINT PK_Inventory PRIMARY KEY (ProductId, LocationId);					
ALTER TABLE Inventory
	ADD CONSTRAINT FK_Inventory_Product_ProductId FOREIGN KEY (ProductId)
		REFERENCES Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE Inventory
	ADD CONSTRAINT FK_Inventory_Location_LocationId FOREIGN KEY (LocationId)
		REFERENCES Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE;


SELECT * FROM Customer;
SELECT * FROM Product;
SELECT * FROM Location;
SELECT * FROM Orders;
SELECT * FROM OrderLine;
SELECT * FROM Inventory;


