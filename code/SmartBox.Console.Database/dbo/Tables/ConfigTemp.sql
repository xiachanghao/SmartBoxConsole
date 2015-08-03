CREATE TABLE [dbo].[ConfigTemp] (
    [PlugInCode]         VARCHAR (64)   NOT NULL,
    [ConfigCategoryCode] VARCHAR (64)   NULL,
    [Key]                VARCHAR (256)  NOT NULL,
    [Value]              VARCHAR (512)  NULL,
    [Summary]            NVARCHAR (512) NULL,
    [IsPublic]           BIT            CONSTRAINT [DF_ConfigTemp_IsPublic] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CONFIGTEMP] PRIMARY KEY CLUSTERED ([PlugInCode] ASC, [Key] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置临时表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件id，如果是主程序该字段为SmartBoxTEST', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp', @level2type = N'COLUMN', @level2name = N'PlugInCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'分类标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp', @level2type = N'COLUMN', @level2name = N'ConfigCategoryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置键值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp', @level2type = N'COLUMN', @level2name = N'Key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置键值所对应的数据值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ConfigTemp', @level2type = N'COLUMN', @level2name = N'Summary';

