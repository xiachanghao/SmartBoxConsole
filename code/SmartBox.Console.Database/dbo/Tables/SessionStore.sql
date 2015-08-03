CREATE TABLE [dbo].[SessionStore] (
    [SessionID]    UNIQUEIDENTIFIER NOT NULL,
    [SessionValue] TEXT             NOT NULL,
    [CreateTime]   DATETIME         NOT NULL,
    CONSTRAINT [PK_SessionStore] PRIMARY KEY CLUSTERED ([SessionID] ASC)
);

