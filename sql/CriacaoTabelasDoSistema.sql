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

CREATE TABLE [HOTELS] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [CNPJ] char(14) NOT NULL,
    [Endereco] varchar(100) NOT NULL,
    [Descricao] varchar(500) NOT NULL,
    [Fotos] varchar(100) NOT NULL,
    CONSTRAINT [PK_HOTELS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [QUARTOS] (
    [Id] uniqueidentifier NOT NULL,
    [IdHotel] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [NumeroOcupantes] int NOT NULL,
    [NumeroDeAdultos] int NOT NULL,
    [NumeroDeCriancas] int NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [Fotos] varchar(100) NOT NULL,
    CONSTRAINT [PK_QUARTOS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_QUARTOS_HOTELS_IdHotel] FOREIGN KEY ([IdHotel]) REFERENCES [HOTELS] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_QUARTOS_IdHotel] ON [QUARTOS] ([IdHotel]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220130231242_InitialCreation', N'6.0.1');
GO

COMMIT;
GO

