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

IF SCHEMA_ID(N'users') IS NULL EXEC(N'CREATE SCHEMA [users];');
GO

CREATE TABLE [users].[Permissions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [users].[UserRegistrations] (
    [UserRegistrationId] uniqueidentifier NOT NULL,
    [Login] nvarchar(70) NOT NULL,
    [Password] nvarchar(25) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(70) NOT NULL,
    [LastName] nvarchar(70) NOT NULL,
    [Name] nvarchar(155) NOT NULL,
    [Address] nvarchar(100) NOT NULL,
    [RegisteredDate] datetime2 NOT NULL,
    [StatusCode] nvarchar(max) NOT NULL,
    [ConfirmedDate] datetime2 NULL,
    CONSTRAINT [PK_UserRegistrations] PRIMARY KEY ([UserRegistrationId])
);
GO

CREATE TABLE [users].[Users] (
    [UserId] uniqueidentifier NOT NULL,
    [Login] nvarchar(70) NOT NULL,
    [Password] nvarchar(25) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [FirstName] nvarchar(70) NOT NULL,
    [LastName] nvarchar(70) NOT NULL,
    [Name] nvarchar(155) NOT NULL,
    [Address] nvarchar(100) NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    [UpdatedDateTime] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [users].[Roles] (
    [Id] int NOT NULL IDENTITY,
    [RoleCode] nvarchar(max) NOT NULL,
    [RolesId] int NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Roles_Roles_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [users].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Roles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [users].[Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [users].[RolePermission] (
    [RoleId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_RolePermission] PRIMARY KEY ([PermissionId], [RoleId]),
    CONSTRAINT [FK_RolePermission_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [users].[Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolePermission_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [users].[Roles] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[users].[Permissions]'))
    SET IDENTITY_INSERT [users].[Permissions] ON;
INSERT INTO [users].[Permissions] ([Id], [Name])
VALUES (1, N'AccessUsers'),
(2, N'ReadUser'),
(3, N'ChangeUser'),
(4, N'RemoveUser'),
(5, N'DeleteComment'),
(6, N'GetComments'),
(7, N'UpdateComment'),
(8, N'AddComment'),
(9, N'GetProducts'),
(10, N'ModifyProduct'),
(11, N'RemoveProduct'),
(12, N'GetSales'),
(13, N'GetItems'),
(14, N'GetOrders'),
(15, N'GetBasket'),
(16, N'GetBuys'),
(17, N'CancelBuy'),
(18, N'BuyItem'),
(20, N'AddItemInBasket'),
(21, N'DeleteBasketItem'),
(22, N'BuyBasket'),
(23, N'PublishProduct'),
(24, N'GetRatings'),
(25, N'AddRating'),
(26, N'UpdateRating'),
(27, N'DeleteRating'),
(28, N'SaleProduct'),
(29, N'UpdateProduct'),
(30, N'GetUserRegistration');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[users].[Permissions]'))
    SET IDENTITY_INSERT [users].[Permissions] OFF;
GO

CREATE INDEX [IX_RolePermission_RoleId] ON [users].[RolePermission] ([RoleId]);
GO

CREATE INDEX [IX_Roles_RolesId] ON [users].[Roles] ([RolesId]);
GO

CREATE INDEX [IX_Roles_UserId] ON [users].[Roles] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231218123338_Add_roles_and_permissions', N'7.0.0');
GO

COMMIT;
GO

