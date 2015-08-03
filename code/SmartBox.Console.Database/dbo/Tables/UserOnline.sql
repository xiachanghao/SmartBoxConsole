CREATE TABLE [dbo].[UserOnline] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [UID]        NVARCHAR (32) NOT NULL,
    [DeviceID]   NVARCHAR (64) NOT NULL,
    [ClientType] NVARCHAR (32) NOT NULL,
    [Status]     INT           NOT NULL,
    [LoginTime]  DATETIME      NULL,
    [LogoutTime] DATETIME      NULL,
    CONSTRAINT [PK_UserOnline] PRIMARY KEY CLUSTERED ([ID] ASC)
);

