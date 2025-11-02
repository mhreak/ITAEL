BEGIN TRANSACTION;
GO

ALTER TABLE [ServiceStation] ADD [IsDestinationStation] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ServiceStation] ADD [IsOrigionStation] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ServiceReservation] ADD [DestinationRouteStationId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ServiceReservation] ADD [OriginRouteStationId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_ServiceReservation_DestinationRouteStationId] ON [ServiceReservation] ([DestinationRouteStationId]);
GO

CREATE INDEX [IX_ServiceReservation_OriginRouteStationId] ON [ServiceReservation] ([OriginRouteStationId]);
GO

ALTER TABLE [ServiceReservation] ADD CONSTRAINT [FK_ServiceReservation_RouteStation_DestinationRouteStationId] FOREIGN KEY ([DestinationRouteStationId]) REFERENCES [RouteStation] ([RouteStationId]);
GO

ALTER TABLE [ServiceReservation] ADD CONSTRAINT [FK_ServiceReservation_RouteStation_OriginRouteStationId] FOREIGN KEY ([OriginRouteStationId]) REFERENCES [RouteStation] ([RouteStationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220807043041_new-mg-11', N'5.0.17');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SMS] ADD [DestinationRouteStationId] int NULL;
GO

ALTER TABLE [SMS] ADD [OrigionRouteStationId] int NULL;
GO

ALTER TABLE [SMS] ADD [PassengerId] int NULL;
GO

ALTER TABLE [SMS] ADD [PaymentTypeId] int NULL;
GO

ALTER TABLE [SMS] ADD [ReserveState] smallint NULL;
GO

ALTER TABLE [SMS] ADD [ShuttleServiceId] nvarchar(max) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceStation]') AND [c].[name] = N'ForthPrice');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ServiceStation] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ServiceStation] ALTER COLUMN [ForthPrice] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220813162215_new-mg-12', N'5.0.17');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Passenger]') AND [c].[name] = N'ShiftWork1');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Passenger] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Passenger] DROP COLUMN [ShiftWork1];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Passenger]') AND [c].[name] = N'ShiftWork2');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Passenger] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Passenger] DROP COLUMN [ShiftWork2];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Passenger]') AND [c].[name] = N'ShiftWork3');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Passenger] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Passenger] DROP COLUMN [ShiftWork3];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Passenger]') AND [c].[name] = N'ShiftWork4');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Passenger] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Passenger] DROP COLUMN [ShiftWork4];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Passenger]') AND [c].[name] = N'PersonnelCode');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Passenger] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Passenger] ALTER COLUMN [PersonnelCode] nvarchar(30) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork1Finish] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork1Start] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork2Finish] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork2Start] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork3Finish] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork3Start] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork4Finish] nvarchar(5) NULL;
GO

ALTER TABLE [Passenger] ADD [ShiftWork4Start] nvarchar(5) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220820031055_new-mg-13', N'5.0.17');
GO

COMMIT;
GO

