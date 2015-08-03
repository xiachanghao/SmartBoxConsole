CREATE TABLE [dbo].[UserAuthLog] (
    [ID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [UID]       NVARCHAR (32) NOT NULL,
    [DeviceID]  NVARCHAR (64) NOT NULL,
    [AuthMode]  NVARCHAR (32) NOT NULL,
    [LoginTime] DATETIME      NOT NULL,
    [Result]    BIT           NOT NULL,
    [ErrorPwd]  NVARCHAR (64) NULL,
    CONSTRAINT [PK_UserAuthLog] PRIMARY KEY CLUSTERED ([ID] ASC)
);

