
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/27/2019 19:16:06
-- Generated from EDMX file: C:\Users\sheri\source\repos\LetsFly\LetsFly\Models\LetsFlyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LetsFlyDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BookingPassenger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Passengers] DROP CONSTRAINT [FK_BookingPassenger];
GO
IF OBJECT_ID(N'[dbo].[FK_RatingAirline]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_RatingAirline];
GO
IF OBJECT_ID(N'[dbo].[FK_AirlineFlight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK_AirlineFlight];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_UserBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_UserRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_UserRating];
GO
IF OBJECT_ID(N'[dbo].[FK_FlightBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_FlightBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_AirportFlight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Flights] DROP CONSTRAINT [FK_AirportFlight];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[Passengers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Passengers];
GO
IF OBJECT_ID(N'[dbo].[Airports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Airports];
GO
IF OBJECT_ID(N'[dbo].[Airlines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Airlines];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[Flights]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Flights];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [PhoneNo] nvarchar(max)  NULL,
    [UserImg] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DateOfBirth] datetime  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RolesId] int IDENTITY(1,1) NOT NULL,
    [RoleType] nvarchar(max)  NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [BookingNumber] int IDENTITY(1,1) NOT NULL,
    [Price] nvarchar(max)  NULL,
    [State] nvarchar(max)  NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [FlightId] int  NOT NULL,
    [BookingDate] datetime  NULL
);
GO

-- Creating table 'Passengers'
CREATE TABLE [dbo].[Passengers] (
    [PassengerId] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PassportNo] nvarchar(max)  NOT NULL,
    [BookingNumber] int  NOT NULL
);
GO

-- Creating table 'Airports'
CREATE TABLE [dbo].[Airports] (
    [AirportId] int IDENTITY(1,1) NOT NULL,
    [AirportName] nvarchar(max)  NOT NULL,
    [AirportCode] nvarchar(max)  NOT NULL,
    [AirportLocationName] nvarchar(max)  NOT NULL,
    [AirportLong] nvarchar(max)  NOT NULL,
    [AirportLat] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Airlines'
CREATE TABLE [dbo].[Airlines] (
    [AirlineId] int IDENTITY(1,1) NOT NULL,
    [AirlineName] nvarchar(max)  NOT NULL,
    [AirlineImg] nvarchar(max)  NOT NULL,
    [AirlineCode] nvarchar(max)  NOT NULL,
    [AirlineDescription] nvarchar(max)  NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [RatingId] int IDENTITY(1,1) NOT NULL,
    [RatingNumber] int  NOT NULL,
    [RatingImg] nvarchar(max)  NULL,
    [RatingDate] datetime  NOT NULL,
    [RatingDescription] nvarchar(max)  NULL,
    [AirlineId] int  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Flights'
CREATE TABLE [dbo].[Flights] (
    [FlightId] int IDENTITY(1,1) NOT NULL,
    [FlightNumber] nvarchar(max)  NOT NULL,
    [DepartureDate] datetime  NOT NULL,
    [ArrivalDate] datetime  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Capacity] int  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [AirlineId] int  NOT NULL,
    [ArrivalAirport] nvarchar(max)  NULL,
    [DepartureAirport] nvarchar(max)  NULL,
    [AirportId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RolesId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RolesId] ASC);
GO

-- Creating primary key on [BookingNumber] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([BookingNumber] ASC);
GO

-- Creating primary key on [PassengerId] in table 'Passengers'
ALTER TABLE [dbo].[Passengers]
ADD CONSTRAINT [PK_Passengers]
    PRIMARY KEY CLUSTERED ([PassengerId] ASC);
GO

-- Creating primary key on [AirportId] in table 'Airports'
ALTER TABLE [dbo].[Airports]
ADD CONSTRAINT [PK_Airports]
    PRIMARY KEY CLUSTERED ([AirportId] ASC);
GO

-- Creating primary key on [AirlineId] in table 'Airlines'
ALTER TABLE [dbo].[Airlines]
ADD CONSTRAINT [PK_Airlines]
    PRIMARY KEY CLUSTERED ([AirlineId] ASC);
GO

-- Creating primary key on [RatingId] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([RatingId] ASC);
GO

-- Creating primary key on [FlightId] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [PK_Flights]
    PRIMARY KEY CLUSTERED ([FlightId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BookingNumber] in table 'Passengers'
ALTER TABLE [dbo].[Passengers]
ADD CONSTRAINT [FK_BookingPassenger]
    FOREIGN KEY ([BookingNumber])
    REFERENCES [dbo].[Bookings]
        ([BookingNumber])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingPassenger'
CREATE INDEX [IX_FK_BookingPassenger]
ON [dbo].[Passengers]
    ([BookingNumber]);
GO

-- Creating foreign key on [AirlineId] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingAirline]
    FOREIGN KEY ([AirlineId])
    REFERENCES [dbo].[Airlines]
        ([AirlineId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingAirline'
CREATE INDEX [IX_FK_RatingAirline]
ON [dbo].[Ratings]
    ([AirlineId]);
GO

-- Creating foreign key on [AirlineId] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK_AirlineFlight]
    FOREIGN KEY ([AirlineId])
    REFERENCES [dbo].[Airlines]
        ([AirlineId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AirlineFlight'
CREATE INDEX [IX_FK_AirlineFlight]
ON [dbo].[Flights]
    ([AirlineId]);
GO

-- Creating foreign key on [UserId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_UserBooking]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBooking'
CREATE INDEX [IX_FK_UserBooking]
ON [dbo].[Bookings]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_UserRoles]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles'
CREATE INDEX [IX_FK_UserRoles]
ON [dbo].[Roles]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_UserRating]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRating'
CREATE INDEX [IX_FK_UserRating]
ON [dbo].[Ratings]
    ([UserId]);
GO

-- Creating foreign key on [FlightId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_FlightBooking]
    FOREIGN KEY ([FlightId])
    REFERENCES [dbo].[Flights]
        ([FlightId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FlightBooking'
CREATE INDEX [IX_FK_FlightBooking]
ON [dbo].[Bookings]
    ([FlightId]);
GO

-- Creating foreign key on [AirportId] in table 'Flights'
ALTER TABLE [dbo].[Flights]
ADD CONSTRAINT [FK_AirportFlight]
    FOREIGN KEY ([AirportId])
    REFERENCES [dbo].[Airports]
        ([AirportId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AirportFlight'
CREATE INDEX [IX_FK_AirportFlight]
ON [dbo].[Flights]
    ([AirportId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------