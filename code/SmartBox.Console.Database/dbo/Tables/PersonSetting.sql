CREATE TABLE [dbo].[PersonSetting] (
    [Uid]        NVARCHAR (64)   NOT NULL,
    [ClientType] NVARCHAR (64)   NOT NULL,
    [Key]        NVARCHAR (128)  NOT NULL,
    [Value]      NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_PERSONSETTING] PRIMARY KEY CLUSTERED ([Uid] ASC, [ClientType] ASC, [Key] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户的个人配置，如使用的应用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PersonSetting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置的用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PersonSetting', @level2type = N'COLUMN', @level2name = N'Uid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'启用的应用列表，以XML形式保存', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PersonSetting', @level2type = N'COLUMN', @level2name = N'Value';

