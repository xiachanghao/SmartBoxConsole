﻿CREATE TABLE [dbo].[ContactCategory] (
    [Id]          INT           NOT NULL,
    [UserId]      INT           NOT NULL,
    [Code]        NVARCHAR (64) NULL,
    [DisplayName] NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_CONTACTCATEGORY] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CONTACTC_FK_USERRE_USERRESO] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserResource] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

