CREATE TABLE [dbo].[NotificationReport] (
    [ID]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [NotificationID]         BIGINT         NULL,
    [ReportCode]             VARCHAR (64)   NOT NULL,
    [ReportMessage]          NVARCHAR (256) NOT NULL,
    [AppCode]                VARCHAR (64)   NOT NULL,
    [DeviceToken]            VARCHAR (64)   NOT NULL,
    [Payload]                NVARCHAR (MAX) NOT NULL,
    [NotificationIdentifier] VARCHAR (64)   NOT NULL,
    [ExpirationData]         DATETIME       NOT NULL,
    [Priority]               INT            NOT NULL,
    CONSTRAINT [PK_NOTIFICATIONREPORT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0       No errors encountered
   1       Processing error
   2       Missing device token
   3       Missing topic
   4       Missing payload
   5       Invalid token size
   6       Invalid topic size
   7       Invalid payload size
   8       Invalid token
   10     Shutdown
   255   None (unknown)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NotificationReport', @level2type = N'COLUMN', @level2name = N'ReportCode';

