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
CREATE SEQUENCE [CustomerAddressSequence] START WITH 1 INCREMENT BY 1 NO CYCLE;

CREATE SEQUENCE [ReviewSequence] START WITH 1 INCREMENT BY 1 NO CYCLE;

CREATE SEQUENCE [RoleClaimSequence] START WITH 1 INCREMENT BY 1 NO CYCLE;

CREATE SEQUENCE [UserClaimSequence] START WITH 1 INCREMENT BY 1 NO CYCLE;

CREATE TABLE [Customers] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] VARCHAR(32) NOT NULL,
    [LastName] VARCHAR(32) NOT NULL,
    [Email] VARCHAR(254) NOT NULL,
    [PhoneNumber] VARCHAR(16) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] VARCHAR(64) NOT NULL,
    [Description] VARCHAR(256) NOT NULL,
    [MadeByCompany] VARCHAR(32) NOT NULL,
    [AverageRating] real NOT NULL,
    [Quantity] int NOT NULL,
    [LastRestockedAt] datetimeoffset NOT NULL,
    [Price] DECIMAL(9,2) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] CHAR(36) NOT NULL,
    [LastModifiedAt] datetimeoffset NOT NULL,
    [LastModifiedBy] CHAR(36) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Product_Rating] CHECK (AverageRating between 1 and 5)
);

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(32) NULL,
    [NormalizedName] varchar(32) NULL,
    [ConcurrencyStamp] char(32) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

CREATE TABLE [UserPasskeys] (
    [UserId] CHAR(36) NOT NULL,
    [CredentialId] varbinary(900) NOT NULL,
    [Data_PublicKey] varbinary(max) NOT NULL,
    [Data_Name] nvarchar(max) NULL,
    [Data_CreatedAt] datetimeoffset NOT NULL,
    [Data_SignCount] bigint NOT NULL,
    [Data_Transports] nvarchar(max) NULL,
    [Data_IsUserVerified] bit NOT NULL,
    [Data_IsBackupEligible] bit NOT NULL,
    [Data_IsBackedUp] bit NOT NULL,
    [Data_AttestationObject] varbinary(max) NOT NULL,
    [Data_ClientDataJson] varbinary(max) NOT NULL,
    CONSTRAINT [PK_UserPasskeys] PRIMARY KEY ([UserId], [CredentialId])
);

CREATE TABLE [CustomerAddresses] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [CustomerAddressSequence]),
    [Country] VARCHAR(32) NOT NULL,
    [City] VARCHAR(32) NOT NULL,
    [Street] VARCHAR(64) NOT NULL,
    [Zipcode] VARCHAR(8) NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CustomerAddresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerAddresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Orders] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [PlacedAt] datetimeoffset NOT NULL,
    [TotalAmount] DECIMAL(9,2) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NULL,
    [UserName] VARCHAR(128) NOT NULL,
    [NormalizedUserName] VARCHAR(128) NOT NULL,
    [Email] VARCHAR(256) NOT NULL,
    [NormalizedEmail] VARCHAR(256) NOT NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] VARCHAR(128) NOT NULL,
    [SecurityStamp] CHAR(36) NOT NULL,
    [ConcurrencyStamp] CHAR(36) NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id])
);

CREATE TABLE [CartItems] (
    [CustomerId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Units] smallint NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([CustomerId], [ProductId]),
    CONSTRAINT [FK_CartItems_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ProductImages] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [SortOrder] tinyint NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_ProductImage_SortOrder] CHECK (SortOrder between 1 and 32),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Reviews] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [ReviewSequence]),
    [ProductId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [Rating] int NOT NULL,
    [Comment] NVARCHAR(128) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Review_Rating] CHECK (Rating between 1 and 5),
    CONSTRAINT [FK_Reviews_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [RoleClaimSequence]),
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] VARCHAR(32) NOT NULL,
    [ClaimValue] VARCHAR(128) NOT NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OrderItems] (
    [OrderId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] DECIMAL(9,2) NOT NULL,
    [TotalPrice] DECIMAL(9,2) NOT NULL,
    [Units] smallint NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Payments] (
    [OrderId] uniqueidentifier NOT NULL,
    [TransactionId] nvarchar(450) NOT NULL,
    [GatewayName] nvarchar(max) NOT NULL,
    [PaidAmount] DECIMAL(9,2) NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Shipments] (
    [OrderId] uniqueidentifier NOT NULL,
    [AddressId] int NOT NULL,
    [EstimatedDelivery] datetimeoffset NOT NULL,
    [ActualDelivery] datetimeoffset NULL,
    [TrackingNumber] VARCHAR(36) NOT NULL,
    [CarrierName] VARCHAR(32) NOT NULL,
    [Notes] VARCHAR(64) NULL,
    CONSTRAINT [PK_Shipments] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Shipments_CustomerAddresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [CustomerAddresses] ([Id]),
    CONSTRAINT [FK_Shipments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
);

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [UserClaimSequence]),
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] NVARCHAR(64) NOT NULL,
    [ClaimValue] NVARCHAR(64) NOT NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserLoginProviders] (
    [LoginProvider] VARCHAR(128) NOT NULL,
    [ProviderKey] VARCHAR(128) NOT NULL,
    [ProviderDisplayName] VARCHAR(32) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserLoginProviders] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLoginProviders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] VARCHAR(64) NOT NULL,
    [Value] VARCHAR(64) NOT NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_CartItems_ProductId] ON [CartItems] ([ProductId]);

CREATE UNIQUE INDEX [IX_CustomerAddresses_CustomerId] ON [CustomerAddresses] ([CustomerId]);

CREATE INDEX [IX_Customer_Email] ON [Customers] ([Email]);

CREATE INDEX [IX_Customer_FullName] ON [Customers] ([FirstName], [LastName]);

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);

CREATE INDEX [IX_Payment_TransactionId] ON [Payments] ([TransactionId]);

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);

CREATE INDEX [IX_Product_Description] ON [Products] ([Description]);

CREATE INDEX [IX_Product_MadeByCompany] ON [Products] ([MadeByCompany]);

CREATE INDEX [IX_Product_Name] ON [Products] ([Name]);

CREATE INDEX [IX_Reviews_CustomerId] ON [Reviews] ([CustomerId]);

CREATE INDEX [IX_Reviews_ProductId] ON [Reviews] ([ProductId]);

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);

CREATE INDEX [IX_Shipment_TrackingNumber] ON [Shipments] ([TrackingNumber]);

CREATE UNIQUE INDEX [IX_Shipments_AddressId] ON [Shipments] ([AddressId]);

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);

CREATE INDEX [IX_ProviderDisplayName] ON [UserLoginProviders] ([ProviderDisplayName]);

CREATE UNIQUE INDEX [IX_UserLoginProviders_UserId] ON [UserLoginProviders] ([UserId]);

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);

CREATE INDEX [IX_User_NormalizedEmail] ON [Users] ([NormalizedEmail]);

CREATE INDEX [IX_User_NormalizedUserName] ON [Users] ([NormalizedUserName]);

CREATE UNIQUE INDEX [IX_Users_CustomerId] ON [Users] ([CustomerId]) WHERE [CustomerId] IS NOT NULL;

CREATE INDEX [IX_UserTokens_LoginProvider] ON [UserTokens] ([LoginProvider]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251210101236_Initial', N'10.0.0');

COMMIT;
GO

