BEGIN TRANSACTION;
GO

DROP TABLE [UserLog];
GO

EXEC sp_rename N'[JobAnnouncement].[HasEmplyementExam]', N'IsDeleted', N'COLUMN';
GO

ALTER TABLE [ReferralCode] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [JobAnnouncement] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [JobAnnouncement] ADD [Gender] bit NULL;
GO

ALTER TABLE [JobAnnouncement] ADD [HasEmployementExam] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Company] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Applicant]') AND [c].[name] = N'NationalCode');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Applicant] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Applicant] ALTER COLUMN [NationalCode] nvarchar(10) NOT NULL;
ALTER TABLE [Applicant] ADD DEFAULT N'' FOR [NationalCode];
GO

ALTER TABLE [Applicant] ADD [BirthDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241222141545_mg-04', N'5.0.17');
GO

COMMIT;
GO

