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

CREATE TABLE [Applicant] (
    [ApplicantId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Gender] bit NOT NULL,
    [Mobile] nvarchar(11) NOT NULL,
    [NationalCode] nvarchar(10) NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Applicant] PRIMARY KEY ([ApplicantId])
);
GO

CREATE TABLE [CommissionRule] (
    [CommissionRuleId] int NOT NULL IDENTITY,
    [CommissionBasedOn] smallint NOT NULL,
    [MinimumAmount] bigint NULL,
    [MaximumAmount] bigint NULL,
    [MinimumNumber] int NULL,
    [MaximumNumber] int NULL,
    [CommissionType] smallint NOT NULL,
    [Value] int NOT NULL,
    [Active] bit NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommissionRule] PRIMARY KEY ([CommissionRuleId])
);
GO

CREATE TABLE [Company] (
    [CompanyId] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(100) NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
);
GO

CREATE TABLE [JobAnnouncementCategory] (
    [JobAnnouncementCategoryId] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(max) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_JobAnnouncementCategory] PRIMARY KEY ([JobAnnouncementCategoryId])
);
GO

CREATE TABLE [ReferralCode] (
    [ReferralCodeId] int NOT NULL IDENTITY,
    [ReferralCodeName] nvarchar(50) NOT NULL,
    [RefCode] nvarchar(30) NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_ReferralCode] PRIMARY KEY ([ReferralCodeId])
);
GO

CREATE TABLE [Skill] (
    [SkillId] int NOT NULL IDENTITY,
    [SkillName] nvarchar(50) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY ([SkillId])
);
GO

CREATE TABLE [StudyField] (
    [StudyFieldId] int NOT NULL IDENTITY,
    [StudyFieldName] nvarchar(50) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_StudyField] PRIMARY KEY ([StudyFieldId])
);
GO

CREATE TABLE [SystemSMS] (
    [SystemSMSId] int NOT NULL IDENTITY,
    [SMSType] smallint NOT NULL,
    [Mobile] nvarchar(11) NOT NULL,
    [SendDate] datetime2 NOT NULL,
    CONSTRAINT [PK_SystemSMS] PRIMARY KEY ([SystemSMSId])
);
GO

CREATE TABLE [UserLog] (
    [LogId] bigint NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Title] nvarchar(50) NULL,
    [Description] nvarchar(150) NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_UserLog] PRIMARY KEY ([LogId])
);
GO

