CREATE TABLE [dbo].[DeviceConfig] (
    [Key]       NVARCHAR (64)  NOT NULL,
    [ValueType] INT            NOT NULL,
    [Value]     NVARCHAR (512) NULL,
    [XmlValue]  XML            NULL,
    CONSTRAINT [PK_DeviceConfig] PRIMARY KEY CLUSTERED ([Key] ASC)
);

