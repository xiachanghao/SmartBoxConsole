CREATE TABLE [dbo].[PluginCategory] (
    [PluginCateId]   INT            IDENTITY (1, 1) NOT NULL,
    [PluginCateCode] VARCHAR (64)   NULL,
    [DisplayName]    NVARCHAR (256) NULL,
    CONSTRAINT [PK_PLUGINCATEGORY] PRIMARY KEY CLUSTERED ([PluginCateId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件类型如面板类插件，后台服务类插件，主菜单类插件等,Action', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增长主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginCategory', @level2type = N'COLUMN', @level2name = N'PluginCateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件分类标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginCategory', @level2type = N'COLUMN', @level2name = N'PluginCateCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件分类的显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginCategory', @level2type = N'COLUMN', @level2name = N'DisplayName';

