CREATE TABLE [dbo].[SystemConfig] (
    [Key]   NVARCHAR (64)  NOT NULL,
    [Value] NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED ([Key] ASC)
);

