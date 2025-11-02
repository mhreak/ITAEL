BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627223908_new-mg-16', N'5.0.17');
GO

COMMIT;
GO

