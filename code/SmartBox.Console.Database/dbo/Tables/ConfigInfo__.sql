CREATE TABLE [dbo].[ConfigInfo__] (
    [ConfigId]           INT            IDENTITY (1, 1) NOT NULL,
    [ConfigCategoryCode] VARCHAR (64)   NULL,
    [UserUId]            VARCHAR (64)   NULL,
    [PlugInCode]         VARCHAR (64)   NULL,
    [Key]                VARCHAR (256)  NULL,
    [Value]              VARCHAR (512)  NULL,
    [Summary]            NVARCHAR (512) NULL,
    [IsPublic]           BIT            NOT NULL
);

