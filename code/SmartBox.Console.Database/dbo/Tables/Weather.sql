CREATE TABLE [dbo].[Weather] (
    [City_Code]       VARCHAR (9)  NOT NULL,
    [Day]             DATETIME     NOT NULL,
    [Day_Night]       VARCHAR (20) NOT NULL,
    [WeatherIcoIndex] VARCHAR (2)  NULL,
    [Weather]         VARCHAR (20) NULL,
    [Temperature]     VARCHAR (20) NULL,
    [Wind_Direction]  VARCHAR (20) NULL,
    [Wind_Power]      VARCHAR (20) NULL,
    CONSTRAINT [PK_WEATHER] PRIMARY KEY CLUSTERED ([City_Code] ASC, [Day] ASC, [Day_Night] ASC),
    CONSTRAINT [FK_WEATHER_CODE=CODE_CITY] FOREIGN KEY ([City_Code]) REFERENCES [dbo].[City] ([City_Code])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最近City表中所有城市的最近三天记录', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Weather';

