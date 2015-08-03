CREATE TABLE [dbo].[ClientType] (
    [ClientType]  NVARCHAR (64)  NOT NULL,
    [DisplayName] NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (512) NULL,
    CONSTRAINT [PK_CLIENTTYPE] PRIMARY KEY CLUSTERED ([ClientType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述终端的类型，包含硬件类型（如PC，平板，Phone）和系统类型（如Window, Android, iOS）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入端的类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClientType', @level2type = N'COLUMN', @level2name = N'ClientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入终端类型的显示名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClientType', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入终端类型的描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClientType', @level2type = N'COLUMN', @level2name = N'Description';

