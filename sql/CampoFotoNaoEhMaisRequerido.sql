BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QUARTOS]') AND [c].[name] = N'Fotos');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [QUARTOS] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [QUARTOS] ALTER COLUMN [Fotos] varchar(100) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HOTELS]') AND [c].[name] = N'Fotos');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [HOTELS] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [HOTELS] ALTER COLUMN [Fotos] varchar(100) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220131184947_CampoFotoNaoEhMaisRequerido', N'6.0.1');
GO

COMMIT;
GO

