CREATE TABLE [dbo].[Notification] (
    [ID]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [AppCode]                VARCHAR (64)   NOT NULL,
    [DeviceToken]            VARCHAR (64)   NOT NULL,
    [Payload]                NVARCHAR (MAX) NOT NULL,
    [NotificationIdentifier] VARCHAR (64)   NOT NULL,
    [ExpirationData]         DATETIME       NOT NULL,
    [Priority]               INT            NOT NULL,
    [RetryCount]             INT            NOT NULL,
    [IsSuccess]              BIT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_NOTIFICATION] PRIMARY KEY CLUSTERED ([ID] ASC)
);

