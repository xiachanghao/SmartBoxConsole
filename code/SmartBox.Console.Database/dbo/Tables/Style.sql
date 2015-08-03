CREATE TABLE [dbo].[Style] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (64)  NOT NULL,
    [DipalsyName] NVARCHAR (256) NOT NULL,
    [ClientType]  NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_Style] PRIMARY KEY CLUSTERED ([ID] ASC)
);

