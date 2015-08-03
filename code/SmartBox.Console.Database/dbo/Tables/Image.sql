CREATE TABLE [dbo].[Image] (
    [ID]       INT        IDENTITY (100000, 1) NOT NULL,
    [HashCode] CHAR (128) NULL,
    [Data]     IMAGE      NOT NULL,
    CONSTRAINT [PK_IMAGE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储图片', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Image';

