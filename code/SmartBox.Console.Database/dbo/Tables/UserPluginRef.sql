CREATE TABLE [dbo].[UserPluginRef] (
    [UserUId]    VARCHAR (64) NOT NULL,
    [PluginCode] VARCHAR (64) NOT NULL,
    [IsUse]      BIT          NULL,
    [Sequence]   INT          NULL,
    CONSTRAINT [PK_USERPLUGINREF] PRIMARY KEY CLUSTERED ([UserUId] ASC, [PluginCode] ASC),
    CONSTRAINT [FK_USERPLUG_REFERENCE_PLUGININ] FOREIGN KEY ([PluginCode]) REFERENCES [dbo].[PluginInfo] ([PluginCode])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserPluginRef', @level2type = N'COLUMN', @level2name = N'UserUId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserPluginRef', @level2type = N'COLUMN', @level2name = N'PluginCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录用户引用的插件是否引用，', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserPluginRef', @level2type = N'COLUMN', @level2name = N'IsUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserPluginRef', @level2type = N'COLUMN', @level2name = N'Sequence';

