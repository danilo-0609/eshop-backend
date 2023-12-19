IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'catalog') IS NULL EXEC(N'CREATE SCHEMA [catalog];');
GO

CREATE TABLE [catalog].[Comments] (
    [CommentId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Content] nvarchar(3000) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    [UpdatedDateTime] datetime2 NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentId])
);
GO

CREATE TABLE [catalog].[Products] (
    [ProductId] uniqueidentifier NOT NULL,
    [SellerId] uniqueidentifier NOT NULL,
    [Name] nvarchar(300) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Size] nvarchar(100) NOT NULL,
    [Color] nvarchar(100) NOT NULL,
    [ProductTypeValue] nvarchar(max) NOT NULL,
    [InStock] int NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    [UpdatedDateTime] datetime2 NULL,
    [ExpiredDateTime] datetime2 NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);
GO

CREATE TABLE [catalog].[Ratings] (
    [RatingId] uniqueidentifier NOT NULL,
    [Feedback] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Rate] float NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    [UpdatedDateTime] datetime2 NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY ([RatingId])
);
GO

CREATE TABLE [catalog].[Sales] (
    [SaleId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [AmountOfProducts] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Sales] PRIMARY KEY ([SaleId])
);
GO

CREATE TABLE [catalog].[Tags] (
    [ProductId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([ProductId], [Id]),
    CONSTRAINT [FK_Tags_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [catalog].[Products] ([ProductId]) ON DELETE CASCADE
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231219114829_Add_initial_catalog', N'7.0.0');
GO

COMMIT;
GO

