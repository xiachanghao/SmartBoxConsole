CREATE TABLE [dbo].[Authentication] (
    [UID]            NVARCHAR (64)  NOT NULL,
    [AuthMode]       NVARCHAR (64)  NOT NULL,
    [ExtID]          NVARCHAR (64)  NULL,
    [Password]       NVARCHAR (256) NOT NULL,
    [LastUpdateTime] DATETIME       NOT NULL,
    [LastUseTime]    DATETIME       NULL,
    CONSTRAINT [PK_Authentication_1] PRIMARY KEY CLUSTERED ([UID] ASC)
);

