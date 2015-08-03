CREATE TABLE [dbo].[Pattern] (
    [patternId]  UNIQUEIDENTIFIER NOT NULL,
    [deviceId]   VARCHAR (64)     NULL,
    [UserUid]    VARCHAR (64)     NOT NULL,
    [HasPattern] VARCHAR (5)      NOT NULL,
    [Pattern]    VARCHAR (15)     NULL,
    CONSTRAINT [PK_LoginPattern] PRIMARY KEY CLUSTERED ([patternId] ASC)
);

