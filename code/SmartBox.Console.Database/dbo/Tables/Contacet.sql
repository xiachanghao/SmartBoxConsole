CREATE TABLE [dbo].[Contacet] (
    [Id]            INT           IDENTITY (100000, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [ExtType]       NVARCHAR (64) NULL,
    [ExtCode]       NVARCHAR (64) NULL,
    [ImageId]       INT           NULL,
    [DisplayName]   NVARCHAR (64) NULL,
    [FirstName]     NVARCHAR (64) NULL,
    [FistNameExt]   NVARCHAR (64) NULL,
    [MiddleName]    NVARCHAR (64) NULL,
    [MiddleNameExt] NVARCHAR (64) NULL,
    [LastName]      NVARCHAR (64) NULL,
    [LastNameExt]   NVARCHAR (64) NULL,
    [Prefix]        NVARCHAR (64) NULL,
    [Suffix]        NVARCHAR (64) NULL,
    [NickName]      NVARCHAR (64) NULL,
    [Birthday]      DATETIME      NULL,
    CONSTRAINT [PK_CONTACET] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CONTACET_FK_USERRE_USERRESO] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserResource] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

