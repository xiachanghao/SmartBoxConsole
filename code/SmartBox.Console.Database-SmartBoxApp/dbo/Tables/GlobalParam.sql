CREATE TABLE [dbo].[GlobalParam] (
    [ConfigKey]   NVARCHAR (50)  NOT NULL,
    [ConfigValue] NVARCHAR (50)  NULL,
    [ConfigDesc]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_GlobalParam] PRIMARY KEY CLUSTERED ([ConfigKey] ASC)
);