CREATE TABLE [Wallet] (
    [WalletId] int NOT NULL IDENTITY,
    [WalletName] nvarchar(50) NOT NULL,
    [ApplicationUserId] int NULL,
    [Active] bit NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Wallet] PRIMARY KEY ([WalletId]),
    CONSTRAINT [FK_Wallet_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [JobAnnouncement] (
    [JobAnnouncementId] int NOT NULL IDENTITY,
    [CompanyId] int NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(3000) NULL,
    [PublishDate] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NULL,
    [Capacity] int NULL,
    [ShowCompanyInfo] bit NOT NULL,
    [HasEmplyementExam] bit NOT NULL,
    [ExamDate] datetime2 NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_JobAnnouncement] PRIMARY KEY ([JobAnnouncementId]),
    CONSTRAINT [FK_JobAnnouncement_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Wallet_ReferralCode_CommissionRule] (
    [WalletId] int NOT NULL,
    [ReferralCodeId] int NOT NULL,
    [CommissionRuleId] int NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Wallet_ReferralCode_CommissionRule] PRIMARY KEY ([WalletId], [ReferralCodeId], [CommissionRuleId]),
    CONSTRAINT [FK_Wallet_ReferralCode_CommissionRule_CommissionRule_CommissionRuleId] FOREIGN KEY ([CommissionRuleId]) REFERENCES [CommissionRule] ([CommissionRuleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wallet_ReferralCode_CommissionRule_ReferralCode_ReferralCodeId] FOREIGN KEY ([ReferralCodeId]) REFERENCES [ReferralCode] ([ReferralCodeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wallet_ReferralCode_CommissionRule_Wallet_WalletId] FOREIGN KEY ([WalletId]) REFERENCES [Wallet] ([WalletId]) ON DELETE CASCADE
);
GO

CREATE TABLE [WalletCommission] (
    [WalletCommissionId] int NOT NULL IDENTITY,
    [WalletId] int NOT NULL,
    [Commission] int NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_WalletCommission] PRIMARY KEY ([WalletCommissionId]),
    CONSTRAINT [FK_WalletCommission_Wallet_WalletId] FOREIGN KEY ([WalletId]) REFERENCES [Wallet] ([WalletId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Applicant_JobAnnouncement] (
    [ApplicantId] int NOT NULL,
    [JobAnnouncementId] int NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    [Applicant_JobAnnouncementApplicantId] int NULL,
    [Applicant_JobAnnouncementJobAnnouncementId] int NULL,
    CONSTRAINT [PK_Applicant_JobAnnouncement] PRIMARY KEY ([ApplicantId], [JobAnnouncementId]),
    CONSTRAINT [FK_Applicant_JobAnnouncement_Applicant_ApplicantId] FOREIGN KEY ([ApplicantId]) REFERENCES [Applicant] ([ApplicantId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applicant_JobAnnouncement_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnounc~] FOREIGN KEY ([Applicant_JobAnnouncementApplicantId], [Applicant_JobAnnouncementJobAnnouncementId]) REFERENCES [Applicant_JobAnnouncement] ([ApplicantId], [JobAnnouncementId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Applicant_JobAnnouncement_JobAnnouncement_JobAnnouncementId] FOREIGN KEY ([JobAnnouncementId]) REFERENCES [JobAnnouncement] ([JobAnnouncementId]) ON DELETE CASCADE
);
GO

CREATE TABLE [JobAnnouncement_JobAnnouncementCategory] (
    [JobAnnouncementId] int NOT NULL,
    [JobAnnouncementCategoryId] int NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_JobAnnouncement_JobAnnouncementCategory] PRIMARY KEY ([JobAnnouncementId], [JobAnnouncementCategoryId]),
    CONSTRAINT [FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncement_JobAnnouncementId] FOREIGN KEY ([JobAnnouncementId]) REFERENCES [JobAnnouncement] ([JobAnnouncementId]) ON DELETE CASCADE,
    CONSTRAINT [FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategory_JobAnnouncementCategoryId] FOREIGN KEY ([JobAnnouncementCategoryId]) REFERENCES [JobAnnouncementCategory] ([JobAnnouncementCategoryId]) ON DELETE CASCADE
);
GO

CREATE TABLE [JobAnnouncement_Skill] (
    [JobAnnouncementId] int NOT NULL,
    [SkillId] int NOT NULL,
    [RegistrationAmount] int NOT NULL,
    [InsertDate] datetime2 NOT NULL,
    CONSTRAINT [PK_JobAnnouncement_Skill] PRIMARY KEY ([JobAnnouncementId], [SkillId]),
    CONSTRAINT [FK_JobAnnouncement_Skill_JobAnnouncement_JobAnnouncementId] FOREIGN KEY ([JobAnnouncementId]) REFERENCES [JobAnnouncement] ([JobAnnouncementId]) ON DELETE CASCADE,
    CONSTRAINT [FK_JobAnnouncement_Skill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([SkillId]) ON DELETE CASCADE
);
GO

CREATE TABLE [JobAnnouncement_StudyField] (
    [JobAnnouncementId] int NOT NULL,
    [StudyFieldId] int NOT NULL,
    CONSTRAINT [PK_JobAnnouncement_StudyField] PRIMARY KEY ([JobAnnouncementId], [StudyFieldId]),
    CONSTRAINT [FK_JobAnnouncement_StudyField_JobAnnouncement_JobAnnouncementId] FOREIGN KEY ([JobAnnouncementId]) REFERENCES [JobAnnouncement] ([JobAnnouncementId]) ON DELETE CASCADE,
    CONSTRAINT [FK_JobAnnouncement_StudyField_StudyField_StudyFieldId] FOREIGN KEY ([StudyFieldId]) REFERENCES [StudyField] ([StudyFieldId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnouncementId] ON [Applicant_JobAnnouncement] ([Applicant_JobAnnouncementApplicantId], [Applicant_JobAnnouncementJobAnnouncementId]);
GO

CREATE INDEX [IX_Applicant_JobAnnouncement_JobAnnouncementId] ON [Applicant_JobAnnouncement] ([JobAnnouncementId]);
GO

CREATE INDEX [IX_JobAnnouncement_CompanyId] ON [JobAnnouncement] ([CompanyId]);
GO

CREATE INDEX [IX_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategoryId] ON [JobAnnouncement_JobAnnouncementCategory] ([JobAnnouncementCategoryId]);
GO

CREATE INDEX [IX_JobAnnouncement_Skill_SkillId] ON [JobAnnouncement_Skill] ([SkillId]);
GO

CREATE INDEX [IX_JobAnnouncement_StudyField_StudyFieldId] ON [JobAnnouncement_StudyField] ([StudyFieldId]);
GO

CREATE INDEX [IX_Wallet_ApplicationUserId] ON [Wallet] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Wallet_ReferralCode_CommissionRule_CommissionRuleId] ON [Wallet_ReferralCode_CommissionRule] ([CommissionRuleId]);
GO

CREATE INDEX [IX_Wallet_ReferralCode_CommissionRule_ReferralCodeId] ON [Wallet_ReferralCode_CommissionRule] ([ReferralCodeId]);
GO

CREATE INDEX [IX_WalletCommission_WalletId] ON [WalletCommission] ([WalletId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241214110456_mg-02', N'5.0.17');
GO

COMMIT;
GO

