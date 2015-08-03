CREATE TABLE [dbo].[DeviceException] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [useruid] NVARCHAR (50) NULL,
    [type]    INT           NULL,
    CONSTRAINT [PK_DeviceException] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1启用例外 2禁用例外', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceException', @level2type = N'COLUMN', @level2name = N'type';

