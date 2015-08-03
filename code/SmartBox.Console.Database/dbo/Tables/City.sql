CREATE TABLE [dbo].[City] (
    [City_Code] VARCHAR (9)  NOT NULL,
    [City_Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED ([City_Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'天气预报中城市信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City';

