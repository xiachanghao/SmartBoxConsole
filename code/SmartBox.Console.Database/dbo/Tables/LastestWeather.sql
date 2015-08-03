CREATE TABLE [dbo].[LastestWeather] (
    [City_Code]            VARCHAR (9)  NOT NULL,
    [Hour]                 CHAR (2)     NOT NULL,
    [Temperature]          VARCHAR (20) NULL,
    [Wind_Direction]       VARCHAR (20) NULL,
    [Wind_Power]           VARCHAR (20) NULL,
    [Wind_Direction_Max]   VARCHAR (20) NULL,
    [Wind_Power_Max]       VARCHAR (20) NULL,
    [Humidity]             VARCHAR (20) NULL,
    [Atmospheric_Pressure] VARCHAR (20) NULL,
    CONSTRAINT [PK_LASTESTWEATHER] PRIMARY KEY CLUSTERED ([City_Code] ASC, [Hour] ASC),
    CONSTRAINT [FK_LASTESTW_CODE=CODE_CITY] FOREIGN KEY ([City_Code]) REFERENCES [dbo].[City] ([City_Code])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当天天气预报实时信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LastestWeather';

