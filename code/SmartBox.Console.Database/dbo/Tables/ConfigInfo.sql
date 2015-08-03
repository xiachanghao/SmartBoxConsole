CREATE TABLE [dbo].[ConfigInfo] (
    [ConfigId]           INT            IDENTITY (1, 1) NOT NULL,
    [ConfigCategoryCode] VARCHAR (64)   NULL,
    [UserUId]            VARCHAR (64)   NULL,
    [PlugInCode]         VARCHAR (64)   NULL,
    [Key]                VARCHAR (256)  NULL,
    [Value]              VARCHAR (512)  NULL,
    [Summary]            NVARCHAR (512) NULL,
    [IsPublic]           BIT            CONSTRAINT [DF_ConfigInfo_IsPublic] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CONFIGINFO] PRIMARY KEY CLUSTERED ([ConfigId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置信息表，与UserInfo和PluginInfo有关联，但是可能有些配置信息只在一个字段或两个字段都为空（系统配置等）所以没有设置外键，', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增长主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'ConfigId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置分类
   全局配置:GlobalConfig
   常规系统配置:SysCommonConfig
   常规插件配置:PluginConfig
   个人系统配置:PersonalSysConfig
   个人插件配置:PersonalPluginConfig', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'ConfigCategoryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户登录id，如果是用户个人配置则此字段有值，否则没有值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'UserUId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识，如果是插件配置则有值否则为空', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'PlugInCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置的键值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'Key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置的值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigInfo', @level2type = N'COLUMN', @level2name = N'Summary';

