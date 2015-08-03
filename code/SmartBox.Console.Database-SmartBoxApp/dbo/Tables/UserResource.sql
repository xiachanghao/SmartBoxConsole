﻿CREATE TABLE [dbo].[UserResource] (
    [Id]      INT           IDENTITY (100000, 1) NOT NULL,
    [ExtType] NVARCHAR (64) NOT NULL,
    [ExtCode] NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_USERRESOURCE] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EXT]
    ON [dbo].[UserResource]([ExtType] ASC, [ExtCode] ASC);

