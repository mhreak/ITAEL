BEGIN TRANSACTION;
GO

ALTER TABLE [Student] ADD [LastName] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220617060651_new-mg-10', N'5.0.17');
GO

COMMIT;
GO

