CREATE TABLE [dbo].[AppHttpPost] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [AppCode]     VARCHAR (50) NOT NULL,
    [AppPostHost] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_APPHTTPPOST] PRIMARY KEY CLUSTERED ([Id] ASC)
);

