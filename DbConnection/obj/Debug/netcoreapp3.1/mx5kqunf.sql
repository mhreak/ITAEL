BEGIN TRANSACTION;
GO

ALTER TABLE [Wallet] DROP CONSTRAINT [FK_Wallet_User_ApplicationUserId];
GO

DROP INDEX [IX_Wallet_ApplicationUserId] ON [Wallet];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Wallet]') AND [c].[name] = N'ApplicationUserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Wallet] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Wallet] DROP COLUMN [ApplicationUserId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'CompanyName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [User] DROP COLUMN [CompanyName];
GO

EXEC sp_rename N'[JobAnnouncement].[HasEmployementExam]', N'HasEmploymentExam', N'COLUMN';
GO

ALTER TABLE [Applicant] ADD [CityId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Applicant] ADD [StudyFieldId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Province] (
    [ProvinceId] int NOT NULL IDENTITY,
    [ProvinceName] nvarchar(50) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY ([ProvinceId])
);
GO

CREATE TABLE [City] (
    [CityId] int NOT NULL IDENTITY,
    [CityName] nvarchar(50) NOT NULL,
    [ProvinceId] int NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY ([CityId]),
    CONSTRAINT [FK_City_Province_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Province] ([ProvinceId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Applicant_CityId] ON [Applicant] ([CityId]);
GO

CREATE INDEX [IX_Applicant_StudyFieldId] ON [Applicant] ([StudyFieldId]);
GO

CREATE INDEX [IX_City_ProvinceId] ON [City] ([ProvinceId]);
GO

ALTER TABLE [Applicant] ADD CONSTRAINT [FK_Applicant_City_CityId] FOREIGN KEY ([CityId]) REFERENCES [City] ([CityId]) ON DELETE CASCADE;
GO

ALTER TABLE [Applicant] ADD CONSTRAINT [FK_Applicant_StudyField_StudyFieldId] FOREIGN KEY ([StudyFieldId]) REFERENCES [StudyField] ([StudyFieldId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241225132342_mg-05', N'5.0.17');
GO

COMMIT;
GO

