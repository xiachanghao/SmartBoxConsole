CREATE TABLE [dbo].[ActionExtend] (
    [PluginCode] VARCHAR (64) NOT NULL,
    [ActionCode] VARCHAR (64) NULL,
    [Summary]    TEXT         NULL,
    CONSTRAINT [PK_ACTIONEXTEND] PRIMARY KEY CLUSTERED ([PluginCode] ASC),
    CONSTRAINT [FK_ACTIONEX_REFERENCE_PLUGININ] FOREIGN KEY ([PluginCode]) REFERENCES [dbo].[PluginInfo] ([PluginCode])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ActionExtend', @level2type = N'COLUMN', @level2name = N'PluginCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'action说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ActionExtend', @level2type = N'COLUMN', @level2name = N'Summary';

